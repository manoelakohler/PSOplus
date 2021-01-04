using System;

namespace PSO.Particles
{
    /// <summary>
    /// Provides a basic implementation of the parcticle swarm optimization.
    /// </summary>
    public abstract class ParticleSwarm
    {
        #region Variables
        protected static Random Rnd = new Random();
        #endregion
        //---------------------------------------------------------------------
        #region Properties
        /// <summary>
        /// The particles of this swarm.
        /// </summary>
        public Particle[] Particles { get; set; }
        //---------------------------------------------------------------------
        /// <summary>
        /// Indexer.
        /// </summary>
        public Particle this[int index]
        {
            get { return Particles[index]; }
            set { Particles[index] = value; }
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// The number of particles in the swarm.
        /// </summary>
        public int SwarmSize
        {
            get
            {
                return Particles == null ? 0 : Particles.Length;
            }
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// The cost of the current best particle of the swarm.
        /// </summary>
        public virtual double Cost { get { return this[0].Cost; } }
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
        public double[] CurrentBestPosition { get { return this[0].Position; } }
        //---------------------------------------------------------------------
        /// <summary>
        /// The best position of the swarm so far.
        /// </summary>
        public double[] BestPosition { get; protected set; }
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
        #endregion
        //---------------------------------------------------------------------
        #region Constructor

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="tendencyToOwnBest"></param>
        /// <param name="tendencyToGlobalBest"></param>
        /// <param name="momentum"></param>
        protected ParticleSwarm(double tendencyToOwnBest, double tendencyToGlobalBest, double momentum)
        {
            BestCost = double.MaxValue;

            _tendencyToOwnBest = tendencyToOwnBest;
            _tendencyToGlobalBest = tendencyToGlobalBest;
            _momentum = momentum;
        }

        protected ParticleSwarm()
        {
            BestCost = double.MaxValue; 

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
            Array.Sort(Particles);
        }
        //---------------------------------------------------------------------
        /// <summary>
        /// Does one iteration of the algorithm.
        /// </summary>
        public virtual void Iteration(int iteration)
        {
            // Foreach particle calculate the cost and update the history:
            var localParticles = Particles;	// Range-Check-Elimination
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
            if (Cost < BestCost)
            {
                BestCost = Cost;
                BestPosition = this[0].BestPosition;
            }

            // Determine new velocity and position of the particles in
            // the swarm:
            foreach (var particle in localParticles)
                particle.UpdateVelocityAndPosition(_useGlobalOptimum ? BestPosition : this[0].Position);
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
