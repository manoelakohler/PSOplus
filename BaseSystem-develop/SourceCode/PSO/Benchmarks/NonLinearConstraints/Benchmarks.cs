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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
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
            return -Math.Abs(numerator / denominator);
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G05);
            return constraints.GetConstraintViolationG05(x);
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G14 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 0.0406684113216282, 0.147721240492452, 0.783205732104114, 0.00141433931889084, 0.485293636780388, 0.000693183051556082,0.0274052040687766,0.0179509660214818, 0.0373268186859717, 0.0968844604336845
        /// f(x*) = -47.7648884594915;
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            var c = new double[] {-6.089, -17.164, -34.054, -5.914, -24.721, -14.986, -24.1, -10.708, -26.662, -22.179};
            var sum = MathOperations.Sum(1, x.Length, x);
            var accumulator = 0.0;
            for (var i = 0; i < x.Length; i++)
            {
                accumulator += x.ElementAt(i)*(c[i] + Math.Log(x.ElementAt(i) / sum));
            }
            return accumulator;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G14);
            return constraints.GetConstraintViolationG14(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }


    public class G15 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 3.51212812611795133,0.216987510429556135, 3.55217854929179921
        /// f(x*) = 961.715022289961
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return 1000 - Math.Pow(x.ElementAt(0), 2) - 2*(Math.Pow(x.ElementAt(1), 2)) -
                   (Math.Pow(x.ElementAt(2), 2)) - x.ElementAt(0)*x.ElementAt(1) - x.ElementAt(0)*x.ElementAt(2);
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G15);
            return constraints.GetConstraintViolationG15(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G16 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 705.174537070090537, 68.5999999999999943, 102.899999999999991, 282.324931593660324, 37.5841164258054832
        /// f(x*) = -1.90515525853479
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            var y1 = x.ElementAt(1) + x.ElementAt(2) + 41.6;
            var c1 = 0.024*x.ElementAt(3) - 4.62;
            var y2 = 12.5/c1 + 12;
            var c2 = 0.0003535*Math.Pow(x.ElementAt(0), 2) + 0.5311*x.ElementAt(0) + 0.08705*y2*x.ElementAt(0);
            var c3 = 0.052*x.ElementAt(0) + 78 + 0.002377*y2*x.ElementAt(0);
            var y3 = c2/c3;
            var y4 = 19*y3;
            var c4 = 0.04782*(x.ElementAt(0) - y3) + (0.1956*Math.Pow(x.ElementAt(0) - y3, 2))/x.ElementAt(1) +
                     0.6376*y4 + 1.594*y3;
            var c5 = 100*x.ElementAt(1);
            var c6 = x.ElementAt(0) - y3 - y4;
            var c7 = 0.95 - (c4/c5);
            var y5 = c6*c7;
            var y6 = x.ElementAt(0) - y5 - y4 - y3;
            var c8 = (y5 + y4)*0.995;
            var y7 = c8/y1;
            var y8 = c8/3798;
            var c9 = y7 - (0.0663*y7/y8) - 0.3153;
            var y9 = 96.82/c9 + 0.321*y1;
            var y10 = 1.29*y5 + 1.258*y4 + 2.29*y3 + 1.71*y6;
            var y11 = 1.71*x.ElementAt(0) - 0.452*y4 + 0.58*y3;
            var c10 = 12.3/752.3;
            var c11 = 1.75*y2*0.995*x.ElementAt(0);
            var c12 = 0.995*y10 + 1998;
            var y12 = c10*x.ElementAt(0) + c11/c12;
            var y13 = c12 - 1.75*y2;
            var y14 = 3623 + 64.4*x.ElementAt(1) + 58.4*x.ElementAt(2) + 146312/(y9 + x.ElementAt(4));
            var c13 = 0.995*y10 + 60.8*x.ElementAt(1) + 48*x.ElementAt(3) - 0.1121*y14 - 5095;
            var y15 = y13/c13;
            var y16 = 148000 - 331000*y15 + 40*y13 - 61*y15*y13;
            var c14 = 2324*y10 - 28740000*y2; 
            var y17 = 14130000 - 1328*y10 - 531*y11 + c14/c12;
            var c15 = y13/y15 - y13/0.52;
            var c16 = 1.104 - 0.72*y15;
            var c17 = y9 + x.ElementAt(4);
            return 0.000117*y14 + 0.1365 + 0.00002358*y13 + 0.000001502*y16 + 0.0321*y12 + 0.004324*y5 + 0.0001*c15/c16 +
                   37.48*y2/c12 - 0.0000005843*y17;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G16);
            return constraints.GetConstraintViolationG16(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G17 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 201.784467214523659, 99.9999999999999005, 383.071034852773266, 420, ¡10.9076584514292652, 0.0731482312084287128
        /// f(x*) = 8853.53967480648
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            double f1;
            if (x.ElementAt(0) >= 0 && x.ElementAt(0) < 300)
            {
                f1 = 30 * x.ElementAt(0);
            }
            else if (x.ElementAt(0) >= 300 && x.ElementAt(0) < 400)
            {
                f1 = 31 * x.ElementAt(0);
            }
            else
            {
                throw new Exception();
            }

            double f2;
            if (x.ElementAt(1) >= 0 && x.ElementAt(1) < 100)
            {
                f2 = 28 * x.ElementAt(1);
            }
            else if (x.ElementAt(1) >= 100 && x.ElementAt(1) < 200)
            {
                f2 = 29 * x.ElementAt(1);
            }
            else if (x.ElementAt(1) >= 200 && x.ElementAt(1) < 1000)
            {
                f2 = 30 * x.ElementAt(1);
            }
            else
            {
                throw new Exception();
            }

            return f1 + f2;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G17);
            return constraints.GetConstraintViolationG17(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G18 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = -0.657776192427943163, -0.153418773482438542, 0.323413871675240938, -0.946257611651304398, - 0.657776194376798906, -0.753213434632691414, 0.323413874123576972, -0.346462947962331735, 0.59979466285217542
        /// f(x*) = -0.866025403784439
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return -0.5*
                   (x.ElementAt(0)*x.ElementAt(3) - x.ElementAt(1)*x.ElementAt(2) + x.ElementAt(2)*x.ElementAt(8) -
                    x.ElementAt(4)*x.ElementAt(8) + x.ElementAt(4)*x.ElementAt(7) - x.ElementAt(5)*x.ElementAt(6));
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G18);
            return constraints.GetConstraintViolationG18(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G19 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 1.66991341326291344e-17, 3.95378229282456509e-16, 3.94599045143233784, 1.06036597479721211e-16, 3.2831773458454161, 9.99999999999999822, 1.12829414671605333e - 17, 1.2026194599794709e - 17, 2.50706276000769697e -15, 2.24624122987970677e - 15, 0.370764847417013987, 0.278456024942955571, 0.523838487672241171,0.388620152510322781, 0.298156764974678579
        /// f(x*) = 32.6555929502463
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            var b = new[] {-40, -2, -0.25, -4, -4, -1, -40, -60, 5, 1};
            var e = new int[] {-15, -27, -36, -18, -12};
            var c = new int[,]
            {
                {30, -20, -10, 32, -10}, {-20, 39, -6, -31, 32}, {-10, -6, 10, -6, -10}, {32, -31, -6, 39, -20},
                {-10, 32, -10, -20, 30}
            };
            var d = new int[] {4, 8, 10, 6, 2};
            var a = new double[,]
            {
                {-16, 2, 0, 1, 0}, {0, -2, 0, 0.4, 2}, {-3.5, 0, 2, 0, 0}, {0, -2, 0, -4, -1}, {0, -9, -2, 1, -2.8}, 
                {2, 0, -4, 0, 0}, {-1, -1, -1, -1, -1}, {-1, -2, -3, -2, -1}, {1, 2, 3, 4, 5}, {1, 1, 1, 1, 1}
            };

            var accumulator1 = 0.0;
            for (var j = 0; j < 5; j++)
            {
                for (var i = 0; i < 5; i++)
                {
                    accumulator1 += c[i, j]*x[10 + i]*x[10+j];
                }
            }

            var accumulator2 = 0.0;
            for (var j = 0; j < 5; j++)
            {
                    accumulator2 += d[j]*Math.Pow(x[10 + j], 3);
            }

            var accumulator3 = 0.0;
            for (var i = 0; i < 10; i++)
            {
                    accumulator3 += b[i]*x[i];
            }

            return accumulator1 + 2*accumulator2 - accumulator3;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G19);
            return constraints.GetConstraintViolationG19(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G20 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 1.28582343498528086e - 18, 4.83460302526130664e -34, 0, 0, 6.30459929660781851e - 18, 7.57192526201145068e - 34, 5.03350698372840437e - 34,9.28268079616618064e - 34, 0, 1.76723384525547359e - 17, 3.55686101822965701e - 34,2.99413850083471346e-34, 0.158143376337580827, 2.29601774161699833e-19, 1.06106938611042947e-18, 1.31968344319506391e - 18, 0.530902525044209539, 0, 2.89148310257773535e - 18,3.34892126180666159e-18, 0, 0.310999974151577319, 5.41244666317833561e-05, 4.84993165246959553e-16
        /// f(x*) = 0.2049794002 um pouco inválida
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            var a = new[]
            {
                0.0693, 0.0577, 0.05, 0.2, 0.26, 0.55, 0.06, 0.1, 0.12, 0.18, 0.1, 0.09, 0.0693, 0.0577, 0.05, 0.2, 0.26,
                0.55, 0.06, 0.1, 0.12, 0.18, 0.1, 0.09
            };

            var accumulator = 0.0;
            for (var i = 0; i < 24; i++)
            {
                accumulator += a[i]*x[i];
            }
            return accumulator;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G20);
            return constraints.GetConstraintViolationG20(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G21 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 193.724510070034967, 5.56944131553368433e-27, 17.3191887294084914, 100.047897801386839, 6.68445185362377892, 5.99168428444264833,6.21451648886070451
        /// f(x*) = 193.724510070035
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return x[0];
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G21);
            return constraints.GetConstraintViolationG21(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G22 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 236.430975504001054, 135.82847151732463, 204.818152544824585, 6446.54654059436416,3007540.83940215595, 4074188.65771341929, 32918270.5028952882, 130.075408394314167,170.817294970528621, 299.924591605478554, 399.258113423595205, 330.817294971142758,184.51831230897065, 248.64670239647424, 127.658546694545862, 269.182627528746707,160.000016724090955, 5.29788288102680571, 5.13529735903945728, 5.59531526444068827,5.43444479314453499, 5.07517453535834395
        /// f(x*) = 236.430975504001
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return x[0];
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G22);
            return constraints.GetConstraintViolationG22(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G23 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 0.00510000000000259465, 99.9947000000000514,9.01920162996045897e - 18, 99.9999000000000535, 0.000100000000027086086, 2.75700683389584542e -14, 99.9999999999999574, 200,0.00100000100000100008
        /// f(x*) = -400.055099999999584
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return -9*x[4] - 15*x[7] + 6*x[0] + 16*x[1] + 10*(x[5] + x[6]);
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G23);
            return constraints.GetConstraintViolationG23(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }

    public class G24 : NLFunctionBase
    {
        /// <summary>
        /// Global minimum x* = 2.32952019747762, 3.17849307411774
        /// f(x*) = -5.50801327159536
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public override double Function(double[] x)
        {
            return -x[0] - x[1];
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            var constraints = new NonLinearConstraints(BenchmarksNames.G24);
            return constraints.GetConstraintViolationG24(x);
        }

        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
    }



    #region Funcoes Harold
    
    //Sphere
    public class SphereFunction600 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        public override double Function(double[] x)
        {
            var accumulator = 0.0;
            for (var i = 0; i < x.Length; i++)
            {
                accumulator = accumulator + x.ElementAt(i) * x.ElementAt(i);
            }
            return accumulator;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    //Ackley
    public class AckleyFunction30 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        public override double Function(double[] x)
        {
            var f1 = -20 * Math.Exp(-0.2 * Math.Sqrt(0.1 * MathOperations.SquaredSum(1, 10, x)));
            var f2 = -Math.Exp(0.1 * MathOperations.Cos2PiSum(1, 10, x));
            var f3 = 20 + Math.Exp(1);
            return f1 + f2 + f3;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    //Rastrigin
    public class Rastrigin5Dot12 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

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

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    //Rosenbrock
    public class RosenbrockFunctionMinus9To11 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        public override double Function(double[] x)
        {
            var accumulator = 0.0;
            for (var i = 0; i < x.Length - 1; i++)
            {
                accumulator = accumulator + 100 * (x.ElementAt(i) * x.ElementAt(i) - x.ElementAt(i)) *
                                                  (x.ElementAt(i) * x.ElementAt(i) - x.ElementAt(i)) +
                                                  (x.ElementAt(i) - 1) * (x.ElementAt(i) - 1);
            }
            return accumulator;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }

    //Griewank
    public class GriewankFunction30 : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }

        public override double Function(double[] x)
        {
            var f1 = MathOperations.SquaredSumDividedByNumber(1, 10, x, 4000);
            var f2 = MathOperations.CosProductGriewank(1, 10, x);
            return 1 + f1 - f2;
        }

        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    #endregion

    
    public class PeaksFunction : NLFunctionBase //rever, tem algo de errado
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            var z =
                   3 * (1 - x[0]) * (1 - x[0]) *
                   Math.Exp(-x[0] * x[0]) -
                   (x[1] + 1) * (x[1] + 1) -
                   10 * (x[0] / 5 - x[0] * x[0] * x[0] - x[1] * x[1] * x[1] * x[1] * x[1]) *
                   Math.Exp(-x[0] * x[0] - x[1] * x[1]) -
                   1 / 3 * Math.Exp(-(x[0] + 1) * (x[0] + 1) - x[1] * x[1]);
            return 1 - z;
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    //-------------------------------------------------------------------------
    public class StepFunction : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            return Math.Abs(x[0]) + Math.Abs(x[1]);
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    //-------------------------------------------------------------------------
    public class RosenbockFunction : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            //return 100 * (x * x - y * y) * (x * x - y * y) + (1 - x) * (1 - x);
            return 100 * (x[1] - x[0] * x[0]) * (x[1] - x[0] * x[0]) + (1 - x[0]) * (1 - x[0]);
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    //-------------------------------------------------------------------------
    public class SincFunction : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            var r = Math.Sqrt(x[0] * x[0] + x[1] * x[1]);
            var z = r == 0 ? 1 : Math.Sin(r) / r;

            return 1 - z;
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    //-------------------------------------------------------------------------
    public class ManyPeaksFunction : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            // x, y in [-3,3] -> [0,1]:
            x[0] = (x[0] + 3) / 6d;
            x[1] = (x[1] + 3) / 6d;

            var z = 15 * x[0] * x[1] * (1 - x[0]) * (1 - x[1]) * Math.Sin(9 * Math.PI * x[1]);
            z *= z;

            return 1 - z;
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    //-------------------------------------------------------------------------
    public class HoleFunction : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            // x,y in [-3,3] -> [-5,5]:
            x[0] = (x[0] + 3) / 6d;	// [0,1]
            x[1] = (x[1] + 3) / 6d;	// [0,1]
            x[0] = 10 * x[0] - 5;
            x[1] = 10 * x[1] - 5;

            var z = Math.Cos(x[0]) * Math.Cos(x[1]);
            z *= z * z;
            return 1 - z;
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
        }
    }
    //-------------------------------------------------------------------------
    public class BumpsFunction : NLFunctionBase
    {
        public override double Function(int[] x)
        {
            throw new NotImplementedException();
        }
        public override double Function(double[] x)
        {
            // x,y in [-3,3] -> [-5,5]:
            x[0] = (x[0] + 3) / 6d;	// [0,1]
            x[1] = (x[1] + 3) / 6d;	// [0,1]
            x[0] = 10 * x[0] - 5;
            x[1] = 10 * x[1] - 5;

            var z = Math.Cos(x[0]) + Math.Cos(x[1]);
            z = Math.Abs(z);
            return Math.Sqrt(z);
        }
        public override ConstraintViolationAndVariables ConstraintsViolation(double[] x)
        {
            throw new NotImplementedException();
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

        public static double SquaredSumDividedByNumber(int lowerBound, int upperBound, double[] x, int number)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += Math.Pow(x.ElementAt(index), 2) / 4000;
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
        /// Faz o soma do cosseno do elemento multiplicado por 2Pi
        /// </summary>
        /// <returns></returns>
        public static double Cos2PiSum(int lowerBound, int upperBound, double[] x)
        {
            double sum = 0.0;
            for (var index = lowerBound - 1; index < upperBound; index++)
            {
                sum += Math.Cos(x.ElementAt(index)*2*Math.PI);
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

        public static double CosProductGriewank(int lowerBound, int upperBound, double[] x)
        {
            var product = 1.0;
            for (var index = lowerBound; index < upperBound; index++)
            {
                product *= Math.Cos(x.ElementAt(index) / Math.Sqrt(index));
            }
            return product;
        }
    }
}
