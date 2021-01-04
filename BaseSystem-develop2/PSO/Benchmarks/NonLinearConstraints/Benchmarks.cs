using System;
using System.Linq;

namespace PSO.Benchmarks.NonLinearConstraints
{
    public class G01 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = (1,1,1,1,1,1,1,1,1,3,3,3,1);
        /// 6 restrições ativas: (g1, g2, g3, g7, g8 e g9);
        /// f(x*) = -15;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(int[] x)
        {
            var z = 5*MathOperations.Sum(1, 4, x) - 5*MathOperations.SquaredSum(1, 4, x) - MathOperations.Sum(5, 13, x);
            return z;
        }

        public override double Function(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G02 : NLFunctionBase
    {
        /// <summary>
        /// Global maximum unknown;
        /// 1 restrição perto de ser ativa: (g1 = -10^-8);
        /// Best found: f(x*) = 0.803619;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            var numerator = MathOperations.CosPowerSum(1, 20, x, 4) - 2*MathOperations.CosPowerProduct(1, 20, x, 2);
            var denominator = Math.Sqrt(MathOperations.SquaredSumMultipliedByIndex(1, 20, x));
            return Math.Abs(numerator / denominator);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G03 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global maximum  xi* = 1/sqrt(n);
        /// f(x*) = 1;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return Math.Pow(Math.Sqrt(10), 10)*MathOperations.PowerProduct(1,10,x);
        }
    }

    public class G04 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = 78, 33, 29.995 256 025682, 45, 36.775 812 905 788;
        /// f(x*) = -30 665.539;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return (5.3578547*Math.Pow(x.ElementAt(2), 2)) + (0.8356891*x.ElementAt(0)*x.ElementAt(4)) +
                   (37.293239*x.ElementAt(0)) - 40792.141;
        }
    }

    public class G05 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Best known Glocal minimum x* = 679.9453, 1026.067, 0.1188764, -0.3962336;
        /// f(x*) = 5126.4981
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return 3*x.ElementAt(0) + 0.000001*Math.Pow(x.ElementAt(0), 3) + 2*x.ElementAt(1) +
                   (0.000002/3)*Math.Pow(x.ElementAt(1), 3);
        }
    }

    public class G06 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = 14.095, 0.84296
        /// f(x*) = -6961.81388
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return Math.Pow((x.ElementAt(0) - 10), 3) + Math.Pow((x.ElementAt(1) - 20), 3);
        }
    }

    public class G07 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = 2.171996, 2.363683, 8.773926, 5.095984, 0.9906548, 1.430574, 1.321644, 9.828726, 8.280092, 8.375927;
        /// f(x*) = 24.3062091;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return Math.Pow(x.ElementAt(0), 2) + Math.Pow(x.ElementAt(1), 2) + (x.ElementAt(0)*x.ElementAt(1)) -
                   14*x.ElementAt(0) - 16*x.ElementAt(1) + Math.Pow((x.ElementAt(2)-10), 2) +
                   4*(Math.Pow((x.ElementAt(3) - 5), 2)) + Math.Pow((x.ElementAt(4) - 3), 2) +
                   2*(Math.Pow((x.ElementAt(5) - 1), 2)) + 5*Math.Pow(x.ElementAt(6), 2) +
                   7*(Math.Pow((x.ElementAt(7) - 11), 2)) + 2*(Math.Pow((x.ElementAt(8) - 10), 2)) +
                   Math.Pow((x.ElementAt(9) - 7), 2) + 45;
        }
    }

    public class G08 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global maximum x*= 1.2279713, 4.2453733;
        /// f(x*) = 0.095825;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            var numerator = Math.Pow((Math.Sin(2*Math.PI*x.ElementAt(0))), 3)*Math.Sin(2*Math.PI*x.ElementAt(1));
            var denominator = Math.Pow(x.ElementAt(0), 3)*(x.ElementAt(0) + x.ElementAt(1));
            return numerator/denominator;
        }
    }

    public class G09 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = 2.330499, 1.951372, -0.4775414, 4.365726, -0.6244870, 1.038131, 1.594227;
        /// f(x*) = 680.6300573
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return Math.Pow((x.ElementAt(0) - 10), 2) + 5*(Math.Pow((x.ElementAt(1) - 12), 2)) +
                   Math.Pow(x.ElementAt(2), 4) + 3*(Math.Pow((x.ElementAt(3) - 11), 2)) + 10*Math.Pow(x.ElementAt(4), 6) +
                   7*Math.Pow(x.ElementAt(5), 2) + Math.Pow(x.ElementAt(6), 4) - 4*x.ElementAt(5)*x.ElementAt(6) -
                   10*x.ElementAt(5) - 8*x.ElementAt(6);
        }
    }

    public class G10 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = 579.3167, 1359.943, 5110.071, 182.0174, 295.5985, 217.9799, 286.4162, 395.5979;
        /// f(x*) = 7049.3307;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return x.ElementAt(0) + x.ElementAt(1) + x.ElementAt(2);
        }
    }

    public class G11 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = +/- 1/sqrt(2), 1/2;
        /// f(x*) = 0.75;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return Math.Pow(x.ElementAt(0), 2) + Math.Pow((x.ElementAt(1) - 1), 2);
        }
    }

    public class G12 : NLFunctionBase
    {
        /// <summary>
        /// Global maximum x* = 5, 5, 5
        /// f(x*) = 1
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(int[] x)
        {
            var numerator = 100 - Math.Pow((x.ElementAt(0)-5),2) - Math.Pow((x.ElementAt(1)-5),2) - Math.Pow((x.ElementAt(2)-5),2);
            return numerator/100;
        }

        public override double Function(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G13 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Global minimum x* = -1.717143, 1.595709, 1.827247, -0.7636413, -0.763645;
        /// f(x*) = 0.0539498;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return Math.Exp(x.ElementAt(0)*x.ElementAt(1)*x.ElementAt(2)*x.ElementAt(3)*x.ElementAt(4));
        }
    }


    /// <summary>
    /// Classe de operações usuais dos benchmarks
    /// </summary>
    public static class MathOperations
    {
        /// <summary>
        /// Faz o soma
        /// </summary>
        /// <returns></returns>
        public static double Sum(int lowerBound, int upperBound, int[] x)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += x.ElementAt(index);
            }
            return sum;
        }
        public static double Sum(int lowerBound, int upperBound, double[] x)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += x.ElementAt(index);
            }
            return sum;
        }

        /// <summary>
        /// Faz o soma do quadrado
        /// </summary>
        /// <returns></returns>
        public static double SquaredSum(int lowerBound, int upperBound, int[] x)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += Math.Pow(x.ElementAt(index), 2);
            }
            return sum;
        }
        public static double SquaredSum(int lowerBound, int upperBound, double[] x)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += Math.Pow(x.ElementAt(index), 2);
            }
            return sum;
        }

        /// <summary>
        /// Faz o soma
        /// </summary>
        /// <returns></returns>
        public static double SquaredSumMultipliedByIndex(int lowerBound, int upperBound, double[] x)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += (Math.Pow(x.ElementAt(index),2) * index);
            }
            return sum;
        }

        /// <summary>
        /// Faz o soma do cosseno do elemento elevado a um valor
        /// </summary>
        /// <returns></returns>
        public static double CosPowerSum(int lowerBound, int upperBound, double[] x, int power)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += Math.Pow(Math.Cos(x.ElementAt(index)), power);
            }
            return sum;
        }

        /// <summary>
        /// Faz o produtorio do cosseno do elemento elevado a um valor
        /// </summary>
        /// <returns></returns>
        public static double CosPowerProduct(int lowerBound, int upperBound, double[] x, int power)
        {
            double product = Math.Pow(Math.Cos(x.ElementAt(0)), power);
            for (var index = lowerBound; index < upperBound; index++)
            {
                product *= Math.Pow(Math.Cos(x.ElementAt(index)), power);
            }
            return product;
        }

        /// <summary>
        /// Faz o produtório dos elementos de um array
        /// </summary>
        /// <param name="lowerBound"></param>
        /// <param name="upperBound"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static double PowerProduct(int lowerBound, int upperBound, double[] x)
        {
            double product = x.ElementAt(0);
            for (int index = lowerBound; index < upperBound; index++)
            {
                product *= x.ElementAt(index);
            }
            return product;
        }
    }
}
