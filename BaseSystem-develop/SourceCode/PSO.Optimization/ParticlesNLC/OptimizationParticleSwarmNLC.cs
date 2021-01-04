using System;
using System.Linq;
using PSO.Benchmarks;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;
using PSO.Optimization.Model;
using PSO.ParticlesNLC;

namespace PSO.Optimization.ParticlesNLC
{
    public sealed class OptimizationParticleSwarmNLC : ParticleSwarmNLC
    {
        private readonly NLFunctionBase _function;

        private readonly BenchmarksNames _functionName;

        private const int _maximumNumberOfTries = 5000000;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OptimizationParticleSwarmNLC(NLFunctionBase function, bool isMaximization)
        {
            _function = function;
            _functionName = (BenchmarksNames)Enum.Parse(typeof(BenchmarksNames), _function.GetType().Name, true);
            TendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest;
            TendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest;
            Momentum = PsoConfiguration.InitialInertia;
            UseGlobalOptimum = PsoConfiguration.UseGlobalOptimum;
            Constraints = new NonLinearConstraints(_functionName);
            IsMaximization = isMaximization;

            if (IsMaximization)
                BestCost = double.MinValue;
            else
                BestCost = double.MaxValue; 
        }

        public void CreateSwarm()
        {
            // Create the swarm:
            InitializeSwarm(PsoConfiguration.PopulationSize);
            InitializeReferenceParticles(PsoConfiguration.PopulationSize);
            InitializeFootHolds(PsoConfiguration.PopulationSize);

            //Sort according to the cost of each particle:
            SortParticles();
            //Atualiza custo e histórico do enxame
            UpdateCostAndHistory();
        }

        public void CreateNextRunSwarm()
        {
            //salvo a melhor particula da população de referência
            var previousRunBestParticle = ReferenceParticles[0];

            //inicializo as populações novamente
            InitializeSwarm(PsoConfiguration.PopulationSize);
            InitializeReferenceParticles(PsoConfiguration.PopulationSize);
            InitializeFootHolds(PsoConfiguration.PopulationSize);

            //Coloco a melhor partícula da rodada anterior na população nova
            ReferenceParticles[0] = previousRunBestParticle;

            //Sort according to the cost of each particle:
            SortParticles();
            //Atualiza custo e histórico do enxame
            UpdateCostAndHistory();
        }

        /// <summary>
        /// Inicializa o enxame
        /// </summary>
        /// <param name="swarmSize"></param>
        public void InitializeSwarm(int swarmSize)
        {
            // Create the array of particles:
            SearchParticles = new OptimizationParticleNLC[swarmSize];
            for (var i = 0; i < swarmSize; i++)
            {
                var particlePosition = new int[_function.Dimension];
                var particleVelocity = new int[_function.Dimension];
                IParticleNLC particleTry;

                //do
                //{
                    for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                    {
                        //random initial position
                        var variable = Rnd.Next(0, 2);
                        particlePosition[positionIndex] = variable;

                        //random initial velocity
                        var velocity = Rnd.Next(0, 101);
                        particleVelocity[positionIndex] = velocity;
                    }
                    ApplyBounders(particlePosition, particleVelocity);
                    particleTry = new OptimizationParticleNLC(this, particlePosition, particleVelocity, _function, Constraints);

                //} while (!Constraints.IsFeasible(particleTry));

                    SearchParticles[i] = (OptimizationParticleNLC)particleTry;
                    SearchParticles[i].UpdateHistory();
            }
        }
        /// <summary>
        /// Inicializa as partículas de referencia
        /// </summary>
        /// <param name="swarmSize"></param>
        public void InitializeReferenceParticles(int swarmSize)
        {
            // Create the array of particles:
            ReferenceParticles = new OptimizationParticleNLC[swarmSize];

            var tries = 0;
            var notFound = false;
            var currentParticleIndex = 0;

            for (var i = 0; i < swarmSize; i++)
            {
                currentParticleIndex = i;
                var particlePosition = new int[_function.Dimension];
                var particleVelocity = new int[_function.Dimension];
                IParticleNLC particleTry;

                do
                {
                    for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                    {
                        //random initial position
                        var variable = Rnd.Next(0, 2);
                        particlePosition[positionIndex] = variable;

                        //random initial velocity
                        var velocity = Rnd.Next(0, 101);
                        particleVelocity[positionIndex] = velocity;
                    }
                    ApplyBounders(particlePosition, particleVelocity);
                    particleTry = new OptimizationParticleNLC(this, particlePosition, particleVelocity, _function, Constraints);

                    tries++;

                    //Se atingiu o número máximo de tentativas e já achei pelo menos uma 
                    //solução válida, saio do while e replico a primeira solução.
                    if (tries > _maximumNumberOfTries && i > 0)
                    {
                        notFound = true;
                        break;
                    }

                } while (!Constraints.IsFeasible(particleTry));

                if (notFound == false)
                {
                    ReferenceParticles[i] = (OptimizationParticleNLC) particleTry;
                    ReferenceParticles[i].UpdateHistory();
                }
                else
                {
                    break;
                }

                //break;
            }

            if (notFound)
            {
                for (int i = currentParticleIndex++; i < swarmSize; i++)
                {
                    ReferenceParticles[i] = ReferenceParticles[0];
                }
            }


            //gerar outra a partir de uma encontrada - dica gerado na força bruta
            //if (!isSearchParticle)
            //{
            //    for (var i = 1; i < swarmSize; i++)
            //    {
            //        var particlePosition = new double[_function.Dimension];
            //        var particleVelocity = new double[_function.Dimension];
            //        IParticle particleTry;

            //        do
            //        {
            //            for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
            //            {
            //                if (_functionName == BenchmarksNames.G02)
            //                {
            //                    if (positionIndex % 2 == 0) //par
            //                    {
            //                        //random initial position
            //                        particlePosition[positionIndex] = Rnd.NextDouble() * 10;
            //                    }
            //                    else //impar
            //                    {
            //                        //inverse of previous random initial position
            //                        particlePosition[positionIndex] = 1 / particlePosition[positionIndex - 1];
            //                    }
            //                    if (positionIndex == _function.Dimension - 1) //se é último (par), multipplica por 0,75
            //                    {
            //                        particlePosition[positionIndex] = particlePosition[positionIndex] * 0.75;
            //                    }
            //                }
            //                else
            //                {
            //                    //random initial position
            //                    var variable = Rnd.NextDouble();
            //                    particlePosition[positionIndex] = variable;
            //                }
            //                //random initial velocity
            //                var velocity = Rnd.NextDouble();
            //                particleVelocity[positionIndex] = velocity;
            //            }
            //            ApplyBounders(particlePosition, particleVelocity);
            //            particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
            //                _function);

            //            //Verifica validade da partícula
            //            var isFeasible = Constraints.IsFeasible(particleTry);

            //            if (!isFeasible)
            //            {
            //                RecalculateVelocityAndPosition(Constraints, particleTry);
            //            }

            //        } while (!Constraints.IsFeasible(particleTry));

            //        particles[i] = (OptimizationParticleNlcDouble)particleTry;
            //        particles[i].UpdateHistory();
            //    }
            //}
        }

        public void InitializeFootHolds(int swarmSize)
        {
            // Create the array of particles:
            FootHolds = new OptimizationParticleNLC[swarmSize];
            for (var i = 0; i < swarmSize; i++)
            {
                var particlePosition = new int[_function.Dimension];
                var particleVelocity = new int[_function.Dimension];
                IParticleNLC particleTry;

                for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                {
                    //random initial position
                    var variable = Rnd.Next(0, 2);
                    particlePosition[positionIndex] = variable;

                    //random initial velocity
                    var velocity = Rnd.Next(0, 101);
                    particleVelocity[positionIndex] = velocity;
                }
                ApplyBoundersFootHolds(particlePosition, particleVelocity);
                particleTry = new OptimizationParticleNLC(this, particlePosition, particleVelocity, _function, Constraints);

                FootHolds[i] = (OptimizationParticleNLC)particleTry;
                //FoothHolds[i].UpdateHistory();
            }
        }

        ///// <summary>
        ///// Recalcula velocidade e posição da partícula que não obedecer às restrições lineares.
        ///// </summary>
        //private void RecalculateVelocityAndPosition(IConstraints constraints, IParticle particleTry)
        //{
        //    //Pego uma partícula válida da população de referencia? Faço um crossover aritmético da melhor partícula válida com a partícula inválida
        //    //Objetivo é tentar aproximar a partícula inválida do espaço de soluções válidas. Refaço o procedimento até que a partícula inválida se torne válida
        //    var nTrials = 0;
        //    var isFeasible = false;
        //    const int maximumNumberOfTrials = 1000;
        //    var referenceParticle = ReferenceParticles.ElementAt(0); //primeira válida
        //    var positionsCount = ReferenceParticles.First().Position.Count();
        //    while (!isFeasible && nTrials < maximumNumberOfTrials)
        //    {
        //        var randomNumber = Sampler.NextDouble();
        //        var newPosition = new double[positionsCount];

        //        for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
        //        {
        //            var feasiblePosition = referenceParticle.Position.ElementAt(positionIndex);
        //            var infeasiblePosition = particleTry.Position.ElementAt(positionIndex);

        //            var newPositionValue = randomNumber * feasiblePosition +
        //                                   (1 - randomNumber) * infeasiblePosition;

        //            newPosition[positionIndex] = newPositionValue;
        //        }
        //        //Atualiza partícula
        //        particleTry.Position = newPosition;

        //        //Verifica restrições lineares atualiza isFeasible.
        //        if (constraints.IsFeasible(particleTry))
        //            isFeasible = true;
        //        nTrials++;
        //    }

        //    // se depois de Max tentativas nao encontrar uma partícula válida, copia a original 
        //    if (nTrials == maximumNumberOfTrials)
        //    {
        //        particleTry.Position = ReferenceParticles.ElementAt(0).Position;
        //    }
        //}


        private void ApplyBounders(int[] particlePosition, int[] particleVelocity)
        {
            switch (_functionName)
            {
                case BenchmarksNames.G01:
                    G1Bounders(particlePosition);
                    break;
                case BenchmarksNames.G12:
                    G12Bounders(particlePosition, particleVelocity);
                    break;
            }
            
        }

        private void ApplyBoundersFootHolds(int[] particlePosition, int[] particleVelocity)
        {
            switch (_functionName)
            {
                case BenchmarksNames.G01:
                    G1Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G12:
                    G12Bounders2(particlePosition, particleVelocity);
                    break;
            }

        }

        public override void Iteration(int generation)
        {
            //Determine the COnstraints
            var nonLinearConstraints = new NonLinearConstraints(_functionName);
            // Determine new velocity and position of the particles in
            // the swarm:
            foreach (var particle in SearchParticles)
                particle.UpdateVelocityAndPosition(UseGlobalOptimum ? BestPosition : ReferenceParticles[0].Position, nonLinearConstraints);

            // Sort according to fitness:
            SortParticles();
            
            // Update history of the swarm:
            UpdateReferencePopulation();
            UpdateCostAndHistory();

            //Atualizo os parâmetros dinâmicos: inércia, c1 e c2.
            UpdateDinamicParameters(generation);
        }

        private void UpdateReferencePopulation()
        {
            //Copia população de busca para um array auxiliar juntamente com as partículas de referencia
            var auxiliarParticles = new ParticleNLC[SwarmSize * 2];
            var referenceParticlesClone = (ParticleNLC[])ReferenceParticles.Clone();
            referenceParticlesClone.CopyTo(auxiliarParticles, 0);
            
            var index = SwarmSize;
            foreach (var particle in SearchParticles)
            {
                if (!Constraints.IsFeasible(particle))
                {
                    //retira um elemento do array == partícula inválida!
                    auxiliarParticles = auxiliarParticles.Take(auxiliarParticles.Count() - 1).ToArray();
                    continue;
                }
                var clonedParticle = ((OptimizationParticleNLC)particle).Clone();
                auxiliarParticles[index] = (ParticleNLC)clonedParticle;
                index++;
            }
            //Remove repetidos
            //var uniqueParticles = auxiliarParticles.GroupBy(x => x.Position).Select(x => x.First()).ToArray();

            //Ordena a lista de partículas e copia os melhores
            Array.Sort(auxiliarParticles);
            if(IsMaximization)
                Array.Reverse(auxiliarParticles);

            var result = new ParticleNLC[SwarmSize];
            Array.Copy(auxiliarParticles, 0, result, 0, SwarmSize);
            result.CopyTo(ReferenceParticles, 0);
        }

        /// <summary>
        /// Atualizo os parâmetros dinâmicos: inércia, c1 e c2.
        /// </summary>
        /// <param name="generation"></param>
        private void UpdateDinamicParameters(int generation)
        {
            TendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest +
                                (((PsoConfiguration.FinalAccelerationCoeficientTendencyToOwnBest -
                                   PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest) /
                                  PsoConfiguration.NumberOfGenerations) * (generation + 1));

            TendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest +
                                   (((PsoConfiguration.FinalAccelerationCoeficientTendencyToGlobalBest -
                                      PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest) /
                                     PsoConfiguration.NumberOfGenerations) * (generation + 1));

            Momentum = PsoConfiguration.InitialInertia +
                       (((PsoConfiguration.FinalInertia - PsoConfiguration.InitialInertia) /
                         PsoConfiguration.NumberOfGenerations) * (generation + 1));

        }


        #region Bounders

        /// <summary>
        /// G1
        /// </summary>
        /// <param name="particlePosition"></param>
        private static void G1Bounders(int[] particlePosition)
        {
            //0 a 100
            for (int index = 9; index < 12; index++)
            {
                particlePosition[index] = Rnd.Next(0,101);
            }
        }

        private void G12Bounders(int[] particlePosition, int[] particleVelocity)
        {
            for (int index = 0; index < 3; index++)
            {
                particlePosition[index] = Rnd.Next(0, 11);
                particleVelocity[index] = Rnd.Next(0, 11);
            }
        }

        #endregion


        #region BoundersFootHolds

        /// <summary>
        /// G1
        /// </summary>
        /// <param name="particlePosition"></param>
        private static void G1Bounders2(int[] particlePosition)
        {
            //0 a 100
            for (int index = 9; index < 12; index++)
            {
                particlePosition[index] = Rnd.Next(0 - PsoConfiguration.FootHoldsMultiplier, 101 *PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G12Bounders2(int[] particlePosition, int[] particleVelocity)
        {
            for (int index = 0; index < 3; index++)
            {
                particlePosition[index] = Rnd.Next(0 - PsoConfiguration.FootHoldsMultiplier, 11 * PsoConfiguration.FootHoldsMultiplier);
                particleVelocity[index] = Rnd.Next(0 - PsoConfiguration.FootHoldsMultiplier, 11 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        #endregion
    }
}
