using System;
using System.Linq;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;
using PSO.ParticlesNlcDouble;

namespace PSO.Optimization.ParticlesNlcDouble
{
    public sealed class OptimizationParticleNlcDouble : ParticleNlcDouble, ICloneable
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
        /// <param name="isConstraintVioloationOptimization"></param>
        public OptimizationParticleNlcDouble(ParticleSwarmNlcDouble swarm, double[] position, double[] velocity,
            NLFunctionBase function, IConstraints constraints, bool isConstraintVioloationOptimization = false)
        {
            _function = function;
            _constraints = constraints;
            Swarm = swarm;
            Position = position;
            Velocity = velocity;
            BestCost = Cost = Swarm.IsMaximization ? Double.MinValue : Double.MaxValue;
            if(isConstraintVioloationOptimization)
                CalculateCostCv();
            else
                CalculateCost();
        }

        public override void CalculateCost()
        {
            if (_constraints.IsFeasible(this))
                Cost = _function.Function(Position);
        }

        public override void CalculateCostCv()
        {
            Cost = _function.ConstraintsViolation(Position).TotalViolation;
        }

        public object Clone()
        {
            var newPosition = new double[Position.Count()];
            for (var positionIndex = 0; positionIndex < Position.Count(); positionIndex++)
            {
                newPosition[positionIndex] = Position[positionIndex];
            }
            var newVelocity = new double[Velocity.Count()];
            for (var velocityIndex = 0; velocityIndex < Velocity.Count(); velocityIndex++)
            {
                newVelocity[velocityIndex] = Velocity[velocityIndex];
            }

            var newParticle = new OptimizationParticleNlcDouble(Swarm, newPosition, newVelocity, _function, _constraints)
            {
                BestCost = BestCost,
                BestPosition = BestPosition,
                Cost = Cost
            };

            return newParticle;
        }
    }
}
