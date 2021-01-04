namespace PSO.Interfaces
{
    public interface IParticle
    {
        double[] Position { get; set; }
    }

    public interface IParticleNLC
    {
        int[] Position { get; set; }
    }
}
