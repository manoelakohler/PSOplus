using System;
using System.Linq;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;


namespace PSO.ParticlesNLC
{
    /// <summary>
    /// Provides a basic implementation of a paricle.
    /// </summary>
    public abstract class ParticleNLC : IComparable<ParticleNLC>, IParticleNLC
    {
        #region Variables
        protected static Random Rnd = new Random();
        public double BestCost { get; set;}

        /// <summary>
        /// Numero máximo de tentativas que o algoritmo fará até encontrar uma partícula válida.
        /// </summary>
        private const int MaximumNumberOfTrials = 400;

        #endregion
        //---------------------------------------------------------------------
        #region Properties
        /// <summary>
        /// The cost for this particle. The lower the better.
        /// </summary>
        public double Cost { get; set; }
        //---------------------------------------------------------------------
        /// <summary>
        /// The current position of the particle.
        /// </summary>
        public virtual int[] Position { get; set; }
        //---------------------------------------------------------------------
        /// <summary>
        /// The best position of this particle so far.
        /// </summary>
        /// <remarks>pBest.</remarks>
        public int[] BestPosition { get; protected set; }
        //---------------------------------------------------------------------
        /// <summary>
        /// Indexer.
        /// </summary>
        /// <param name="index">Index.</param>
        /// <exception cref="IndexOutOfRangeException">
        /// The index is outside the valid limits.
        /// </exception>
        public int this[int index]
        {
            get { return Position[index]; }
            set { Position[index] = value; }
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// The velocity of the particle.
        /// </summary>
        public int[] Velocity { get; protected set; }
        //---------------------------------------------------------------------
        /// <summary>
        /// The particle swarm to whom this particle belongs.
        /// </summary>
        public ParticleSwarmNLC Swarm { get; protected set; }
        #endregion
        //---------------------------------------------------------------------
        #region Methods
        /// <summary>
        /// Calculates the cost for this particle.
        /// </summary>
        public abstract void CalculateCost();
        //---------------------------------------------------------------------
        /// <summary>
        /// Updates the history for this particle.
        /// </summary>
        public virtual void UpdateHistory()
        {
            if (Swarm.IsMaximization)
            {
                if (!(Cost >= BestCost)) return;
            }
            else
            {
                if (!(Cost <= BestCost)) return;
            }
            //if (!Swarm.Constraints.IsFeasible(this)) return;

            BestCost = Cost;
            BestPosition = GetArrayCopy();
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Updates the position (and velocity) of the particle.
        /// </summary>
        /// <param name="bestPositionOfSwarm">
        /// The current best position of the particle swarm.
        /// </param>
        /// <param name="nonLinearConstraints"></param>
        public void UpdateVelocityAndPosition(int[] bestPositionOfSwarm, NonLinearConstraints nonLinearConstraints)
        {
            if (BestPosition == null)
                UpdateHistory();

            // Determine maximum allowed velocity:
            var xmax = Math.Max(
                Math.Abs(Position.Min()),
                Math.Abs(Position.Max()));
            var vmax = (int)(Swarm.PercentMaximumVelocityOfSearchSpace * xmax);

            // Range-Check-Elimination: Therefore get a reference to the arrays
            // on the local stack -> improvement of speed:
            var localVelocity = Velocity;
            var localPosition = Position;
            var localBestPosition = BestPosition;

            for (var i = 0; i < localVelocity.Length; i++)
            {
                // Factors for calculating the velocity:
                var c1 = Swarm.TendencyToOwnBest;
                var r1 = Rnd.NextDouble();
                var c2 = Swarm.TendencyToGlobalBest;
                var r2 = Rnd.NextDouble();
                var m = Swarm.Momentum;

                // New velocity of the particle:
                var newVelocity = (int)(
                    m * localVelocity[i] + 
                    c1 * r1 * (localBestPosition[i] - localPosition[i]) +
                    c2 * r2 * (bestPositionOfSwarm[i] - localPosition[i]));

                // Limit the velocity to the maximum value:
                if (newVelocity > vmax)
                    newVelocity = vmax;
                if (newVelocity < -vmax)
                    newVelocity = -vmax;

                // Assign new velocity and calculate the new position:
                localVelocity[i] = newVelocity;
                localPosition[i] += localVelocity[i];
            }

            //Verifica validade da partícula
            var isFeasible = nonLinearConstraints.IsFeasible(this);

            if (!isFeasible)
            {
                RecalculateVelocityAndPosition(nonLinearConstraints);
            }
            UpdateCostAndHistory();
        }

        private void UpdateCostAndHistory()
        {
            //from current particle
            CalculateCost();
            UpdateHistory();
        }

        /// <summary>
        /// Recalcula velocidade e posição da partícula que não obedecer às restrições lineares.
        /// </summary>
        private void RecalculateVelocityAndPosition(IConstraints constraints)
        {
            //Pego uma partícula válida da população de referencia (90% de porbabilidade) ou foothold (10% de probabilidade).
            //Faço um crossover aritmético da melhor partícula válida com a partícula inválida.
            //Objetivo é tentar aproximar a partícula inválida do espaço de soluções válidas. Refaço o procedimento até que a partícula inválida se torne válida
            var nTrials = 0;
            var isFeasible = false;
            var probability = Swarm.Sampler.NextDouble();
            var random = Swarm.Sampler.Next(0, Swarm.ReferenceParticles.Count());
            var chosenParticle = probability < 0.6 ? Swarm.ReferenceParticles.ElementAt(random) : Swarm.FootHolds.ElementAt(random);

            while (!isFeasible && nTrials < MaximumNumberOfTrials)
            {
                var randomNumber = Swarm.Sampler.NextDouble();
                var newPosition = new int[Position.Count()];

                for (var positionIndex = 0; positionIndex < Position.Count(); positionIndex++)
                {
                    var feasiblePosition = chosenParticle.Position.ElementAt(positionIndex);
                    var infeasiblePosition = Position.ElementAt(positionIndex);

                    var newPositionValue = randomNumber * feasiblePosition +
                                           (1 - randomNumber) * infeasiblePosition;

                    newPosition[positionIndex] = (int)newPositionValue;
                }
                //Atualiza partícula
                Position = newPosition;

                //Verifica restrições lineares atualiza isFeasible.
                if (constraints.IsFeasible(this)) 
                    isFeasible = true;
                nTrials++;
            }

            // se depois de Max tentativas nao encontrar uma partícula válida, copia a original ???
            // ou retorna erro???? todo: avaliar impacto aqui
            if (nTrials == MaximumNumberOfTrials)
            {
                Position = chosenParticle.Position;
                //modificar velocidade?????

                //if (!linearConstraints.IsFeasible(this))
                //    throw new Exception("Partícula inválida.");
            }
        }

        

        #endregion
        //---------------------------------------------------------------------
        #region Private Methoden
        /// <summary>
        /// Gets a copy of the current solution vector.
        /// </summary>
        public int[] GetArrayCopy()
        {
            var tmp = new int[Position.Length];
            Position.CopyTo(tmp, 0);

            return tmp;
        }
        #endregion
        //---------------------------------------------------------------------
        #region IComparable<Particle> Member
        /// <summary>
        /// Compares to particles. Used for sorting.
        /// </summary>
        public int CompareTo(ParticleNLC other)
        {
            if (other == null)
                throw new ArgumentNullException("other");
            //-----------------------------------------------------------------
            if (this == other || this.Cost == other.Cost)
                return 0;

            if (Cost > other.Cost)
                return 1;
            return -1;
        }
        #endregion
    }
}
