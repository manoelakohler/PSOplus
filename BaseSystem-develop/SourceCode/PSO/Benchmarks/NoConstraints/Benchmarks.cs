using System;
using System.Linq;
using PSO.Benchmarks.NonLinearConstraints;

namespace PSO.Benchmarks.NoConstraints
{
    public class SphereFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            return x * x + y * y;
        }
    }
    //-------------------------------------------------------------------------
    /// <summary>
    /// https://www.google.com.br/webhp?sourceid=chrome-instant&ion=1&espv=2&es_th=1&ie=UTF-8#q=global+minimum+PeaksFunction&es_th=1
    /// </summary>
    public class PeaksFunction : FunctionBase //rever, tem algo de errado
    {
        public override double Function(double x, double y)
        {
         var z =
                3 * (1 - x) * (1 - x) *
                Math.Exp(-x * x) -
                (y + 1) * (y + 1) -
                10 * (x / 5 - x * x * x - y * y * y * y * y) *
                Math.Exp(-x * x - y * y) -
                1 / 3 * Math.Exp(-(x + 1) * (x + 1) - y * y);
            return 1 - z;
        }
    }
    //-------------------------------------------------------------------------
    public class StepFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            return Math.Abs(x) + Math.Abs(y);
        }
    }
    //-------------------------------------------------------------------------
    public class RosenbockFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            //return 100 * (x * x - y * y) * (x * x - y * y) + (1 - x) * (1 - x);
            return 100 * (y - x * x) * (y - x * x) + (1 - x) * (1 - x);
        }
    }
    //-------------------------------------------------------------------------
    public class SincFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            var r = Math.Sqrt(x * x + y * y);
            var z = r == 0 ? 1 : Math.Sin(r) / r;

            return 1 - z;
        }
    }
    //-------------------------------------------------------------------------
    public class ManyPeaksFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            // x, y in [-3,3] -> [0,1]:
            x = (x + 3) / 6d;
            y = (y + 3) / 6d;

            var z = 15 * x * y * (1 - x) * (1 - y) * Math.Sin(9 * Math.PI * y);
            z *= z;

            return 1 - z;
        }
    }
    //-------------------------------------------------------------------------
    public class HoleFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            // x,y in [-3,3] -> [-5,5]:
            x = (x + 3) / 6d;	// [0,1]
            y = (y + 3) / 6d;	// [0,1]
            x = 10 * x - 5;
            y = 10 * y - 5;

            var z = Math.Cos(x) * Math.Cos(y);
            z *= z * z;
            return 1 - z;
        }
    }
    //-------------------------------------------------------------------------
    public class BumpsFunction : FunctionBase
    {
        public override double Function(double x, double y)
        {
            // x,y in [-3,3] -> [-5,5]:
            x = (x + 3) / 6d;	// [0,1]
            y = (y + 3) / 6d;	// [0,1]
            x = 10 * x - 5;
            y = 10 * y - 5;

            var z = Math.Cos(x) + Math.Cos(y);
            z = Math.Abs(z);
            return Math.Sqrt(z);
        }
    }

    #region Funcoes Harold

    //Sphere
    public class SphereFunction600 : FunctionBase2
    {
        public override double Function(double []x)
        {
            var accumulator = 0.0;
            for (var i = 0; i < x.Length; i++)
            {
                accumulator = accumulator + x.ElementAt(i)*x.ElementAt(i);
            }
            return accumulator;
        }
    }

    //Ackley
    public class AckleyFunction30 : FunctionBase2
    {
        public override double Function(double[] x)
        {
            var f1 = -20*Math.Exp(-0.2*Math.Sqrt(0.1 * MathOperations.SquaredSum(1, 10,x)));
            var f2 = -Math.Exp(0.1*MathOperations.Cos2PiSum(1, 10, x));
            var f3 = 20 + Math.Exp(1);
            return f1+f2+f3;
        }
    }

    //Rastrigin
    public class Rastrigin5Dot12 : FunctionBase2
    {
        public override double Function(double[] x)
        {
            var accumulator = 0.0;
            for (int i = 0; i < x.Length; i++)
            {
                var f = x.ElementAt(i) * x.ElementAt(i) - 10 * Math.Cos(2 * Math.PI * x.ElementAt(i)) + 10;
                accumulator = accumulator + f;
            }
            return accumulator;
        }
    }

    //Rosenbrock
    public class RosenbrockFunctionMinus9To11 : FunctionBase2
    {
        public override double Function(double[] x)
        {
            var accumulator = 0.0;
            for (var i = 0; i < x.Length-1; i++)
            {
                accumulator = accumulator + 100 * (x.ElementAt(i) * x.ElementAt(i) - x.ElementAt(i)) * 
                                                  (x.ElementAt(i) * x.ElementAt(i) - x.ElementAt(i)) +
                                                  (x.ElementAt(i) - 1)*(x.ElementAt(i) - 1);
            }
            return accumulator;
        }
    }

    //Griewank
    public class GriewankFunction30 : FunctionBase2
    {
        public override double Function(double[] x)
        {
            var f1 = MathOperations.SquaredSumDividedByNumber(1, 10, x,4000);
            var f2 = MathOperations.CosProductGriewank(1,10, x);
            return 1 + f1 - f2;
        }
    }
    #endregion


}
