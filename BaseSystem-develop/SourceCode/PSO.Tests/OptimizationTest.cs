using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PSO.Benchmarks;
using PSO.Benchmarks.NoConstraints;
using PSO.Optimization.FunctionMinimizing;

namespace PSO.Tests
{
    [TestClass]
    public class OptimizationTest
    {
        private FunctionBase _function;
        private FunctionMinimizingParticleSwarm _swarm;
        private double[] _bestSolution;
        private List<double[]> _solutions;
        public int SwarmSize = 20;

        [TestMethod]
        public void OptimizeAllBenchmarks()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            //Pega todos os benchmarks. Todas as classes que herdam de FunctionBase
            var availableFunctions = FunctionBase.GetAvailableFunctions();

            var bestSolutions = new StreamWriter(@"D:/bestSolutions.txt");

            //Otimiza cada um dos benchmarks
            foreach (var function in availableFunctions)
            {
                //Inicializa parâmetros
                _solutions = new List<double[]>();
                var oldMinimum = double.MinValue;
                var iteration = 0;
                var countSame = 0;

                _function = FunctionBase.CreateFunction(function);

                _swarm = new FunctionMinimizingParticleSwarm(_function, SwarmSize);
                _bestSolution = _swarm.CurrentBestPosition;
                
                //Código abaixo não funciona no teste ?
//                // Wait for 25 iteration without any improvement:
//#if STOP
//                while (countSame < 25)
//#else
//                while (true)
//#endif
                //Número de iterações sem melhora na funçao de avaliação
               // while (countSame <25)
                while(iteration < 500)
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
                        throw new Exception("Erro durante a otimização da função: " + function.FullName + " durante a iteração " + iteration);
                    }

                }

                //escreve no arquivo as melhores posições e custos
                bestSolutions.WriteLine("Função: " + function.Name);
                bestSolutions.WriteLine("Melhor Posição Encontrada: " + _bestSolution.ElementAt(0) + ", " + _bestSolution.ElementAt(1));
                bestSolutions.WriteLine("Menor Custo Encontrado: " + _swarm.BestCost);

                BenchmarksNames benchmarkName;
                Enum.TryParse(function.Name, out benchmarkName);

                bestSolutions.WriteLine(BenchmarksSolution.GetBestPositionMessage(benchmarkName));
                bestSolutions.WriteLine(BenchmarksSolution.GetBestCostMessage(benchmarkName));

                bestSolutions.WriteLine();
            }
            bestSolutions.Close();
            
        }

        
    }
}
