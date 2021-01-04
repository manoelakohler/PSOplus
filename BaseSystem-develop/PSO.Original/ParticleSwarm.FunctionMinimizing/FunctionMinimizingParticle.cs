using ParticleSwarm.Particles;

namespace ParticleSwarm.FunctionMinimizing
{
    public sealed class FunctionMinimizingParticle : Particle
	{
		private readonly FunctionBase _function;
		//---------------------------------------------------------------------
		public FunctionMinimizingParticle(
			FunctionBase function,
			Particles.ParticleSwarm swarm,
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