using System;

namespace PSO.Benchmarks
{
    public enum BenchmarksNames
    {
        SphereFunction,
        PeaksFunction,
        StepFunction,
        RosenbockFunction,
        SincFunction,
        ManyPeaksFunction,
        HoleFunction,
        BumpsFunction,
        G01,
        G02,
        G03,
        G04,
        G05,
        G06,
        G07,
        G08,
        G09,
        G10,
        G11,
        G12,
        G13
    }

    public static class BenchmarksSolution
    {
        public static string GetBestPositionMessage(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.SphereFunction:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.PeaksFunction:
                    return "Melhor Posição: 0.2283, -1.6255";
                case BenchmarksNames.StepFunction:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.RosenbockFunction:
                    return "Melhor Posição: 1, 1";
                case BenchmarksNames.SincFunction:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.ManyPeaksFunction:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.HoleFunction:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.BumpsFunction:
                    return "Melhor Posição: x, y";
                case BenchmarksNames.G01:
                    return "Melhor Posição: 1,1,1,1,1,1,1,1,1,3,3,3,1";
                case BenchmarksNames.G02:
                    return "Melhor Posição: f(x*) = 0.803619";
                case BenchmarksNames.G03:
                    return "Melhor Posição: 0.316227766";
                case BenchmarksNames.G04:
                    return "Melhor Posição: 78, 33, 29.995256025682, 45, 36.775812905788";
                case BenchmarksNames.G05:
                    return "Melhor Posição: 679.9453, 1026.067, 0.1188764, -0.3962336";
                case BenchmarksNames.G06:
                    return "Melhor Posição: 14.095, 0.84296";
                case BenchmarksNames.G07:
                    return "Melhor Posição: 2.171996, 2.363683, 8.773926, 5.095984, 0.9906548, 1.430574, 9.828726, 8.280092, 8.375927";
                case BenchmarksNames.G08:
                    return "Melhor Posição: 1.2279713, 4.2453733";
                case BenchmarksNames.G09:
                    return "Melhor Posição: 2.330499, 1.951372, -0.4775414, 4.365726, -0.6244870, 1.038131, 1.594227";
                case BenchmarksNames.G10:
                    return "Melhor Posição: 579.3167, 1359.943, 5110.071, 182.0174, 295.5985, 217.9799, 286.4162, 395.5979";
                case BenchmarksNames.G11:
                    return "Melhor Posição: +/- 707106, 0.5";
                case BenchmarksNames.G12:
                    return "Melhor Posição: 5, 5, 5";
                case BenchmarksNames.G13:
                    return "Melhor Posição: -1.717143, 1.595709, 1.827247, -0.7636413, -0.763645";
                default:
                    return "Melhor Posição: ";
            }
        }

        public static string GetBestCostMessage(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.SphereFunction:
                    return "Menor Custo: 0";
                case BenchmarksNames.PeaksFunction:
                    return "Menor Custo: -6.5511";
                case BenchmarksNames.StepFunction:
                    return "Menor Custo: 0";
                case BenchmarksNames.RosenbockFunction:
                    return "Menor Custo: 0";
                case BenchmarksNames.SincFunction:
                    return "Menor Custo: 0";
                case BenchmarksNames.ManyPeaksFunction:
                    return "Menor Custo: 1";
                case BenchmarksNames.HoleFunction:
                    return "Menor Custo: 0";
                case BenchmarksNames.BumpsFunction:
                    return "Menor Custo: 0,000x";
                case BenchmarksNames.G01:
                    return "Melhor Custo: -15";
                case BenchmarksNames.G02:
                    return "Melhor Custo: 0.803619";
                case BenchmarksNames.G03:
                    return "Melhor Custo: 1";
                case BenchmarksNames.G04:
                    return "Melhor Custo: -30 665.539";
                case BenchmarksNames.G05:
                    return "Melhor Custo: 5126.4981";
                case BenchmarksNames.G06:
                    return "Melhor Custo: -6961.81388";
                case BenchmarksNames.G07:
                    return "Melhor Custo: 24.3062091";
                case BenchmarksNames.G08:
                    return "Melhor Custo: 0.095825";
                case BenchmarksNames.G09:
                    return "Melhor Custo: 680.6300573";
                case BenchmarksNames.G10:
                    return "Melhor Custo: 7049.3307";
                case BenchmarksNames.G11:
                    return "Melhor Custo: 0.75";
                case BenchmarksNames.G12:
                    return "Melhor Custo: 1";
                case BenchmarksNames.G13:
                    return "Melhor Custo: 0.0539498";
                default:
                    return "";
            }
        }

        public static double GetBestCost(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.G01:
                    return -15;
                case BenchmarksNames.G02:
                    return 0.803619;
                case BenchmarksNames.G03:
                    return 1;
                case BenchmarksNames.G04:
                    return -30665.539;
                case BenchmarksNames.G05:
                    return 5126.4981;
                case BenchmarksNames.G06:
                    return -6961.81388;
                case BenchmarksNames.G07:
                    return 24.3062091;
                case BenchmarksNames.G08:
                    return 0.095825;
                case BenchmarksNames.G09:
                    return 680.6300573;
                case BenchmarksNames.G10:
                    return 7049.3307;
                case BenchmarksNames.G11:
                    return 0.75;
                case BenchmarksNames.G12:
                    return 1;
                case BenchmarksNames.G13:
                    return 0.0539498;
                default:
                    return 0;
            }
        }

        public static string[] GetBestPosition(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.G01:
                    return new[] {"1", "1", "1", "1", "1", "1", "1", "1", "1", "3", "3", "3", "1"};
                case BenchmarksNames.G02:
                    return new[] {"unknown"};
                case BenchmarksNames.G03:
                    return new[] { "0.316227766", "0.316227766", "0.316227766", "0.316227766", "0.316227766", "0.316227766", "0.316227766", "0.316227766", "0.316227766", "0.316227766"};
                case BenchmarksNames.G04:
                    return new[] {"78", "33", "29.995256025682", "45", "36.775812905788"};
                case BenchmarksNames.G05:
                    return new[] {"679.9453", "1026.067", "0.1188764", "-0.3962336"};
                case BenchmarksNames.G06:
                    return new[] {"14.095", "0.84296"};
                case BenchmarksNames.G07:
                    return new[] { "2.171996", "2.363683", "8.773926", "5.095984", "0.9906548", "1.430574", "1.321644", "9.828726", "8.280092", "8.375927" };
                case BenchmarksNames.G08:
                    return new[] {"1.2279713", "4.2453733"};
                case BenchmarksNames.G09:
                    return new[] {"2.330499", "1.951372", "-0.4775414", "4.365726", "-0.6244870", "1.038131", "1.594227"};
                case BenchmarksNames.G10:
                    return new[] {"579.3167", "1359.943", "5110.071", "182.0174", "295.5985", "217.9799", "286.4162", "395.5979"};
                case BenchmarksNames.G11:
                    return new[] {"0.707106", "0.5"};
                case BenchmarksNames.G12:
                    return new[] {"5", "5", "5"};
                case BenchmarksNames.G13:
                    return new[] {"-1.717143", "1.595709", "1.827247", "-0.7636413", "-0.763645"};
                default:
                    return new[] {"error"};
            }
        }

        public static int GetBenchmarkDimension(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.G01:
                    return 13;
                case BenchmarksNames.G02:
                    return 20;
                case BenchmarksNames.G03:
                    return 10;
                case BenchmarksNames.G04:
                    return 5;
                case BenchmarksNames.G05:
                    return 4;
                case BenchmarksNames.G06:
                    return 2;
                case BenchmarksNames.G07:
                    return 10;
                case BenchmarksNames.G08:
                    return 2;
                case BenchmarksNames.G09:
                    return 7;
                case BenchmarksNames.G10:
                    return 8;
                case BenchmarksNames.G11:
                    return 2;
                case BenchmarksNames.G12:
                    return 3;
                case BenchmarksNames.G13:
                    return 5;
                default:
                    return 0;
            }
        }
    }
}
