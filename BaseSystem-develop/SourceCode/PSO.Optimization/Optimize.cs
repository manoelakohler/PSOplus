using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using PSO.Benchmarks;
using PSO.Benchmarks.NoConstraints;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Optimization.Model;
using PSO.Optimization.Particles;
using PSO.Optimization.ParticlesLC;
using PSO.Optimization.ParticlesNLC;
using PSO.Optimization.ParticlesNlcDouble;
using PSO.Optimization.Utils;

namespace PSO.Optimization
{
    public class Optimize
    {
        public static FunctionBase _function;
        public static NLFunctionBase _nlFunction;
        private static OptimizationParticleSwarm _swarm;
        private static OptimizationParticleSwarmLC _swarmLC;
        private static OptimizationParticleSwarmNLC _swarmNLC;
        private static OptimizationParticleSwarmNlcDouble _swarmNlcDouble;
        private static double[] _bestSolution;
        private static List<double[]> _solutions;
        private static List<int[]> _solutionsNLC;
        private static int[] _bestSolutionNLC;

        private const string ResultsPartialPath = "bestSolutionsOriginal.txt";
        private const string ResultsPartialPathLinearOptimization = "bestSolutionsWithLinearConstraints.txt";

        private static double[] BestSolutionFromAll { get; set; }
        private static double BestCostFromAll { get; set; }

        //declare a variable to hold the CurrentCulture
        static CultureInfo _oldCi;
        //get the old CurrenCulture and set the new, en-US
        static void SetNewCurrentCulture()
        {
            _oldCi = Thread.CurrentThread.CurrentCulture;
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US");
        }
        //reset Current Culture back to the originale
        static void ResetCurrentCulture()
        {
            Thread.CurrentThread.CurrentCulture = _oldCi;
        }

        static void CreateDirectory(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        #region PSOs antigos salvando em txt
        /// <summary>
        /// Otimização original. PSO sem restrições. OLD TXT
        /// </summary>
        /// <param name="resultsPath"></param>
        public static void StartOptimizationTxt(string resultsPath)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = FunctionBase.GetAvailableFunctions();

            var path = Path.Combine(resultsPath, ResultsPartialPath);
            var bestSolutions = new StreamWriter(path);
            
            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions)
            {
                //Inicializa parâmetros
                _solutions = new List<double[]>();
                var oldMinimum = double.MinValue;
                var iteration = 0;
                var countSame = 0;

                _function = FunctionBase.CreateFunction(function);

                _swarm = new OptimizationParticleSwarm(_function);
                _bestSolution = _swarm.CurrentBestPosition;

                while (iteration < PsoConfiguration.NumberOfGenerations)
                {
                    _swarm.Iteration(iteration);
                    iteration++;
                    var minimum = _swarm.BestCost;

                    oldMinimum = _swarm.VerifyNoImprovement(oldMinimum, minimum, ref countSame);

                    try
                    {
                        var currentSolution = _swarm.CurrentBestPosition;
                        _solutions.Add(currentSolution);
                        _bestSolution = _swarm.BestPosition;
                    }
                    catch
                    {
                        throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                            " durante a iteração " + iteration);
                    }

                }
                
                //escreve no arquivo as melhores posições e custos
                bestSolutions.WriteLine("Função: " + function.Name);
                bestSolutions.WriteLine("Melhor Posição Encontrada: " + _bestSolution.ElementAt(0) + ", " +
                                        _bestSolution.ElementAt(1));
                if (function.Name == "ManyPeaksFunction") //rever
                {
                    var x = _bestSolution.ElementAt(0);
                    var y = _bestSolution.ElementAt(1);
                    var z = 15 * x * y * (1 - x) * (1 - y) * Math.Sin(9 * Math.PI * y);
                    z *= z;
                    z = 1 - z;
                    bestSolutions.WriteLine("Menor Custo Encontrado: " + z);
                }
                else
                {
                    bestSolutions.WriteLine("Menor Custo Encontrado: " + _swarm.BestCost);
                }

                BenchmarksNames benchmarkName;
                Enum.TryParse(function.Name, out benchmarkName);

                bestSolutions.WriteLine(BenchmarksSolution.GetBestPositionMessage(benchmarkName));
                bestSolutions.WriteLine(BenchmarksSolution.GetBestCostMessage(benchmarkName));

                bestSolutions.WriteLine();
            }
            bestSolutions.Close();

            ResetCurrentCulture();
        }

        /// <summary>
        /// Otimização considerando restrições lineares.
        /// No caso, x e y têm que estar no intervalo [-3,3]
        /// </summary>
        /// <param name="resultsPath"></param>
        public static void StartOptimizationWithLinearConstraintsTxt(string resultsPath)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = FunctionBase.GetAvailableFunctions();

            var path = Path.Combine(resultsPath, ResultsPartialPathLinearOptimization);
            var bestSolutions = new StreamWriter(path);

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions)
            {
                //Inicializa parâmetros
                _solutions = new List<double[]>();
                var oldMinimum = double.MinValue;
                var iteration = 0;
                var countSame = 0;

                _function = FunctionBase.CreateFunction(function);

                _swarmLC = new OptimizationParticleSwarmLC(_function) { Sampler = new Random() };
                _bestSolution = _swarmLC.CurrentBestPosition;

                while (iteration < PsoConfiguration.NumberOfGenerations)
                {
                    _swarmLC.Iteration(iteration);
                    iteration++;
                    var minimum = _swarmLC.BestCost;

                    oldMinimum = _swarmLC.VerifyNoImprovement(oldMinimum, minimum, ref countSame);

                    try
                    {
                        var currentSolution = _swarmLC.CurrentBestPosition;
                        _solutions.Add(currentSolution);
                        _bestSolution = _swarmLC.BestPosition;
                    }
                    catch
                    {
                        throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                            " durante a iteração " + iteration);
                    }

                }

                //escreve no arquivo as melhores posições e custos
                bestSolutions.WriteLine("Função: " + function.Name);
                bestSolutions.WriteLine("Melhor Posição Encontrada: " + _bestSolution.ElementAt(0) + ", " +
                                        _bestSolution.ElementAt(1));
                if (function.Name == "ManyPeaksFunction") //rever
                {
                    var x = _bestSolution.ElementAt(0);
                    var y = _bestSolution.ElementAt(1);
                    var z = 15 * x * y * (1 - x) * (1 - y) * Math.Sin(9 * Math.PI * y);
                    z *= z;
                    z = 1 - z;
                    bestSolutions.WriteLine("Menor Custo Encontrado: " + z);
                }
                else
                {
                    bestSolutions.WriteLine("Menor Custo Encontrado: " + _swarmLC.BestCost);
                }

                BenchmarksNames benchmarkName;
                Enum.TryParse(function.Name, out benchmarkName);

                bestSolutions.WriteLine(BenchmarksSolution.GetBestPositionMessage(benchmarkName));
                bestSolutions.WriteLine(BenchmarksSolution.GetBestCostMessage(benchmarkName));

                bestSolutions.WriteLine();
            }
            bestSolutions.Close();

            ResetCurrentCulture();
        }
        #endregion

        /// <summary>
        /// Otimização original. PSO sem restrições. Excel
        /// </summary>
        /// <param name="resultsPath"></param>
        public static void StartOptimization(string resultsPath)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = FunctionBase.GetAvailableFunctions();
            availableFunctions.AddRange(FunctionBase2.GetAvailableFunctions());

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions)
            {
                var pointsPath = Path.Combine(resultsPath, "OriginalPso" + function.Name + ".xls");

                //Inicializa parâmetros
                _solutions = new List<double[]>();
                var oldMinimum = double.MinValue;
                var iteration = 0;
                var countSame = 0;
                BestCostFromAll = Double.MaxValue;

                _function = FunctionBase.CreateFunction(function);
                _swarm = _function == null
                    ? new OptimizationParticleSwarm(FunctionBase2.CreateFunction(function))
                    : new OptimizationParticleSwarm(_function);
                
                _bestSolution = _swarm.CurrentBestPosition;

                var bestMeanWorstSdRefAndSearchAndTimeList =
                    new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                Stopwatch stopWatch = Stopwatch.StartNew();

                while (iteration < PsoConfiguration.NumberOfGenerations)
                {
                    _swarm.Iteration(iteration);
                    iteration++;
                    var minimum = _swarm.BestCost;

                    oldMinimum = _swarm.VerifyNoImprovement(oldMinimum, minimum, ref countSame);

                    try
                    {
                        var currentSolution = _swarm.CurrentBestPosition;
                        _solutions.Add(currentSolution);
                        _bestSolution = _swarm.BestPosition;
                    }
                    catch
                    {
                        throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                            " durante a iteração " + iteration);
                    }

                    var meanByGenerationRef = MathOperations.Sum(1, _swarm.Particles.Length,
                                _swarm.Particles.Select(x => x.Cost).ToArray()) / _swarm.Particles.Length;
                    var meanByGenerationSearch = MathOperations.Sum(1, _swarm.Particles.Length,
                        _swarm.Particles.Select(x => x.Cost).ToArray()) / _swarm.Particles.Length;
                    var sdByGenerationRef = CalculateStdDev(_swarm.Particles.Select(x => x.Cost));
                    var sdByGenerationSearch = CalculateStdDev(_swarm.Particles.Select(x => x.Cost));

                    double bestCost;
                    if (function.Name == "ManyPeaksFunction") //rever
                    {
                        var x = _bestSolution.ElementAt(0);
                        var y = _bestSolution.ElementAt(1);
                        var z = 15 * x * y * (1 - x) * (1 - y) * Math.Sin(9 * Math.PI * y);
                        z *= z;
                        z = 1 - z;
                        bestCost = z;
                    }
                    else
                    {
                        bestCost = _swarm.BestCost;
                    }

                    bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(bestCost,
                        meanByGenerationRef, _swarm.Particles.Last().Cost, sdByGenerationRef,
                        bestCost, meanByGenerationSearch,
                        new Tuple<double, double, double>(_swarm.Particles.Last().Cost, sdByGenerationSearch,
                        stopWatch.Elapsed.TotalSeconds)));
                }

                //escreve no arquivo as melhores posições e custos
                var functionName = "Função: " + function.Name;
                var bestPosition = "Melhor Posição Encontrada: " + _bestSolution.ElementAt(0) + ", " +
                                        _bestSolution.ElementAt(1);

              
                BenchmarksNames benchmarkName;
                Enum.TryParse(function.Name, out benchmarkName);

                var bestFunctionPosition = BenchmarksSolution.GetBestPositionMessage(benchmarkName);
                bestFunctionPosition = bestFunctionPosition.Replace("Melhor Posição: ", "");
                var targetParticle = bestFunctionPosition.Split(',');
                var bestFunctionCostText = BenchmarksSolution.GetBestCostMessage(benchmarkName);
                bestFunctionCostText = bestFunctionCostText.Replace("Menor Custo: ", "");
                var bestFunctionCost = Convert.ToDouble(bestFunctionCostText);
                
                //melhor solução de todas as iterações
                if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 < BestCostFromAll)
                {
                    BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                    BestSolutionFromAll = _bestSolution;
                }
                //pior solução das duas populações
                var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 <
                                bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                    ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                    : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                //desvio padrão
                var listRef = new List<double>();
                var listFront = new List<double>();
                for (var index = 0; index < _swarm.Particles.Length; index++)
                {
                    listFront.Add(_swarm.Particles.ElementAt(index).Cost);
                    listRef.Add(_swarm.Particles.ElementAt(index).Cost);
                }
                var std = new List<double> { CalculateStdDev(listRef), CalculateStdDev(listFront) };

                //tempo computacional
                stopWatch.Stop();
                var timeCost = stopWatch.Elapsed.TotalSeconds;

                //so uma iteração sempre. independente da view. para liberar excel.
                Services.OnlyOneIterationOptimization = true;
                Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, 1, timeCost, targetParticle,
                    bestFunctionCost, BestSolutionFromAll, _bestSolution, worstCost, std, BestCostFromAll);
                bestMeanWorstSdRefAndSearchAndTimeList.Clear();
            }
            ResetCurrentCulture();
        }

        public static void StartOptimizationWithLinearConstraints(string resultsPath)
        {
            StartOptimizationWithLinearConstraintsMe(resultsPath);
            StartOptimizationWithLinearConstraintsMera(resultsPath);
        }

        /// <summary>
        /// Otimização considerando restrições de domínio.
        /// No caso, x e y têm que estar no intervalo [-3,3]
        /// </summary>
        /// <param name="resultsPath"></param>
        public static void StartOptimizationWithLinearConstraintsMera(string resultsPath)
        {
            var random = new Random();
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = FunctionBase.GetAvailableFunctions();
            availableFunctions.AddRange(FunctionBase2.GetAvailableFunctions());

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions)
            {
                var pointsPath = Path.Combine(resultsPath, "PsoDominio(ME+RA)." + function.Name + ".xls");

                //Inicializa parâmetros
                _solutions = new List<double[]>();
                var oldMinimum = double.MinValue;
                var iteration = 0;
                var countSame = 0;
                BestCostFromAll = Double.MaxValue;

                _function = FunctionBase.CreateFunction(function);

                _swarmLC = _function == null
                    ? new OptimizationParticleSwarmLC(FunctionBase2.CreateFunction(function)) { Sampler = random, UseArithmeticReposition = true }
                    : new OptimizationParticleSwarmLC(_function) {Sampler = random, UseArithmeticReposition = true};

                _bestSolution = _swarmLC.CurrentBestPosition;

                var bestMeanWorstSdRefAndSearchAndTimeList = new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                Stopwatch stopWatch = Stopwatch.StartNew();

                while (iteration < PsoConfiguration.NumberOfGenerations)
                {
                    _swarmLC.Iteration(iteration);
                    iteration++;
                    var minimum = _swarmLC.BestCost;

                    oldMinimum = _swarmLC.VerifyNoImprovement(oldMinimum, minimum, ref countSame);

                    try
                    {
                        var currentSolution = _swarmLC.CurrentBestPosition;
                        _solutions.Add(currentSolution);
                        _bestSolution = _swarmLC.BestPosition;
                    }
                    catch
                    {
                        throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                            " durante a iteração " + iteration);
                    }
                    var meanByGenerationRef = MathOperations.Sum(1, _swarmLC.Particles.Length,
                                _swarmLC.Particles.Select(x => x.Cost).ToArray()) / _swarmLC.Particles.Length;
                    var meanByGenerationSearch = MathOperations.Sum(1, _swarmLC.Particles.Length,
                        _swarmLC.Particles.Select(x => x.Cost).ToArray()) / _swarmLC.Particles.Length;
                    var sdByGenerationRef = CalculateStdDev(_swarmLC.Particles.Select(x => x.Cost));
                    var sdByGenerationSearch = CalculateStdDev(_swarmLC.Particles.Select(x => x.Cost));

                    double bestCost;
                    if (function.Name == "ManyPeaksFunction") //rever
                    {
                        var x = _bestSolution.ElementAt(0);
                        var y = _bestSolution.ElementAt(1);
                        var z = 15 * x * y * (1 - x) * (1 - y) * Math.Sin(9 * Math.PI * y);
                        z *= z;
                        z = 1 - z;
                        bestCost = z;
                    }
                    else
                    {
                        bestCost = _swarmLC.BestCost;
                    }

                    bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(bestCost,
                        meanByGenerationRef, _swarmLC.Particles.Last().Cost, sdByGenerationRef,
                        bestCost, meanByGenerationSearch,
                        new Tuple<double, double, double>(_swarmLC.Particles.Last().Cost, sdByGenerationSearch,
                        stopWatch.Elapsed.TotalSeconds)));
                }

                //escreve no arquivo as melhores posições e custos
                var functionName = "Função: " + function.Name;
                var bestPosition = "Melhor Posição Encontrada: " + _bestSolution.ElementAt(0) + ", " +
                                        _bestSolution.ElementAt(1);

                BenchmarksNames benchmarkName;
                Enum.TryParse(function.Name, out benchmarkName);

                var bestFunctionPosition = BenchmarksSolution.GetBestPositionMessage(benchmarkName);
                bestFunctionPosition = bestFunctionPosition.Replace("Melhor Posição: ", "");
                var targetParticle = bestFunctionPosition.Split(',');
                var bestFunctionCostText = BenchmarksSolution.GetBestCostMessage(benchmarkName);
                bestFunctionCostText = bestFunctionCostText.Replace("Menor Custo: ", "");
                var bestFunctionCost = Convert.ToDouble(bestFunctionCostText);

                //melhor solução de todas as iterações
                if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 < BestCostFromAll)
                {
                    BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                    BestSolutionFromAll = _bestSolution;
                }
                //pior solução das duas populações
                var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 <
                                bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                    ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                    : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                //desvio padrão
                var listRef = new List<double>();
                var listFront = new List<double>();
                for (var index = 0; index < _swarmLC.Particles.Length; index++)
                {
                    listFront.Add(_swarmLC.Particles.ElementAt(index).Cost);
                    listRef.Add(_swarmLC.Particles.ElementAt(index).Cost);
                }
                var std = new List<double> { CalculateStdDev(listRef), CalculateStdDev(listFront) };

                //tempo computacional
                stopWatch.Stop();
                var timeCost = stopWatch.Elapsed.TotalSeconds;

                //so uma iteração sempre. independente da view. para liberar excel.
                Services.OnlyOneIterationOptimization = true;
                Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, 1, timeCost, targetParticle,
                    bestFunctionCost, BestSolutionFromAll, _bestSolution, worstCost, std, BestCostFromAll);
                bestMeanWorstSdRefAndSearchAndTimeList.Clear();

            }
            ResetCurrentCulture();
        }

        public static void StartOptimizationWithLinearConstraintsMe(string resultsPath)
        {
            var random = new Random();
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = FunctionBase.GetAvailableFunctions();
            availableFunctions.AddRange(FunctionBase2.GetAvailableFunctions());

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions)
            {
                var pointsPath = Path.Combine(resultsPath, "Pso(ME)." + function.Name + ".xls");

                //Inicializa parâmetros
                _solutions = new List<double[]>();
                var oldMinimum = double.MinValue;
                var iteration = 0;
                var countSame = 0;
                BestCostFromAll = Double.MaxValue;

                _function = FunctionBase.CreateFunction(function);

                _swarmLC = _function == null
                    ? new OptimizationParticleSwarmLC(FunctionBase2.CreateFunction(function)) { Sampler = random, UseArithmeticReposition = true }
                    : new OptimizationParticleSwarmLC(_function) { Sampler = random, UseArithmeticReposition = true };

                _bestSolution = _swarmLC.CurrentBestPosition;

                var bestMeanWorstSdRefAndSearchAndTimeList = new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                Stopwatch stopWatch = Stopwatch.StartNew();

                while (iteration < PsoConfiguration.NumberOfGenerations)
                {
                    _swarmLC.Iteration(iteration);
                    iteration++;
                    var minimum = _swarmLC.BestCost;

                    oldMinimum = _swarmLC.VerifyNoImprovement(oldMinimum, minimum, ref countSame);

                    try
                    {
                        var currentSolution = _swarmLC.CurrentBestPosition;
                        _solutions.Add(currentSolution);
                        _bestSolution = _swarmLC.BestPosition;
                    }
                    catch
                    {
                        throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                            " durante a iteração " + iteration);
                    }
                    var meanByGenerationRef = MathOperations.Sum(1, _swarmLC.Particles.Length,
                                _swarmLC.Particles.Select(x => x.Cost).ToArray()) / _swarmLC.Particles.Length;
                    var meanByGenerationSearch = MathOperations.Sum(1, _swarmLC.Particles.Length,
                        _swarmLC.Particles.Select(x => x.Cost).ToArray()) / _swarmLC.Particles.Length;
                    var sdByGenerationRef = CalculateStdDev(_swarmLC.Particles.Select(x => x.Cost));
                    var sdByGenerationSearch = CalculateStdDev(_swarmLC.Particles.Select(x => x.Cost));

                    double bestCost;
                    if (function.Name == "ManyPeaksFunction") //rever
                    {
                        var x = _bestSolution.ElementAt(0);
                        var y = _bestSolution.ElementAt(1);
                        var z = 15 * x * y * (1 - x) * (1 - y) * Math.Sin(9 * Math.PI * y);
                        z *= z;
                        z = 1 - z;
                        bestCost = z;
                    }
                    else
                    {
                        bestCost = _swarmLC.BestCost;
                    }

                    bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(bestCost,
                        meanByGenerationRef, _swarmLC.Particles.Last().Cost, sdByGenerationRef,
                        bestCost, meanByGenerationSearch,
                        new Tuple<double, double, double>(_swarmLC.Particles.Last().Cost, sdByGenerationSearch,
                        stopWatch.Elapsed.TotalSeconds)));
                }

                //escreve no arquivo as melhores posições e custos
                var functionName = "Função: " + function.Name;
                var bestPosition = "Melhor Posição Encontrada: " + _bestSolution.ElementAt(0) + ", " +
                                        _bestSolution.ElementAt(1);

                BenchmarksNames benchmarkName;
                Enum.TryParse(function.Name, out benchmarkName);

                var bestFunctionPosition = BenchmarksSolution.GetBestPositionMessage(benchmarkName);
                bestFunctionPosition = bestFunctionPosition.Replace("Melhor Posição: ", "");
                var targetParticle = bestFunctionPosition.Split(',');
                var bestFunctionCostText = BenchmarksSolution.GetBestCostMessage(benchmarkName);
                bestFunctionCostText = bestFunctionCostText.Replace("Menor Custo: ", "");
                var bestFunctionCost = Convert.ToDouble(bestFunctionCostText);

                //melhor solução de todas as iterações
                if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 < BestCostFromAll)
                {
                    BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                    BestSolutionFromAll = _bestSolution;
                }
                //pior solução das duas populações
                var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 <
                                bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                    ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                    : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                //desvio padrão
                var listRef = new List<double>();
                var listFront = new List<double>();
                for (var index = 0; index < _swarmLC.Particles.Length; index++)
                {
                    listFront.Add(_swarmLC.Particles.ElementAt(index).Cost);
                    listRef.Add(_swarmLC.Particles.ElementAt(index).Cost);
                }
                var std = new List<double> { CalculateStdDev(listRef), CalculateStdDev(listFront) };

                //tempo computacional
                stopWatch.Stop();
                var timeCost = stopWatch.Elapsed.TotalSeconds;

                //so uma iteração sempre. independente da view. para liberar excel.
                Services.OnlyOneIterationOptimization = true;
                Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, 1, timeCost, targetParticle,
                    bestFunctionCost, BestSolutionFromAll, _bestSolution, worstCost, std, BestCostFromAll);
                bestMeanWorstSdRefAndSearchAndTimeList.Clear();
            }
            ResetCurrentCulture();
        }

        /// <summary>
        /// Otimização considerando restrições não lineares.
        /// Benchmark: 
        /// </summary>
        /// <param name="resultsPath"></param>
        /// <param name="useFootholds"></param>
        public static void StartOptimizationWithNonLinearConstraints(string resultsPath, bool useFootholds)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            var random = new Random();
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = NLFunctionBase.GetAvailableFunctions();
            var pointsPath = Path.Combine(resultsPath, useFootholds ? "BestMeanAndWorstG01.xls" : "BestMeanAndWorstG01NoPa.xls");
            var stopOptimization = false;

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions.Where(x => x.Name == "G01"))
            {
                BestCostFromAll = Double.MaxValue; //minimização

                for (var iteration = 1; iteration <= PsoConfiguration.NumberOfIterations; iteration++)
                {
                    var bestMeanWorstSdRefAndSearchAndTimeList = new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                    Stopwatch stopWatch = Stopwatch.StartNew();
                    
                    //Inicializa parâmetros
                    _solutionsNLC = new List<int[]>();
                    var generation = 0;

                    _nlFunction = NLFunctionBase.CreateFunction(function);

                    _swarmNLC = new OptimizationParticleSwarmNLC(_nlFunction, false)
                    {
                        Sampler = random,
                        Dimension = _nlFunction.Dimension,
                        UseFootholds = useFootholds
                    };
                    // Create the swarm:
                    _swarmNLC.CreateSwarm();

                    _bestSolutionNLC = _swarmNLC.CurrentBestPosition;

                    //reinicialização com Steady State;
                    for (var run = 1; run <= PsoConfiguration.NumberOfRunsWithSteadyState; run++)
                    {
                        var runGenerations = PsoConfiguration.NumberOfGenerations / PsoConfiguration.NumberOfRunsWithSteadyState;
                        var currentRunMaxGenerations = runGenerations*run;

                        if (run > 1)
                            _swarmNLC.CreateNextRunSwarm();

                        while (generation < currentRunMaxGenerations)
                        {
                            _swarmNLC.Iteration(generation);
                            generation++;

                            try
                            {
                                var currentSolution = _swarmNLC.CurrentBestPosition;
                                _solutionsNLC.Add(currentSolution);
                                _bestSolutionNLC = _swarmNLC.BestPosition;
                            }
                            catch
                            {
                                throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                                    " durante a iteração " + generation + " na rodada " + run +
                                                    "na iteração " + iteration);
                            }

                            //ordena search particles
                            Array.Sort(_swarmNLC.SearchParticles);

                            var meanByGenerationRef = MathOperations.Sum(1, _swarmNLC.ReferenceParticles.Length,
                                _swarmNLC.ReferenceParticles.Select(x => x.Cost).ToArray())/ _swarmNLC.ReferenceParticles.Length;
                            var meanByGenerationSearch = MathOperations.Sum(1, _swarmNLC.SearchParticles.Length,
                                _swarmNLC.SearchParticles.Select(x => x.Cost).ToArray()) / _swarmNLC.SearchParticles.Length;
                            var sdByGenerationRef = CalculateStdDev(_swarmNLC.ReferenceParticles.Select(x => x.Cost));
                            var sdByGenerationSearch = CalculateStdDev(_swarmNLC.SearchParticles.Select(x => x.Cost));
                            bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(_swarmNLC.BestCost,
                                meanByGenerationRef, _swarmNLC.ReferenceParticles.Last().Cost, sdByGenerationRef, 
                                _swarmNLC.SearchParticles.First().Cost, meanByGenerationSearch, 
                                new Tuple<double, double, double>(_swarmNLC.SearchParticles.Last().Cost, sdByGenerationSearch,
                                stopWatch.Elapsed.TotalSeconds)));

                            //criterio de parada
                            const double tolerance = 0.00001;
                            if (!(Math.Abs(BenchmarksSolution.GetBestCost(BenchmarksNames.G01) - _swarmNLC.BestCost) <
                                  tolerance)) continue;
                            stopOptimization = true;
                            break;
                        }
                        if (stopOptimization)
                            break;
                    }

                    //melhor solução de todas as iterações
                    if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 < BestCostFromAll)
                    {
                        BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                        BestSolutionFromAll = Array.ConvertAll(_bestSolutionNLC, x => (double) x);
                    }
                    //pior solução das duas populações
                    var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 >
                                    bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                        ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                        : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                    //desvio padrão
                    var listRef = new List<double>();
                    var listFront = new List<double>();
                    for (var index = 0; index < _swarmNLC.SearchParticles.Length; index++)
                    {
                        listFront.Add(_swarmNLC.SearchParticles.ElementAt(index).Cost);
                        listRef.Add(_swarmNLC.ReferenceParticles.ElementAt(index).Cost);
                    }
                    var std = new List<double> {CalculateStdDev(listRef), CalculateStdDev(listFront)};

                    //tempo computacional
                    stopWatch.Stop();
                    var timeCost = stopWatch.Elapsed.TotalSeconds;

                    BenchmarksNames benchmarkName;
                    Enum.TryParse(function.Name, out benchmarkName);
                    var targetPartile = BenchmarksSolution.GetBestPosition(benchmarkName);

                    Services.OnlyOneIterationOptimization = false;
                    Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, iteration, timeCost,
                        targetPartile,
                        BenchmarksSolution.GetBestCost(benchmarkName), BestSolutionFromAll,
                        Array.ConvertAll(_bestSolutionNLC, x => (double)x), worstCost, std, BestCostFromAll);
                    bestMeanWorstSdRefAndSearchAndTimeList.Clear();
                }
            }
            ResetCurrentCulture();
        }

        public static void StartOptimizationWithNonLinearConstraintsG12(string resultsPath, bool useFootholds)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            var random = new Random();
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = NLFunctionBase.GetAvailableFunctions();
            var stopOptimization = false;

            var pointsPath = Path.Combine(resultsPath, useFootholds ? "BestMeanAndWorstG12.xls" : "BestMeanAndWorstG12NoPa.xls");
          
            for (var iteration = 1; iteration <= PsoConfiguration.NumberOfIterations; iteration++)
            {
                BestCostFromAll = Double.MinValue; //maximização

                //Otimiza cada um dos benchmarks
                foreach (var function in availableFunctions.Where(x => x.Name == "G12"))
                {
                    var bestMeanWorstSdRefAndSearchAndTimeList = new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                    var stopWatch = Stopwatch.StartNew();
                    
                    //Inicializa parâmetros
                    _solutionsNLC = new List<int[]>();
                    var generation = 0;

                    _nlFunction = NLFunctionBase.CreateFunction(function);

                    _swarmNLC = new OptimizationParticleSwarmNLC(_nlFunction, true)
                    {
                        Sampler = random,
                        Dimension = _nlFunction.Dimension,
                        UseFootholds = useFootholds
                    };

                    // Create the swarm:
                    _swarmNLC.CreateSwarm();

                    _bestSolutionNLC = _swarmNLC.CurrentBestPosition;

                    //reinicialização com Steady State;
                    for (var run = 1; run <= PsoConfiguration.NumberOfRunsWithSteadyState; run++)
                    {
                        var runGenerations = PsoConfiguration.NumberOfGenerations / PsoConfiguration.NumberOfRunsWithSteadyState;
                        var currentRunMaxGenerations = runGenerations*run;

                        if (run > 1)
                            _swarmNLC.CreateNextRunSwarm();

                        while (generation < currentRunMaxGenerations)
                        {
                            _swarmNLC.Iteration(generation);
                            generation++;

                            try
                            {
                                var currentSolution = _swarmNLC.CurrentBestPosition;
                                _solutionsNLC.Add(currentSolution);
                                _bestSolutionNLC = _swarmNLC.BestPosition;
                            }
                            catch
                            {
                                throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                                    " durante a geração " + generation + " na rodada " + run +
                                                    "na iteração " + iteration);
                            }

                            //ordena search particles
                            Array.Sort(_swarmNLC.SearchParticles);

                            var meanByGenerationRef = MathOperations.Sum(1, _swarmNLC.ReferenceParticles.Length,
                                _swarmNLC.ReferenceParticles.Select(x => x.Cost).ToArray()) / _swarmNLC.ReferenceParticles.Length;
                            var meanByGenerationSearch = MathOperations.Sum(1, _swarmNLC.SearchParticles.Length,
                                _swarmNLC.SearchParticles.Select(x => x.Cost).ToArray()) / _swarmNLC.SearchParticles.Length;
                            var sdByGenerationRef = CalculateStdDev(_swarmNLC.ReferenceParticles.Select(x => x.Cost));
                            var sdByGenerationSearch = CalculateStdDev(_swarmNLC.SearchParticles.Select(x => x.Cost));
                            bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(_swarmNLC.BestCost,
                                meanByGenerationRef, _swarmNLC.ReferenceParticles.Last().Cost, sdByGenerationRef,
                                _swarmNLC.SearchParticles.First().Cost, meanByGenerationSearch,
                                new Tuple<double, double, double>(_swarmNLC.SearchParticles.Last().Cost, sdByGenerationSearch,
                                stopWatch.Elapsed.TotalSeconds)));

                            //criterio de parada
                            const double tolerance = 0.00001;
                            if (!(Math.Abs(BenchmarksSolution.GetBestCost(BenchmarksNames.G12) - _swarmNLC.BestCost) <
                                  tolerance)) continue;
                            stopOptimization = true;
                            break;
                        }
                        if (stopOptimization)
                            break;
                    }
                    //melhor solução de todas as iterações
                    if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 > BestCostFromAll)
                    {
                        BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                        BestSolutionFromAll = Array.ConvertAll(_bestSolutionNLC, x => (double)x);
                    }
                    //pior solução das duas populações
                    var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 <
                                    bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                        ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                        : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                    //desvio padrão
                    var listRef = new List<double>();
                    var listFront = new List<double>();
                    for (var index = 0; index < _swarmNLC.SearchParticles.Length; index++)
                    {
                        listFront.Add(_swarmNLC.SearchParticles.ElementAt(index).Cost);
                        listRef.Add(_swarmNLC.ReferenceParticles.ElementAt(index).Cost);
                    }
                    var std = new List<double> { CalculateStdDev(listRef), CalculateStdDev(listFront) };

                    //tempo computacional
                    stopWatch.Stop();
                    var timeCost = stopWatch.Elapsed.TotalSeconds;

                    BenchmarksNames benchmarkName;
                    Enum.TryParse(function.Name, out benchmarkName);
                    var targetPartile = BenchmarksSolution.GetBestPosition(benchmarkName);

                    Services.OnlyOneIterationOptimization = false;
                    Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, iteration, timeCost,
                        targetPartile,
                        BenchmarksSolution.GetBestCost(benchmarkName), BestSolutionFromAll,
                        Array.ConvertAll(_bestSolutionNLC, x => (double)x), worstCost, std, BestCostFromAll);
                    bestMeanWorstSdRefAndSearchAndTimeList.Clear();
                }
            }
            ResetCurrentCulture();
        }

        public static void StartOptimizationRealMinimization(string resultsPath, bool useFootholds)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            var random = new Random();
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = NLFunctionBase.GetAvailableFunctions();
            var stopOptimization = false;

            //Otimiza cada um dos benchmarks
            foreach (
                var function in
                    availableFunctions.Where(
                        x => //x.Name == "HoleFunction" || x.Name == "PeaksFunction" || x.Name == "ManyPeaksFunction" ||
                             //x.Name == "StepFunction" || x.Name == "RosenbockFunction" || x.Name == "SincFunction" ||
                             //x.Name == "BumpsFunction" || x.Name == "HoleFunction"
                             //x.Name == "G04" || x.Name == "G05" || x.Name == "G06" || 
                             //x.Name == "G07" ||
                             //x.Name == "G09" || 
                             //x.Name == "G10" || 
                             //x.Name == "G11" || x.Name == "G13" ||
                             //x.Name == "GriewankFunction30" || x.Name == "AckleyFunction30" || x.Name == "Rastrigin5Dot12" ||
                             //x.Name == "RosenbrockFunctionMinus9To11" || x.Name == "SphereFunction600"  ||
                             //x.Name == "G14" || 
                             //x.Name == "G15" || 
                             //x.Name == "G16" || x.Name == "G17" || x.Name == "G18" ||
                             x.Name == "G19" ))//|| 
                             //x.Name == "G20" || //x.Name == "G21" || 
                             //x.Name == "G22" || 
                             //x.Name == "G23" ))//|| x.Name == "G24"))
            {

                BestCostFromAll = Double.MaxValue; //minimização
                for (var iteration = 1; iteration <= PsoConfiguration.NumberOfIterations; iteration++)
                {
                    var stopWatch = Stopwatch.StartNew();

                    var bestMeanWorstSdRefAndSearchAndTimeList = new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                    var termination = useFootholds ? ".xls" : "NoPa.xls";
                    var pointsPath = Path.Combine(resultsPath, "BestMeanAndWorst" + function.Name + termination);

                    //Inicializa parâmetros
                    _solutions = new List<double[]>();
                    var generation = 0;
                    
                    _nlFunction = NLFunctionBase.CreateFunction(function);

                    _swarmNlcDouble = new OptimizationParticleSwarmNlcDouble(_nlFunction, false)
                    {
                        Sampler = random,
                        Dimension = _nlFunction.Dimension,
                        UseFootholds = useFootholds
                    };
                    // Create the swarm:
                    try
                    {
                        _swarmNlcDouble.CreateSwarm();
                    }
                    catch (TimeoutException)
                    {
                        break; //não conseguiu gerar a população válida
                    }

                    _bestSolution = _swarmNlcDouble.CurrentBestPosition;

                    //reinicialização com Steady State;
                    for (var run = 1; run <= PsoConfiguration.NumberOfRunsWithSteadyState; run++)
                    {
                        var runGenerations = PsoConfiguration.NumberOfGenerations / PsoConfiguration.NumberOfRunsWithSteadyState;
                        var currentRunMaxGenerations = runGenerations*run;

                        if (run > 1)
                            _swarmNlcDouble.CreateNextRunSwarm();

                        while (generation < currentRunMaxGenerations)
                        {
                            _swarmNlcDouble.Iteration(generation);
                            generation++;

                            try
                            {
                                var currentSolution = _swarmNlcDouble.CurrentBestPosition;
                                _solutions.Add(currentSolution);
                                _bestSolution = _swarmNlcDouble.BestPosition;
                            }
                            catch
                            {
                                throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                                    " durante a geração " + generation + " na rodada " + run + 
                                                    "na iteração " + iteration);
                            }
                            //ordena search particles
                            Array.Sort(_swarmNlcDouble.SearchParticles);

                            var meanByGenerationRef = MathOperations.Sum(1, _swarmNlcDouble.ReferenceParticles.Length,
                                _swarmNlcDouble.ReferenceParticles.Select(x => x.Cost).ToArray()) / _swarmNlcDouble.ReferenceParticles.Length;
                            var meanByGenerationSearch = MathOperations.Sum(1, _swarmNlcDouble.SearchParticles.Length,
                                _swarmNlcDouble.SearchParticles.Select(x => x.Cost).ToArray()) / _swarmNlcDouble.SearchParticles.Length;
                            var sdByGenerationRef = CalculateStdDev(_swarmNlcDouble.ReferenceParticles.Select(x => x.Cost));
                            var sdByGenerationSearch = CalculateStdDev(_swarmNlcDouble.SearchParticles.Select(x => x.Cost));
                            bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(_swarmNlcDouble.BestCost,
                                meanByGenerationRef, _swarmNlcDouble.ReferenceParticles.Last().Cost, sdByGenerationRef,
                                _swarmNlcDouble.SearchParticles.First().Cost, meanByGenerationSearch,
                                new Tuple<double, double, double>(_swarmNlcDouble.SearchParticles.Last().Cost, sdByGenerationSearch,
                                stopWatch.Elapsed.TotalSeconds)));

                            //criterio de parada
                            const double tolerance = 0.00001;
                            var functionEnum = (BenchmarksNames) Enum.Parse(typeof(BenchmarksNames), function.Name);
                            if (!(Math.Abs(BenchmarksSolution.GetBestCost(functionEnum) - _swarmNlcDouble.BestCost) <
                                  tolerance)) continue;
                            stopOptimization = true;
                            break;
                        }
                        if (stopOptimization)
                            break;
                    }
                    
                    //melhor solução de todas as iterações
                    if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 < BestCostFromAll)
                    {
                        BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                        BestSolutionFromAll = _bestSolution;
                    }
                    //pior solução das duas populações
                    var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 <
                                    bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                        ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                        : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                    //desvio padrão
                    var listRef = new List<double>();
                    var listFront = new List<double>();
                    for (var index = 0; index < _swarmNlcDouble.SearchParticles.Length; index++)
                    {
                        listFront.Add(_swarmNlcDouble.SearchParticles.ElementAt(index).Cost);
                        listRef.Add(_swarmNlcDouble.ReferenceParticles.ElementAt(index).Cost);
                    }
                    var std = new List<double> { CalculateStdDev(listRef), CalculateStdDev(listFront) };

                    //tempo computacional
                    stopWatch.Stop();
                    var timeCost = stopWatch.Elapsed.TotalSeconds;

                    BenchmarksNames benchmarkName;
                    Enum.TryParse(function.Name, out benchmarkName);
                    var targetPartile = BenchmarksSolution.GetBestPosition(benchmarkName);

                    Services.OnlyOneIterationOptimization = false;
                    Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, iteration, timeCost, targetPartile,
                        BenchmarksSolution.GetBestCost(benchmarkName), BestSolutionFromAll, _bestSolution, worstCost, std, BestCostFromAll);
                    bestMeanWorstSdRefAndSearchAndTimeList.Clear();
                }
            }
            ResetCurrentCulture();
        }

        public static void StartOptimizationRealMaximization(string resultsPath, bool useFootholds)
        {
            //Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            SetNewCurrentCulture();
            CreateDirectory(resultsPath);
            var random = new Random();
            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = NLFunctionBase.GetAvailableFunctions();
            var stopOptimization = false;

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions.Where(x => x.Name == "G02" ))//|| x.Name == "G03" || x.Name == "G08"))
            {
                BestCostFromAll = Double.MinValue; //maximização
                for (var iteration = 1; iteration <= PsoConfiguration.NumberOfIterations; iteration++)
                {
                    Stopwatch stopWatch = Stopwatch.StartNew();

                    var bestMeanWorstSdRefAndSearchAndTimeList = new List<Tuple<double, double, double, double, double, double, Tuple<double, double, double>>>();
                    var termination = useFootholds ? ".xls" : "NoPa.xls";
                    var pointsPath = Path.Combine(resultsPath, "BestMeanAndWorst" + function.Name + termination);

                    //Inicializa parâmetros
                    _solutions = new List<double[]>();
                    var generation = 0;

                    _nlFunction = NLFunctionBase.CreateFunction(function);
                    _swarmNlcDouble = new OptimizationParticleSwarmNlcDouble(_nlFunction, true)
                    {
                        Sampler = random,
                        Dimension = _nlFunction.Dimension,
                        UseFootholds = useFootholds
                    };
                    // Create the swarm:
                    _swarmNlcDouble.CreateSwarm();

                    _bestSolution = _swarmNlcDouble.CurrentBestPosition;

                    //reinicialização com Steady State;
                    for (var run = 1; run <= PsoConfiguration.NumberOfRunsWithSteadyState; run++)
                    {
                        var runGenerations = PsoConfiguration.NumberOfGenerations / PsoConfiguration.NumberOfRunsWithSteadyState;
                        var currentRunMaxGenerations = runGenerations*run;

                        if (run > 1)
                            _swarmNlcDouble.CreateNextRunSwarm();

                        while (generation < currentRunMaxGenerations)
                        {
                            _swarmNlcDouble.Iteration(generation);
                            generation++;

                            try
                            {
                                var currentSolution = _swarmNlcDouble.CurrentBestPosition;
                                _solutions.Add(currentSolution);
                                _bestSolution = _swarmNlcDouble.BestPosition;
                            }
                            catch
                            {
                                throw new Exception("Erro durante a otimização da função: " + function.FullName +
                                                    " durante a geração " + generation + " na rodada " + run +
                                                    "na iteração " + iteration);
                            }

                            //ordena search particles
                            Array.Sort(_swarmNlcDouble.SearchParticles);

                            var meanByGenerationRef = MathOperations.Sum(1, _swarmNlcDouble.ReferenceParticles.Length,
                                _swarmNlcDouble.ReferenceParticles.Select(x => x.Cost).ToArray()) / _swarmNlcDouble.ReferenceParticles.Length;
                            var meanByGenerationSearch = MathOperations.Sum(1, _swarmNlcDouble.SearchParticles.Length,
                                _swarmNlcDouble.SearchParticles.Select(x => x.Cost).ToArray()) / _swarmNlcDouble.SearchParticles.Length;
                            var sdByGenerationRef = CalculateStdDev(_swarmNlcDouble.ReferenceParticles.Select(x => x.Cost));
                            var sdByGenerationSearch = CalculateStdDev(_swarmNlcDouble.SearchParticles.Select(x => x.Cost));
                            bestMeanWorstSdRefAndSearchAndTimeList.Add(Tuple.Create(_swarmNlcDouble.BestCost,
                                meanByGenerationRef, _swarmNlcDouble.ReferenceParticles.Last().Cost, sdByGenerationRef,
                                _swarmNlcDouble.SearchParticles.First().Cost, meanByGenerationSearch,
                                new Tuple<double, double, double>(_swarmNlcDouble.SearchParticles.Last().Cost, sdByGenerationSearch,
                                stopWatch.Elapsed.TotalSeconds)));

                            //criterio de parada
                            const double tolerance = 0.00001;
                            var functionEnum = (BenchmarksNames)Enum.Parse(typeof(BenchmarksNames), function.Name);
                            if (!(Math.Abs(BenchmarksSolution.GetBestCost(functionEnum) - _swarmNlcDouble.BestCost) <
                                  tolerance)) continue;
                            stopOptimization = true;
                            break;
                        }
                        if (stopOptimization)
                            break;
                    }

                    //melhor solução de todas as iterações
                    if (bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1 > BestCostFromAll)
                    {
                        BestCostFromAll = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item1;
                        BestSolutionFromAll = _bestSolution;
                    }
                    //pior solução das duas populações
                    var worstCost = bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6 >
                                    bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3
                        ? bestMeanWorstSdRefAndSearchAndTimeList.Last().Item6
                        : bestMeanWorstSdRefAndSearchAndTimeList.Last().Item3;

                    //desvio padrão
                    var listRef = new List<double>();
                    var listFront = new List<double>();
                    for (var index = 0; index < _swarmNlcDouble.SearchParticles.Length; index++)
                    {
                        listFront.Add(_swarmNlcDouble.SearchParticles.ElementAt(index).Cost);
                        listRef.Add(_swarmNlcDouble.ReferenceParticles.ElementAt(index).Cost);
                    }
                    var std = new List<double> { CalculateStdDev(listRef), CalculateStdDev(listFront) };

                    //tempo computacional
                    stopWatch.Stop();
                    var timeCost = stopWatch.Elapsed.TotalSeconds;

                    BenchmarksNames benchmarkName;
                    Enum.TryParse(function.Name, out benchmarkName);
                    var targetPartile = BenchmarksSolution.GetBestPosition(benchmarkName);

                    Services.OnlyOneIterationOptimization = false;
                    Services.CreateExcelFile(bestMeanWorstSdRefAndSearchAndTimeList, pointsPath, iteration, timeCost, targetPartile,
                        BenchmarksSolution.GetBestCost(benchmarkName), BestSolutionFromAll, _bestSolution, worstCost, std, BestCostFromAll);
                    bestMeanWorstSdRefAndSearchAndTimeList.Clear();
                }
            }
            ResetCurrentCulture();
        }

        public static void RunForrestRun(string resultsPath)
        {
            //StartOptimization(resultsPath);
            //StartOptimizationWithLinearConstraints(resultsPath);
            
            //StartOptimizationWithNonLinearConstraints(resultsPath, true);
            //StartOptimizationWithNonLinearConstraints(resultsPath, false);
            //
            //StartOptimizationWithNonLinearConstraintsG12(resultsPath, true);
            //StartOptimizationWithNonLinearConstraintsG12(resultsPath, false);
            //
            StartOptimizationRealMaximization(resultsPath, true);
            //StartOptimizationRealMaximization(resultsPath, false);
            //
            StartOptimizationRealMinimization(resultsPath, true);
            //StartOptimizationRealMinimization(resultsPath, false);
        }
        
        private static double CalculateStdDev(IEnumerable<double> values)
        {
            var ret = 0.0;
            if (!values.Any()) return ret;
            var valuesList = values.ToList();
            
            //Compute the Average      
            var avg = valuesList.Average();
            //Perform the Sum of (value-avg)_2_2      
            var sum = valuesList.Sum(d => Math.Pow(d - avg, 2));
            //Put it all together      
            ret = Math.Sqrt(sum / (valuesList.Count));
            return ret;
        }
    }
}
