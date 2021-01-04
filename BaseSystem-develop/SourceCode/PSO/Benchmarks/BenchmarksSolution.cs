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
        SphereFunction600,
        AckleyFunction30,
        Rastrigin5Dot12,
        RosenbrockFunctionMinus9To11,
        GriewankFunction30,
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
        G13,
        G14,
        G15,
        G16,
        G17,
        G18,
        G19,
        G20,
        G21,
        G22,
        G23,
        G24
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

                case BenchmarksNames.SphereFunction600:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.AckleyFunction30:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.Rastrigin5Dot12:
                    return "Melhor Posição: 0, 0";
                case BenchmarksNames.RosenbrockFunctionMinus9To11:
                    return "Melhor Posição: 1, 1";
                case BenchmarksNames.GriewankFunction30:
                    return "Melhor Posição: 1, 1";

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

                case BenchmarksNames.G14:
                    return "Melhor Posição: 0.0406684113216282, 0.147721240492452, 0.783205732104114, 0.00141433931889084, 0.485293636780388, 0.000693183051556082,0.0274052040687766,0.0179509660214818, 0.0373268186859717, 0.0968844604336845";
                case BenchmarksNames.G15:
                    return "Melhor Posição: 3.51212812611795133,0.216987510429556135, 3.55217854929179921";
                case BenchmarksNames.G16:
                    return "Melhor Posição: 705.174537070090537, 68.5999999999999943, 102.899999999999991, 282.324931593660324, 37.5841164258054832";
                case BenchmarksNames.G17:
                    return "Melhor Posição: 201.784467214523659, 99.9999999999999005, 383.071034852773266, 420, ¡10.9076584514292652, 0.0731482312084287128";
                case BenchmarksNames.G18:
                    return "Melhor Posição: -0.657776192427943163, -0.153418773482438542, 0.323413871675240938, -0.946257611651304398, - 0.657776194376798906, -0.753213434632691414, 0.323413874123576972, -0.346462947962331735, 0.59979466285217542";
                case BenchmarksNames.G19:
                    return "Melhor Posição: 1.66991341326291344e-17, 3.95378229282456509e-16, 3.94599045143233784, 1.06036597479721211e-16, 3.2831773458454161, 9.99999999999999822, 1.12829414671605333e - 17, 1.2026194599794709e - 17, 2.50706276000769697e -15, 2.24624122987970677e - 15, 0.370764847417013987, 0.278456024942955571, 0.523838487672241171,0.388620152510322781, 0.298156764974678579";
                case BenchmarksNames.G20:
                    return "Melhor Posição: 1.28582343498528086e-18, 4.83460302526130664e-34, 0, 0, 6.30459929660781851e-18, 7.57192526201145068e-34, 5.03350698372840437e-34,9.28268079616618064e-34, 0, 1.76723384525547359e-17, 3.55686101822965701e-34,2.99413850083471346e-34, 0.158143376337580827, 2.29601774161699833e-19, 1.06106938611042947e-18, 1.31968344319506391e-18, 0.530902525044209539, 0, 2.89148310257773535e-18,3.34892126180666159e-18, 0, 0.310999974151577319, 5.41244666317833561e-05, 4.84993165246959553e-16";
                case BenchmarksNames.G21:
                    return "Melhor Posição: 193.724510070034967, 5.56944131553368433e-27, 17.3191887294084914, 100.047897801386839, 6.68445185362377892, 5.99168428444264833,6.21451648886070451";
                case BenchmarksNames.G22:
                    return "Melhor Posição: 236.430975504001054, 135.82847151732463, 204.818152544824585, 6446.54654059436416,3007540.83940215595, 4074188.65771341929, 32918270.5028952882, 130.075408394314167,170.817294970528621, 299.924591605478554, 399.258113423595205, 330.817294971142758,184.51831230897065, 248.64670239647424, 127.658546694545862, 269.182627528746707,160.000016724090955, 5.29788288102680571, 5.13529735903945728, 5.59531526444068827,5.43444479314453499, 5.07517453535834395";
                case BenchmarksNames.G23:
                    return "Melhor Posição: 0.00510000000000259465, 99.9947000000000514,9.01920162996045897e - 18, 99.9999000000000535, 0.000100000000027086086, 2.75700683389584542e -14, 99.9999999999999574, 200, 0.001";
                case BenchmarksNames.G24:
                    return "Melhor Posição: 2.32952019747762, 3.17849307411774";

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
                    return "Menor Custo: 0.0001";

                case BenchmarksNames.SphereFunction600:
                    return "Menor Custo: 0";
                case BenchmarksNames.AckleyFunction30:
                    return "Menor Custo: 0";
                case BenchmarksNames.Rastrigin5Dot12:
                    return "Menor Custo: 0";
                case BenchmarksNames.RosenbrockFunctionMinus9To11:
                    return "Menor Custo: 0";
                case BenchmarksNames.GriewankFunction30:
                    return "Menor Custo: 0";

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

                case BenchmarksNames.G14:
                    return "Melhor Custo: -47.7648884594915";
                case BenchmarksNames.G15:
                    return "Melhor Custo: 961.715022289961";
                case BenchmarksNames.G16:
                    return "Melhor Custo: -1.90515525853479";
                case BenchmarksNames.G17:
                    return "Melhor Custo: 8853.53967480648";
                case BenchmarksNames.G18:
                    return "Melhor Custo: -0.866025403784439";
                case BenchmarksNames.G19:
                    return "Melhor Custo: 32.6555929502463";
                case BenchmarksNames.G20:
                    return "Melhor Custo: 0.2049794002";
                case BenchmarksNames.G21:
                    return "Melhor Custo: 193.724510070035";
                case BenchmarksNames.G22:
                    return "Melhor Custo: 236.430975504001";
                case BenchmarksNames.G23:
                    return "Melhor Custo: -400.055099999999584";
                case BenchmarksNames.G24:
                    return "Melhor Custo: -5.50801327159536";

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

                case BenchmarksNames.G14:
                    return -47.7648884594915;
                case BenchmarksNames.G15:
                    return 961.715022289961;
                case BenchmarksNames.G16:
                    return -1.90515525853479;
                case BenchmarksNames.G17:
                    return 8853.53967480648;
                case BenchmarksNames.G18:
                    return -0.866025403784439;
                case BenchmarksNames.G19:
                    return 32.6555929502463;
                case BenchmarksNames.G20:
                    return 0.2049794002;
                case BenchmarksNames.G21:
                    return 193.724510070035;
                case BenchmarksNames.G22:
                    return 236.430975504001;
                case BenchmarksNames.G23:
                    return -400.055099999999584;
                case BenchmarksNames.G24:
                    return -5.50801327159536;

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

                case BenchmarksNames.SphereFunction600:
                    return new[] {"0", "0", "0", "0", "0", "0", "0", "0", "0", "0"};
                case BenchmarksNames.AckleyFunction30:
                    return new[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                case BenchmarksNames.Rastrigin5Dot12:
                    return new[] { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" };
                case BenchmarksNames.RosenbrockFunctionMinus9To11:
                    return new[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };
                case BenchmarksNames.GriewankFunction30:
                    return new[] { "1", "1", "1", "1", "1", "1", "1", "1", "1", "1" };

                case BenchmarksNames.G14:
                    return new[] { "0.0406684113216282","0.147721240492452", "0.783205732104114", "0.00141433931889084", "0.485293636780388", "0.000693183051556082","0.0274052040687766","0.0179509660214818", "0.0373268186859717", "0.0968844604336845"};
                case BenchmarksNames.G15:
                    return new[] {"3.51212812611795133","0.216987510429556135","3.55217854929179921"};
                case BenchmarksNames.G16:
                    return new[] {"705.174537070090537","68.5999999999999943","102.899999999999991","282.324931593660324","37.5841164258054832"};
                case BenchmarksNames.G17:
                    return new[] {"201.784467214523659","99.9999999999999005","383.071034852773266","420","-10.9076584514292652","0.0731482312084287128"};
                case BenchmarksNames.G18:
                    return new[] {"-0.657776192427943163","-0.153418773482438542","0.323413871675240938","-0.946257611651304398","- 0.657776194376798906","-0.753213434632691414","0.323413874123576972","-0.346462947962331735","0.59979466285217542"};
                case BenchmarksNames.G19:
                    return new[] {"1.66991341326291344e-17","3.95378229282456509e-16","3.94599045143233784","1.06036597479721211e-16","3.2831773458454161","9.99999999999999822","1.12829414671605333e - 17","1.2026194599794709e - 17","2.50706276000769697e -15","2.24624122987970677e - 15","0.370764847417013987","0.278456024942955571","0.523838487672241171,0.388620152510322781","0.298156764974678579"};
                case BenchmarksNames.G20:
                    return new[] {"1.28582343498528086e-18","4.83460302526130664e-34","0","0","6.30459929660781851e-18","7.57192526201145068e-34","5.03350698372840437e-34,9.28268079616618064e-34","0","1.76723384525547359e-17","3.55686101822965701e-34,2.99413850083471346e-34","0.158143376337580827","2.29601774161699833e-19","1.06106938611042947e-18","1.31968344319506391e-18","0.530902525044209539","0","2.89148310257773535e-18,3.34892126180666159e-18","0","0.310999974151577319","5.41244666317833561e-05","4.84993165246959553e-16"};
                case BenchmarksNames.G21:
                    return new[] {"193.724510070034967","5.56944131553368433e-27","17.3191887294084914","100.047897801386839","6.68445185362377892","5.99168428444264833,6.21451648886070451"};
                case BenchmarksNames.G22:
                    return new[] {"236.430975504001054","135.82847151732463","204.818152544824585","6446.54654059436416,3007540.83940215595","4074188.65771341929","32918270.5028952882","130.075408394314167,170.817294970528621","299.924591605478554","399.258113423595205","330.817294971142758,184.51831230897065","248.64670239647424","127.658546694545862","269.182627528746707,160.000016724090955","5.29788288102680571","5.13529735903945728","5.59531526444068827,5.43444479314453499","5.07517453535834395"};
                case BenchmarksNames.G23:
                    return new[] {"0.00510000000000259465","99.9947000000000514","9.01920162996045897e-18","99.9999000000000535","0.000100000000027086086","2.75700683389584542e-14","99.9999999999999574","200", "0.01"};
                case BenchmarksNames.G24:
                    return new[] {"2.32952019747762","3.17849307411774"};

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
                case BenchmarksNames.G14:
                    return 10;
                case BenchmarksNames.G15:
                    return 3;
                case BenchmarksNames.G16:
                    return 5;
                case BenchmarksNames.G17:
                    return 6;
                case BenchmarksNames.G18:
                    return 9;
                case BenchmarksNames.G19:
                    return 15;
                case BenchmarksNames.G20:
                    return 24;
                case BenchmarksNames.G21:
                    return 7;
                case BenchmarksNames.G22:
                    return 22;
                case BenchmarksNames.G23:
                    return 9;
                case BenchmarksNames.G24:
                    return 2;
                default:
                    return 0;
            }
        }

        public static double[,] GetBounders(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.SphereFunction600:
                    return new double[,]
                    {
                        {-600, 600}, {-600, 600}, {-600, 600}, {-600, 600}, {-600, 600}, {-600, 600}, {-600, 600},
                        {-600, 600}, {-600, 600}, {-600, 600}
                    };
                case BenchmarksNames.AckleyFunction30:
                    return new double[,]
                    {
                        {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30},
                        {-30, 30}
                    };
                case BenchmarksNames.Rastrigin5Dot12:
                    return new double[,]
                    {
                        {-5.12, 5.12}, {-5.12, 5.12}, {-5.12, 5.12}, {-5.12, 5.12}, {-5.12, 5.12}, {-5.12, 5.12},
                        {-5.12, 5.12}, {-5.12, 5.12}, {-5.12, 5.12}, {-5.12, 5.12}
                    };
                    ;
                case BenchmarksNames.RosenbrockFunctionMinus9To11:
                    return new double[,]
                    {{-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}, {-9, 11}};
                    ;
                case BenchmarksNames.GriewankFunction30:
                    return new double[,]
                    {
                        {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30}, {-30, 30},
                        {-30, 30}
                    };
                case BenchmarksNames.G01:
                    return new double[,]
                    {
                        {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 100}, {0, 100},
                        {0, 100}, {0, 100}, {0, 1}
                    };
                case BenchmarksNames.G02:
                    return new double[,]
                    {
                        {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10},
                        {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}
                    };
                case BenchmarksNames.G03:
                    return new double[,]
                    {{0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}};
                case BenchmarksNames.G04:
                    return new double[,] {{78, 102}, {33, 45}, {27, 45}, {27, 45}, {27, 45}};
                case BenchmarksNames.G05:
                    return new double[,] {{0, 1200}, {0, 1200}, {-0.55, 0.55}, {-0.55, 0.55}};
                case BenchmarksNames.G06:
                    return new double[,] {{13, 100}, {0, 100}};
                case BenchmarksNames.G07:
                    return new double[,]
                    {
                        {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10},
                        {-10, 10}
                    };
                case BenchmarksNames.G08:
                    return new double[,] {{0, 10}, {0, 10}};
                case BenchmarksNames.G09:
                    return new double[,] {{-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}};
                case BenchmarksNames.G10:
                    return new double[,]
                    {
                        {100, 10000}, {1000, 10000}, {1000, 10000}, {10, 1000}, {10, 1000}, {10, 1000}, {10, 1000},
                        {10, 1000}
                    };
                case BenchmarksNames.G11:
                    return new double[,] {{-1, 1}, {-1, 1}};
                case BenchmarksNames.G12:
                    return new double[,] {{0, 10}, {0, 10}, {0, 10}};
                case BenchmarksNames.G13:
                    return new double[,] {{-2.3, 2.3}, {-2.3, 2.3}, {-3.2, 3.2}, {-3.2, 3.2}, {-3.2, 3.2}};

                case BenchmarksNames.G14:
                    return new double[,]
                    {{0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}};
                case BenchmarksNames.G15:
                    return new double[,] {{0, 10}, {0, 10}, {0, 10}};
                case BenchmarksNames.G16:
                    return new double[,]
                    {{704.4148, 906.3855}, {68.6, 288.88}, {0, 134.75}, {193, 287.0966}, {25, 84.1988}};
                case BenchmarksNames.G17:
                    return new double[,] {{0, 400}, {0, 1000}, {340, 420}, {340, 420}, {-1000, 1000}, {0, 0.5236}};
                case BenchmarksNames.G18:
                    return new double[,]
                    {{-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {-10, 10}, {0, 20}};
                //case BenchmarksNames.G19:
                //    return new double[,]
                //    {
                //        {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10}, {0, 10},
                //        {0, 10}, {0, 10}, {0, 10}, {0, 10}
                //    };
                case BenchmarksNames.G19:
                    return new double[,]
                    {
                        {0, 0.1}, {0, 0.1}, {0, 4}, {0, 0.1}, {0, 4}, {9, 10}, {0, 0.1}, {0, 0.1}, {0, 0.1}, {0, 0.1}, {0, 0.5},
                        {0, 0.3}, {0, 0.6}, {0, 0.4}, {0, 3}
                    };
                case BenchmarksNames.G20:
                    return new double[,]
                    {
                        {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1},
                        {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1}, {0, 1},
                        {0, 1}, {0, 1}, {0, 1}
                    };
                case BenchmarksNames.G21:
                    return new double[,] {{0, 1000}, {0, 40}, {0, 40}, {100, 300}, {6.3, 6.7}, {5.9, 6.4}, {4.5, 6.25}};
                //case BenchmarksNames.G22:
                //    return new double[,]
                //    {
                //        {0, 20000}, {0, 1e6}, {0, 1e6}, {0, 1e6}, {0, 4e7}, {0, 4e7}, {0, 4e7}, {100, 299.99},
                //        {100, 399.99}, {100.01, 300}, {100, 400}, {100, 600}, {0, 500}, {0, 500}, {0, 500}, {0.01, 300},
                //        {0.01, 400}, {-4.7, 6.25}, {-4.7, 6.25}, {-4.7, 6.25}, {-4.7, 6.25}, {-4.7, 6.25}
                //    };
                case BenchmarksNames.G22:
                    return new double[,]
                    {
                        {200, 300}, {100, 250}, {100, 250}, {6000, 7000}, {10e5, 4e7}, {10e5, 4e7}, {10e5, 4e7}, {100, 200},
                        {100, 200}, {200, 300}, {300, 400}, {300, 400}, {100, 250}, {100, 250}, {100, 250}, {200, 300},
                        {100, 200}, {5, 6}, {5, 6}, {5, 6}, {5, 6}, {5, 6}
                    };
                case BenchmarksNames.G23:
                    return new double[,]
                    {{0, 100}, {0, 300}, {0, 100}, {0, 100}, {0, 100}, {0, 300}, {0, 100}, {0, 200}, {0.01, 0.03}};
                case BenchmarksNames.G24:
                    return new double[,] {{0, 3}, {0, 4}};




                case BenchmarksNames.PeaksFunction:
                case BenchmarksNames.ManyPeaksFunction:
                case BenchmarksNames.StepFunction:
                case BenchmarksNames.RosenbockFunction:
                case BenchmarksNames.SincFunction:
                case BenchmarksNames.BumpsFunction:
                case BenchmarksNames.HoleFunction:
                    return new double[,] {{-3, 3}, {-5, 5}};

                
                    
                default:
                    return new double[,] {{0.0,0.0}};
            }
        }

        public static double GetBounder(BenchmarksNames function)
        {
            switch (function)
            {
                case BenchmarksNames.SphereFunction600:
                    return 600;
                case BenchmarksNames.AckleyFunction30:
                    return 30;
                case BenchmarksNames.Rastrigin5Dot12:
                    return 5.12;
                case BenchmarksNames.RosenbrockFunctionMinus9To11:
                    return 9;
                case BenchmarksNames.GriewankFunction30:
                    return 30;
                default:
                    return 0;
            }
        }

    }
}
