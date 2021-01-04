using System;
using PSO.Interfaces;

namespace PSO.ParticlesNLC
{
    /// <summary>
    /// Provides a basic implementation of the parcticle swarm optimization.
    /// </summary>
    public abstract class ParticleSwarmNLC
    {
        #region Variables
        protected static Random Rnd = new Random();
        #endregion
        //---------------------------------------------------------------------
        #region Properties

        /// <summary>
        /// Define se otimização é de maximização ou minimização
        /// </summary>
        public bool IsMaximization { get; set; }

        /// <summary>
        /// The particles of this swarm.
        /// </summary>
        public ParticleNLC[] SearchParticles { get; set; }
        
        /// <summary>
        /// The particles of this swarm.
        /// </summary>
        public ParticleNLC[] ReferenceParticles { get; set; }

        public ParticleNLC[] FootHolds { get; set; }

        //---------------------------------------------------------------------
        /// <summary>
        /// Indexer.
        /// </summary>
        public ParticleNLC this[int index]
        {
            get { return SearchParticles[index]; }
            set { SearchParticles[index] = value; }
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// The number of particles in the swarm.
        /// </summary>
        public int SwarmSize
        {
            get
            {
                return SearchParticles == null ? 0 : SearchParticles.Length;
            }
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// The cost of the current best particle of the swarm.
        /// </summary>
        public virtual double Cost { get { return /*this[0].Cost*/ ReferenceParticles[0].Cost; } }
        //---------------------------------------------------------------------
        /// <summary>
        /// The cost of the best particle so far.
        /// </summary>
        public double BestCost { get; protected set; }
        //---------------------------------------------------------------------
        /// <summary>
        /// The current best of position of any particle in the swarm.
        /// </summary>
        /// <remarks>gBest.</remarks>
        public int[] CurrentBestPosition { get { return /*this[0].Position*/ ReferenceParticles[0].Position; } }
        //---------------------------------------------------------------------
        /// <summary>
        /// The best position of the swarm so far.
        /// </summary>
        public int[] BestPosition { get; protected set; }
        //---------------------------------------------------------------------
        //private double _tendencyToOwnBest = 1;
        private double _tendencyToOwnBest;
        /// <summary>
        /// Accelerationcoefficient c1. Default value: 1
        /// </summary>
        public double TendencyToOwnBest
        {
            get { return _tendencyToOwnBest; }
            set { _tendencyToOwnBest = value; }
        }
        //---------------------------------------------------------------------
        //private double _tendencyToGlobalBest = 1;
        private double _tendencyToGlobalBest;
        /// <summary>
        /// Accelerationcoefficient c2. Default value: 1
        /// </summary>
        public double TendencyToGlobalBest
        {
            get { return _tendencyToGlobalBest; }
            set { _tendencyToGlobalBest = value; }
        }
        //---------------------------------------------------------------------
        //todo: tentar outras abordagens com momento
        //private double _momentum = 1.05;
        private double _momentum;
        /// <summary>
        /// Momentum. Default value: 1.05</summary>
        public double Momentum
        {
            get { return _momentum; }
            set { _momentum = value; }
        }
        //---------------------------------------------------------------------
        private double _percentMaximumVelocityOfSearchSpace = 1;
        /// <summary>
        /// Factor for maximum allowed velocity. 
        /// vmax = k * xmax mit k in [0.1, 1].
        /// Defaults to 1.
        /// </summary>
        public double PercentMaximumVelocityOfSearchSpace
        {
            get { return _percentMaximumVelocityOfSearchSpace; }
            set { _percentMaximumVelocityOfSearchSpace = value; }
        }
        //---------------------------------------------------------------------
        private bool _useGlobalOptimum = false;
        /// <summary>
        /// Determines wether to use the global optimum or the current best
        /// solution for updating the particles. Default: false
        /// </summary>
        /// <remarks>
        /// This can be an improvement on speed (converges faster) but 
        /// the possibility to get stucked in a local optimum raises.
        /// <para>
        /// The global versiön can be used to get a rough solution and the 
        /// refine with the current best version.
        /// </para>
        /// </remarks>
        public bool UseGlobalOptimum
        {
            get { return _useGlobalOptimum; }
            set { _useGlobalOptimum = value; }
        }

        public IConstraints Constraints { get; set; }
        public Random Sampler { get; set; }
        /// <summary>
        /// Dimension of the optimization problem
        /// </summary>
        public int Dimension { get; set; }

        public bool UseFootholds { get; set; }

        #endregion
        //---------------------------------------------------------------------
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tendencyToOwnBest"></param>
        /// <param name="tendencyToGlobalBest"></param>
        /// <param name="momentum"></param>
        protected ParticleSwarmNLC(double tendencyToOwnBest, double tendencyToGlobalBest, double momentum)
        {
            //BestCost = double.MaxValue;

            _tendencyToOwnBest = tendencyToOwnBest;
            _tendencyToGlobalBest = tendencyToGlobalBest;
            _momentum = momentum;
        }

        protected ParticleSwarmNLC()
        {
            //BestCost = double.MaxValue; 

            _tendencyToOwnBest = 1;
            _tendencyToGlobalBest = 1;
            _momentum = 1.05;
        }

        #endregion
        //---------------------------------------------------------------------
        #region Methods
        /// <summary>
        /// Sorts the particles according their cost.
        /// </summary>
        public void SortParticles()
        {
            Array.Sort(SearchParticles);
            Array.Sort(ReferenceParticles);

            if (IsMaximization)
            {
                Array.Reverse(ReferenceParticles);
                Array.Reverse(SearchParticles);
            }
        }
        public void UpdateCostAndHistory()
        {
            // Update history of the swarm:
            if (IsMaximization)
            {
                if (!(Cost >= BestCost)) return;
            }
            else
            {
                if (!(Cost <= BestCost)) return;
            }

            BestCost = Cost;
            BestPosition = ReferenceParticles[0].BestPosition;
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Does one iteration of the algorithm.
        /// </summary>
        public virtual void Iteration(int iteration)
        {
            // Foreach particle calculate the cost and update the history:
            var localParticles = SearchParticles;	// Range-Check-Elimination
            foreach (var particle in localParticles)
            {
                //todo: se a iteração é a primeira, a avaliação da partícula já foi feita, então o CalculateCost não deve ser chamado de novo.
                //todo: Por outro lado a "história" da partícula ainda não foi atualizada, então está correto chamá-la em todas as iterações.
                particle.CalculateCost();
                particle.UpdateHistory();
            }

            // Sort according to fitness:
            SortParticles();

            // Update history of the swarm:
            if (IsMaximization)
            {
                if (Cost > BestCost)
                {
                    BestCost = Cost;
                    BestPosition = ReferenceParticles[0].BestPosition;
                }
            }
            else
            {
                if (Cost < BestCost)
                {
                    BestCost = Cost;
                    BestPosition = ReferenceParticles[0].BestPosition;
                }
            }

            // Determine new velocity and position of the particles in the swarm:
            foreach (var particle in localParticles)
                particle.UpdateVelocityAndPosition(_useGlobalOptimum ? BestPosition : ReferenceParticles[0].Position, Constraints);
        }

        /// <summary>
        /// Verifica iterações sem melhora.
        /// </summary>
        /// <param name="oldMinimum"></param>
        /// <param name="minimum"></param>
        /// <param name="countSame"></param>
        /// <returns></returns>
        public double VerifyNoImprovement(double oldMinimum, double minimum, ref int countSame)
        {
            if (Math.Abs(oldMinimum - minimum) < 0.001)
            {
                countSame++;
                oldMinimum = minimum;
            }
            else
            {
                countSame = 0;
                oldMinimum = minimum;
            }
            return oldMinimum;
        }
        #endregion
    }
}
