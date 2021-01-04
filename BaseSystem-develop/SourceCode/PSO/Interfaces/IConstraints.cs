using PSO.Benchmarks;
using PSO.Benchmarks.NonLinearConstraints;

namespace PSO.Interfaces
{
    public interface IConstraints
    {
        /// <summary>
        /// Check if an individual is feasible
        /// </summary>
        /// <returns>true if its feasible</returns>
        bool IsFeasible(IParticle particle);

        /// <summary>
        /// Check if an individual is feasible
        /// </summary>
        /// <returns>true if its feasible</returns>
        bool IsFeasible(IParticleNLC particle);

        BenchmarksNames FunctionName { get; }

        ConstraintViolationAndVariables GetConstraintViolation(double[] x);

    }
}
