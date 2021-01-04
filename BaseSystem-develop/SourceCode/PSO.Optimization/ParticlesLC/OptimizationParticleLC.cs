using System;
using PSO.Benchmarks.NoConstraints;
using PSO.ParticlesLC;

namespace PSO.Optimization.ParticlesLC
{
    public sealed class OptimizationParticleLC : ParticleLC
    {
        private readonly object _function;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="swarm"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="function"></param>
        public OptimizationParticleLC(ParticleSwarmLC swarm, double[] position, double[] velocity, object function)
        {
            _function = function;
            Swarm = swarm;
            Position = position;
            Velocity = velocity;
            BestCost = Double.MaxValue;
            Rnd = new Random();
            CalculateCost();
        }

        public override void CalculateCost()
        {
            try
            {
                var function = (FunctionBase)_function;
                Cost = function.Function(Position);
            }
            catch (Exception)
            {
                var function = (FunctionBase2)_function;
                Cost = function.Function(Position);
            }
        }
    }
}
