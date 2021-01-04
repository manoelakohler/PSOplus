using System;
using System.Linq;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;
using PSO.ParticlesNLC;

namespace PSO.Optimization.ParticlesNLC
{
    public sealed class OptimizationParticleNLC : ParticleNLC, ICloneable
    {
        private readonly NLFunctionBase _function;
        private readonly IConstraints _constraints;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="swarm"></param>
        /// <param name="position"></param>
        /// <param name="velocity"></param>
        /// <param name="function"></param>
        /// <param name="constraints"></param>
        public OptimizationParticleNLC(ParticleSwarmNLC swarm, int[] position, int[] velocity, NLFunctionBase function, IConstraints constraints)
        {
            _function = function;
            _constraints = constraints;
            Swarm = swarm;
            Position = position;
            Velocity = velocity;
            BestCost = Cost = Swarm.IsMaximization ? Double.MinValue : Double.MaxValue;
            CalculateCost();
        }

        public override void CalculateCost()
        {
            if (_constraints.IsFeasible(this))
                Cost = _function.Function(Position);
        }

        public object Clone()
        {
            var newPosition = new int[Position.Count()];
            for (var positionIndex = 0; positionIndex < Position.Count(); positionIndex++)
            {
                newPosition[positionIndex] = Position[positionIndex];
            }
            var newVelocity = new int[Velocity.Count()];
            for (var velocityIndex = 0; velocityIndex < Velocity.Count(); velocityIndex++)
            {
                newVelocity[velocityIndex] = Velocity[velocityIndex];
            }

            var newParticle = new OptimizationParticleNLC(Swarm, newPosition, newVelocity, _function, _constraints)
            {
                BestCost = BestCost,
                BestPosition = BestPosition,
                Cost = Cost
            };

            return newParticle;
        }
    }
}
