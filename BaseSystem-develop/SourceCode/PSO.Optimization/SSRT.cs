using System;
using System.Collections.Generic;
using System.Linq;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Optimization.Model;
using PSO.Optimization.ParticlesNlcDouble;
using PSO.ParticlesNlcDouble;

namespace PSO.Optimization
{
    public class PreSsrt
    {
        private static List<OptimizationParticleNlcDouble> _solutions;

        /// <summary>
        /// Roda uma otimização com pso original para minimizar o custo total das restrições não cumpridas.
        /// </summary>
        /// <param name="function"></param>
        /// <param name="random"></param>
        /// <returns></returns>
        public static OptimizationParticleNlcDouble[] Run(NLFunctionBase function, Random random)
        {
            //Inicializa parâmetros
            var generation = 0;
            const int numberOfGenerations = 100000;
            const int swarmSize = 70;
            const int numberOfRunsWithSteadyState = 5;
            var steadyStatePercentage = (int) (0.2 * swarmSize);
            _solutions = new List<OptimizationParticleNlcDouble>();

            var swarmNlcDouble = new OptimizationParticleSwarmNlcDouble(function, false)
            {
                Sampler = random,
                Dimension = function.Dimension,
                UseFootholds = false
            };

            // Create the swarm:
            swarmNlcDouble.InitializeSwarmCv(swarmSize);
            //Sort according to the cost of each particle:
            swarmNlcDouble.SortParticlesCv();
            //Atualiza custo e histórico do enxame
            swarmNlcDouble.UpdateCostAndHistoryCv();

            for (var run = 1; run <= numberOfRunsWithSteadyState; run++)
            {
                var runGenerations = numberOfGenerations / numberOfRunsWithSteadyState;
                var currentRunMaxGenerations = runGenerations*run;

                if (run > 1)
                {
                    //salvo a melhor particula da população de referência
                    var previousRunBestParticles = new List<ParticleNlcDouble>();
                    for (int i = 0; i < steadyStatePercentage; i++)
                    {
                        previousRunBestParticles.Add(swarmNlcDouble.SearchParticles[i]);
                    }


                    //inicializo as populações novamente
                    swarmNlcDouble.InitializeSwarmCv(swarmSize);

                    //Coloco a melhor partícula da rodada anterior na população nova
                    for (int i = 0; i < steadyStatePercentage; i++)
                    {
                        swarmNlcDouble.SearchParticles[i] = previousRunBestParticles[i];
                    }

                    //Sort according to the cost of each particle:
                    swarmNlcDouble.SortParticlesCv();
                    //Atualiza custo e histórico do enxame
                    swarmNlcDouble.UpdateCostAndHistoryCv();
                }

                while (generation < currentRunMaxGenerations)
                {
                    swarmNlcDouble.IterationCv(generation);
                    generation++;

                    //ordena search particles
                    Array.Sort(swarmNlcDouble.SearchParticles);

                    //salva melhor de cada geração
                    var currentBestParticle = new OptimizationParticleNlcDouble(swarmNlcDouble,
                        swarmNlcDouble.SearchParticles[0].Position.Clone() as double[],
                        swarmNlcDouble.SearchParticles[0].Velocity.Clone() as double[], function,
                        swarmNlcDouble.Constraints, true);
                    _solutions.Add(currentBestParticle);

                    var bestSolutionSoFar = new OptimizationParticleNlcDouble(swarmNlcDouble,
                        swarmNlcDouble.BestPosition.Clone() as double[],
                        swarmNlcDouble.SearchParticles[0].Velocity.Clone() as double[], function,
                        swarmNlcDouble.Constraints, true);
                    if (swarmNlcDouble.Constraints.IsFeasible(bestSolutionSoFar))
                    {
                        return new[] {bestSolutionSoFar};
                    }
                }
            }

            //Ordena solutions
            var orderedSolutions = _solutions.OrderBy(x => x.Cost);
            var bestSolutions = new OptimizationParticleNlcDouble[swarmSize];
            for (var i = 0; i < swarmSize; i++)
            {
                bestSolutions[i] = orderedSolutions.ElementAt(i);
            }

            //ordena search particles
            Array.Sort(bestSolutions);

            //Coloca o custo no local devido e recalcula o custo real da partícula
            foreach (var particle in bestSolutions)
            {
                particle.ContraintsTotalCost = particle.Cost;
                particle.ContraintViolation =
                    swarmNlcDouble.Constraints.GetConstraintViolation(particle.Position).ConstraintViolation;
                particle.Cost = double.MaxValue;
            }

            return bestSolutions;
        }
    }
}
