using System.Linq;

namespace PSO.Benchmarks.NonLinearConstraints
{
    public class ConstraintViolationAndVariables
    {
        public int ConstraintViolation { get; set; }
        public int[] MaxViolationVariables { get; set; }
        public double TotalViolation { get; set; }

        public ConstraintViolationAndVariables(int[,] violationVariables, int constraintViolation, double[] violationCost)
        {
            ConstraintViolation = constraintViolation;
            var maxViolation = violationCost.Max();
            var maxIndexViolation = violationCost.ToList().IndexOf(maxViolation);

            var count = violationVariables.GetLength(1);
            MaxViolationVariables = new int[count];
            for (var i = 0; i < count; i++)
            {
                MaxViolationVariables[i] = violationVariables[maxIndexViolation, i];
            }
            TotalViolation = violationCost.Sum();
        }
    }
}
