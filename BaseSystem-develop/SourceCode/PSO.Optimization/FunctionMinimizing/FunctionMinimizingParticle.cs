using PSO.Benchmarks.NoConstraints;
using PSO.Particles;

namespace PSO.Optimization.FunctionMinimizing
{
    public sealed class FunctionMinimizingParticle : Particle
    {
        private readonly FunctionBase _function;
        //---------------------------------------------------------------------
        public FunctionMinimizingParticle(
            FunctionBase function,
            ParticleSwarm swarm,
            double[] position,
            double[] velocity)
        {
            _function = function;
            Swarm = swarm;
            Position = position;
            Velocity = velocity;

            CalculateCost();
        }
        //---------------------------------------------------------------------
        public override void CalculateCost()
        {
            Cost = _function.Function(Position);
        }
    }
}
