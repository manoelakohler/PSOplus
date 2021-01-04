using System;
using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning;
using PSO.Benchmarks;
using PSO.Benchmarks.NonLinearConstraints;
using PSO.Interfaces;
using PSO.Optimization.Model;
using PSO.ParticlesNlcDouble;

namespace PSO.Optimization.ParticlesNlcDouble
{
    public sealed class OptimizationParticleSwarmNlcDouble : ParticleSwarmNlcDouble
    {
        private readonly NLFunctionBase _function;

        private BenchmarksNames _functionName;

        private const int _maximumNumberOfTries = 50000;
        
        /// <summary>
        /// Constructor
        /// </summary>
        public OptimizationParticleSwarmNlcDouble(NLFunctionBase function, bool isMaximization)
        {
            _function = function;
            _functionName = (BenchmarksNames)Enum.Parse(typeof(BenchmarksNames), _function.GetType().Name, true);
            TendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest;
            TendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest;
            Momentum = PsoConfiguration.InitialInertia;
            UseGlobalOptimum = PsoConfiguration.UseGlobalOptimum;
            Constraints = new NonLinearConstraints(_functionName);
            IsMaximization = isMaximization;

            BestCost = IsMaximization ? double.MinValue : double.MaxValue; 
        }
        
        public void CreateSwarm()
        {
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

        public void InitializeSwarm(int swarmSize)
        {
            // Create the array of particles:
            SearchParticles = new OptimizationParticleNlcDouble[swarmSize];

            for (var i = 0; i < swarmSize; i++)
            {
                var particlePosition = new double[_function.Dimension];
                var particleVelocity = new double[_function.Dimension];

                for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                {
                    if (_functionName == BenchmarksNames.G02)
                    {
                        if (positionIndex%2 == 0) //par
                        {
                            //random initial position
                            particlePosition[positionIndex] = Rnd.NextDouble()*10;
                        }
                        else //impar
                        {
                            //inverse of previous random initial position
                            particlePosition[positionIndex] = 1/particlePosition[positionIndex - 1];
                        }
                        if (positionIndex == _function.Dimension - 1) //se é último (par), multipplica por 0,75
                        {
                            particlePosition[positionIndex] = particlePosition[positionIndex]*0.75;
                        }
                    }
                    else
                    {
                        //random initial position
                        var variable = Rnd.NextDouble();
                        particlePosition[positionIndex] = variable;
                    }
                    //random initial velocity
                    var velocity = Rnd.NextDouble();
                    particleVelocity[positionIndex] = velocity;
                }
                ApplyBounders(particlePosition, particleVelocity);
                IParticle particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
                    _function, Constraints);

                SearchParticles[i] = (OptimizationParticleNlcDouble)particleTry;
                SearchParticles[i].UpdateHistory();
            }
        }

        public void InitializeSwarmCv(int swarmSize)
        {
            // Create the array of particles:
            SearchParticles = new OptimizationParticleNlcDouble[swarmSize];

            for (var i = 0; i < swarmSize; i++)
            {
                var particlePosition = new double[_function.Dimension];
                var particleVelocity = new double[_function.Dimension];

                for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                {
                    //random initial position
                    var variable = Rnd.NextDouble();
                    particlePosition[positionIndex] = variable;

                    //random initial velocity
                    var velocity = Rnd.NextDouble();
                    particleVelocity[positionIndex] = velocity;
                }
                ApplyBounders(particlePosition, particleVelocity);
                IParticle particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
                    _function, Constraints, true);

                SearchParticles[i] = (OptimizationParticleNlcDouble)particleTry;
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
            ReferenceParticles = new OptimizationParticleNlcDouble[swarmSize];

            var tries = 0;
            var notFound = false;
            var currentParticleIndex = 0;

            for (var i = 0; i < swarmSize; i++)
            {
                currentParticleIndex = i;
                var particlePosition = new double[_function.Dimension];
                var particleVelocity = new double[_function.Dimension];
                IParticle particleTry;

                do
                {
                    for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                    {
                        if (_functionName == BenchmarksNames.G02)
                        {
                            if (positionIndex%2 == 0) //par
                            {
                                //random initial position
                                particlePosition[positionIndex] = Rnd.NextDouble()*10;
                            }
                            else //impar
                            {
                                //inverse of previous random initial position
                                particlePosition[positionIndex] = 1/particlePosition[positionIndex - 1];
                            }
                            if (positionIndex == _function.Dimension - 1) //se é último (par), multipplica por 0,75
                            {
                                particlePosition[positionIndex] = particlePosition[positionIndex]*0.75;
                            }
                        }
                        else
                        {
                            //random initial position
                            var variable = Rnd.NextDouble();
                            particlePosition[positionIndex] = variable;
                        }
                        //random initial velocity
                        var velocity = Rnd.NextDouble();
                        particleVelocity[positionIndex] = velocity;
                    }
                    ApplyBounders(particlePosition, particleVelocity);
                    particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
                        _function, Constraints);
                    
                    tries++;

                    //Se atingiu o número máximo de tentativas e já achei pelo menos uma 
                    //solução válida, saio do while e replico a primeira solução.
                    if (tries > _maximumNumberOfTries && i > 0)
                    {
                        notFound = true;
                        break;
                    }

                    //Roda PreSSRT e SSRT
                    if (tries > _maximumNumberOfTries && i == 0 &&
                        _functionName != BenchmarksNames.G02 && _functionName != BenchmarksNames.G03 &&
                        _functionName != BenchmarksNames.G04 && _functionName != BenchmarksNames.G06 &&
                        _functionName != BenchmarksNames.G07 && _functionName != BenchmarksNames.G08 &&
                        _functionName != BenchmarksNames.G09 && _functionName != BenchmarksNames.G10 &&
                        _functionName != BenchmarksNames.G11 && _functionName != BenchmarksNames.G12 &&
                        _functionName != BenchmarksNames.G13 && _functionName != BenchmarksNames.GriewankFunction30 &&
                        _functionName != BenchmarksNames.AckleyFunction30 &&
                        _functionName != BenchmarksNames.Rastrigin5Dot12 &&
                        _functionName != BenchmarksNames.RosenbrockFunctionMinus9To11 &&
                        _functionName != BenchmarksNames.SphereFunction600 && _functionName != BenchmarksNames.G16 && 
                        _functionName != BenchmarksNames.HoleFunction &&
                        _functionName != BenchmarksNames.PeaksFunction &&
                        _functionName != BenchmarksNames.ManyPeaksFunction &&
                        _functionName != BenchmarksNames.StepFunction &&
                        _functionName != BenchmarksNames.RosenbockFunction &&
                        _functionName != BenchmarksNames.SincFunction &&
                        _functionName != BenchmarksNames.BumpsFunction)
                        //aplico operação para tentar reduzir o espaço de busca
                    {
                        var bestParticles = PreSsrt.Run(_function, Rnd); //Pré SSRT
                        
                        if (Constraints.IsFeasible(bestParticles[0]))
                        {
                            particleTry = new OptimizationParticleNlcDouble(this, bestParticles[0].Position.Clone() as double[],
                                particleVelocity.Clone() as double[], _function, Constraints);
                            notFound = false;
                            break;
                        }

                        particleTry = TryToGetFeasibleParticleByUsingSsrt(bestParticles); //SSRT

                        if (Constraints.IsFeasible(SearchParticles[0])) 
                        {
                            particleTry = new OptimizationParticleNlcDouble(this, SearchParticles[0].Position.Clone() as double[],
                                particleVelocity.Clone() as double[], _function, Constraints);
                            notFound = false;
                            break;
                        }
                        if (Constraints.IsFeasible(particleTry)) 
                        {
                            particleTry = new OptimizationParticleNlcDouble(this, particleTry.Position.Clone() as double[],
                                particleVelocity.Clone() as double[], _function, Constraints);
                            notFound = false;
                            break;
                        }
                        notFound = true;
                    }
                    //Serão 50 tentativas de otimização de vilação de restrições e SSRT antes de desistir
                    if (tries > 1.001*_maximumNumberOfTries &&
                        _functionName != BenchmarksNames.G01 &&
                        _functionName != BenchmarksNames.G02 &&
                        _functionName != BenchmarksNames.G03 && 
                        _functionName != BenchmarksNames.G04 &&
                        _functionName != BenchmarksNames.G06 && _functionName != BenchmarksNames.G07 &&
                        _functionName != BenchmarksNames.G08 &&
                        _functionName != BenchmarksNames.G09 && _functionName != BenchmarksNames.G10 &&
                        _functionName != BenchmarksNames.G11 && _functionName != BenchmarksNames.G13 &&
                        _functionName != BenchmarksNames.GriewankFunction30 &&
                        _functionName != BenchmarksNames.AckleyFunction30 &&
                        _functionName != BenchmarksNames.Rastrigin5Dot12 &&
                        _functionName != BenchmarksNames.RosenbrockFunctionMinus9To11 &&
                        _functionName != BenchmarksNames.SphereFunction600 && _functionName != BenchmarksNames.G16)
                    {
                        throw new TimeoutException();
                    }

                } while (!Constraints.IsFeasible(particleTry));

                if (notFound == false)
                {
                    ReferenceParticles[i] = (OptimizationParticleNlcDouble) particleTry;
                    ReferenceParticles[i].UpdateHistory();
                }
                else
                {
                    break;
                }
            }
            if (notFound)
            {
                for (int i = currentParticleIndex++; i < swarmSize; i++)
                {
                    ReferenceParticles[i] = ReferenceParticles[0];
                }
            }
        }
        
        private OptimizationParticleNlcDouble TryToGetFeasibleParticleByUsingSsrt(ParticleNlcDouble[] bestParticles)
        {
            //salva cv
            foreach (var particle in bestParticles)
            {
                var cv = Constraints.GetConstraintViolation(particle.Position);
                particle.ContraintViolation = cv.ConstraintViolation;
                particle.ContraintsTotalCost = cv.TotalViolation;
            }

            //rank
            var sortedSearchParticles = bestParticles.OrderBy(d => d.ContraintViolation).ToArray();

            for (var i = 0; i < 50000; i++)
            {
                //rank
                sortedSearchParticles = sortedSearchParticles.OrderBy(d => d.ContraintsTotalCost).ToArray();

                //todo: nao mudar a lista original aqui
                //ajusta variaveis que estão nas restrições que nao estao sendo satisfeitas
                AdjustBestInfeasibleParticle(sortedSearchParticles[0]);
                
                //atualiza particulas
                UpdateParticles(new[] {sortedSearchParticles[0]});

                if (Constraints.IsFeasible(sortedSearchParticles[0]))
                {
                    return new OptimizationParticleNlcDouble(this, sortedSearchParticles[0].Position.Clone() as double[],
                                new double[sortedSearchParticles[0].Position.Length], _function, Constraints);
                }
                
                var particles = GetBestParticles(sortedSearchParticles);
                var positions = GetBestPositions(particles);

                var particleIndex = 0;
                foreach (var position in positions)
                {
                    var uniqueCentroid = GetCentroids(positions, 1);

                    var infeasibleParticle = new OptimizationParticleNlcDouble(this, position,
                        new double[sortedSearchParticles[0].Position.Length], _function, Constraints);
                    var centroidParticle = new OptimizationParticleNlcDouble(this, uniqueCentroid[0],
                        new double[sortedSearchParticles[0].Position.Length], _function, Constraints);

                    ApproximateParticleToCentroid(centroidParticle, infeasibleParticle);

                    //atualiza particula
                    //sortedSearchParticles[particleIndex].Position = infeasibleParticle.Position; //todo
                    UpdateParticles(sortedSearchParticles);
                    particleIndex++;

                    if (Constraints.IsFeasible(infeasibleParticle))
                    {
                        return infeasibleParticle;
                    }
                    if (Constraints.IsFeasible(centroidParticle))
                    {
                        return centroidParticle;
                    }
                }
            }
            sortedSearchParticles = sortedSearchParticles.OrderBy(d => d.ContraintsTotalCost).ToArray();
            var particleToReturn = new OptimizationParticleNlcDouble(this,
                sortedSearchParticles[0].Position.Clone() as double[],
                new double[sortedSearchParticles[0].Position.Length], _function, Constraints)
            {
                ContraintViolation = sortedSearchParticles[0].ContraintViolation,
                ContraintsTotalCost = sortedSearchParticles[0].ContraintsTotalCost,
                Cost = sortedSearchParticles[0].Cost
            };
            return particleToReturn;
        }

        //private OptimizationParticleNlcDouble TryToGetFeasibleParticleByUsingSsrt(ParticleNlcDouble[] bestParticles)
        //{
        //    //salva cv
        //    foreach (var particle in SearchParticles)
        //    {
        //        var cv = Constraints.GetConstraintViolation(particle.Position);
        //        particle.ContraintViolation = cv.ConstraintViolation;
        //        particle.ContraintsTotalCost = cv.TotalViolation;
        //    }

        //    //rank
        //    var sortedSearchParticles = SearchParticles.OrderBy(d => d.ContraintViolation).ToArray();

        //    for (var i = 0; i < 50000; i++)
        //    {
        //        //rank
        //        sortedSearchParticles = sortedSearchParticles.OrderBy(d => d.ContraintsTotalCost).ToArray();

        //        //ajusta variaveis que estão nas restrições que nao estao sendo satisfeitas
        //        AdjustBestInfeasibleParticle(sortedSearchParticles[0]);

        //        //atualiza particulas
        //        UpdateParticles(new[] { sortedSearchParticles[0] });

        //        if (Constraints.IsFeasible(sortedSearchParticles[0]))
        //        {
        //            return new OptimizationParticleNlcDouble(this, sortedSearchParticles[0].Position.Clone() as double[],
        //                        new double[sortedSearchParticles[0].Position.Length], _function, Constraints);
        //        }

        //        var particles = GetBestParticles(sortedSearchParticles);
        //        var positions = GetBestPositions(particles);

        //        var particleIndex = 0;
        //        foreach (var position in positions)
        //        {
        //            var uniqueCentroid = GetCentroids(positions, 1);

        //            var infeasibleParticle = new OptimizationParticleNlcDouble(this, position,
        //                new double[sortedSearchParticles[0].Position.Length], _function, Constraints);
        //            var centroidParticle = new OptimizationParticleNlcDouble(this, uniqueCentroid[0],
        //                new double[sortedSearchParticles[0].Position.Length], _function, Constraints);

        //            ApproximateParticleToCentroid(centroidParticle, infeasibleParticle);

        //            //atualiza particula
        //            sortedSearchParticles[particleIndex].Position = infeasibleParticle.Position;
        //            UpdateParticles(sortedSearchParticles);
        //            particleIndex++;

        //            if (Constraints.IsFeasible(infeasibleParticle))
        //            {
        //                return infeasibleParticle;
        //            }
        //            if (Constraints.IsFeasible(centroidParticle))
        //            {
        //                return centroidParticle;
        //            }
        //        }
        //    }
        //    return new OptimizationParticleNlcDouble(this, sortedSearchParticles[0].Position.Clone() as double[],
        //                        new double[sortedSearchParticles[0].Position.Length], _function, Constraints);
        //}

        private ParticleNlcDouble[] GetBestParticles(ParticleNlcDouble[] sortedSearchParticles)
        {
            var percentage = (int)(0.2 * sortedSearchParticles.Length);
            var particles = new ParticleNlcDouble[percentage];
            
            for (var i = 0; i < percentage; i++)
            {
                particles[i] = sortedSearchParticles[i];
            }

            return particles;
        }


        private void UpdateParticles(ParticleNlcDouble[] sortedSearchParticles)
        {
            foreach (var particle in sortedSearchParticles)
            {
                particle.ContraintsTotalCost = Constraints.GetConstraintViolation(particle.Position).TotalViolation;
                particle.ContraintViolation = Constraints.GetConstraintViolation(particle.Position).ConstraintViolation;
            }
        }

        private void AdjustBestInfeasibleParticle(ParticleNlcDouble bestInfeasibleParticle)
        {
            var violation = Constraints.GetConstraintViolation(bestInfeasibleParticle.Position);
            var constraintWithMaxViolation = violation.MaxViolationVariables.ToList();

            while (constraintWithMaxViolation.Count > 0 && !Constraints.IsFeasible(bestInfeasibleParticle))
            {
                var randomItemIndex = Rnd.Next(constraintWithMaxViolation.Count);
                var randomVariable = constraintWithMaxViolation[randomItemIndex];
                if (randomVariable == -1)
                {
                    constraintWithMaxViolation.Remove(randomVariable);
                    continue;
                }
                var delta = GetGaussianDistribution(0, 1);

                var minusOrPlus = Rnd.NextDouble();
                if (minusOrPlus < 0.5)
                    bestInfeasibleParticle.Position[randomVariable] += delta;
                else
                    bestInfeasibleParticle.Position[randomVariable] -= delta;

                constraintWithMaxViolation.Remove(randomVariable);
            }
        }
        
        private List<double[]> GetBestPositions(ParticleNlcDouble[] sortedSearchParticles)
        {
            var positionsList = new List<double[]>();
            var percentage = (int)(0.2 * sortedSearchParticles.Length);

            for (var i = 0; i < percentage; i++)
            {
                positionsList.Add(sortedSearchParticles[i].Position);
            }

            return positionsList;
        }

        private void ApproximateParticleToCentroid(OptimizationParticleNlcDouble centroid,
            OptimizationParticleNlcDouble particle)
        {
            var newPosition = new double[particle.Position.Length];

            for (var i = 0; i < particle.Position.Length; i++)
            {
                var randomNumber = Sampler.NextDouble();

                var centroidPosition = centroid.Position.ElementAt(i);
                var infeasiblePosition = particle.Position.ElementAt(i);

                var newPositionValue = randomNumber*centroidPosition +
                                       (1 - randomNumber)*infeasiblePosition;

                newPosition[i] = newPositionValue;

                //Atualiza partícula
                particle.Position[i] = newPosition[i];

                particle.ContraintViolation = Constraints.GetConstraintViolation(particle.Position).ConstraintViolation;
                particle.ContraintsTotalCost = Constraints.GetConstraintViolation(particle.Position).TotalViolation;
                
                if (Constraints.IsFeasible(particle))
                {
                    break;
                }
            }

            //Atualiza partícula
            particle.Position = newPosition;
        }

        private double GetGaussianDistribution(double mean, double stdDev)
        {
            double u1 = 1.0 - Sampler.NextDouble(); //uniform(0,1] random doubles
            double u2 = 1.0 - Sampler.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) *
                         Math.Sin(2.0 * Math.PI * u2); //random normal(0,1)
            double randNormal =
                         mean + stdDev * randStdNormal; //random normal(mean,stdDev^2)

            return randNormal;
        }

        /// <summary>
        /// http://accord-framework.net/docs/html/T_Accord_MachineLearning_KMeans.htm
        /// https://github.com/accord-net/framework/tree/master
        /// https://www.codeproject.com/Articles/985824/Implementing-The-K-Means-Clustering-Algorithm-in-C
        /// </summary>
        private List<double[]> GetCentroids(List<double[]> positions, int numberOfClusters)
        {
            //Accord.Math.Random.Generator.Seed = 0;

            //Cópia por valor
            var observations = new double[positions.Count][];

            for (var i = 0; i < positions.Count; i++)
            {
                observations[i] = new double[positions[i].Length];
                for (var j = 0; j < positions[i].Length; j++)
                {
                    observations[i][j] = positions[i][j];
                }
            }
            
            // Cria Kmeans 
            var kmeans = new KMeans(numberOfClusters);

            // Faz clusterização
            var clusters = kmeans.Learn(observations);

            //Centroides
            var centroids = clusters.Select(cluster => cluster.Centroid).ToList();

            // As a result, the first two observations should belong to the
            //  same cluster (thus having the same label). The same should
            //  happen to the next four observations and to the last three.
            //int[] labels = clusters.Decide(observations);

            return centroids;
        }


        private void InitializeFootHolds(int swarmSize)
        {
            // Create the array of footholds:
            FootHolds = new OptimizationParticleNlcDouble[swarmSize];

            for (var i = 0; i < swarmSize; i++)
            {
                var particlePosition = new double[_function.Dimension];
                var particleVelocity = new double[_function.Dimension];

                for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                {
                    //random initial position
                    var variable = Rnd.NextDouble();
                    particlePosition[positionIndex] = variable;

                    //random initial velocity
                    var velocity = Rnd.NextDouble();
                    particleVelocity[positionIndex] = velocity;
                }
                ApplyBoundersFootHolds(particlePosition, particleVelocity);
                IParticle particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
                    _function, Constraints);

                FootHolds[i] = (OptimizationParticleNlcDouble)particleTry;
                //FootHolds[i].UpdateHistory();
            }
        }

        private void FillInitialPopulation(int swarmSize, ParticleNlcDouble[] particles, bool isSearchParticle)
        {
            for (var i = 0; i < swarmSize; i++)
            {
                var particlePosition = new double[_function.Dimension];
                var particleVelocity = new double[_function.Dimension];
                IParticle particleTry;

                if (isSearchParticle) //não precisa ser partícula válida
                {
                    for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                    {
                        if (_functionName == BenchmarksNames.G02)
                        {
                            if (positionIndex % 2 == 0) //par
                            {
                                //random initial position
                                particlePosition[positionIndex] = Rnd.NextDouble() * 10;
                            }
                            else //impar
                            {
                                //inverse of previous random initial position
                                particlePosition[positionIndex] = 1 / particlePosition[positionIndex - 1];
                            }
                            if (positionIndex == _function.Dimension - 1) //se é último (par), multipplica por 0,75
                            {
                                particlePosition[positionIndex] = particlePosition[positionIndex] * 0.75;
                            }
                        }
                        else
                        {
                            //random initial position
                            var variable = Rnd.NextDouble();
                            particlePosition[positionIndex] = variable;
                        }
                        //random initial velocity
                        var velocity = Rnd.NextDouble();
                        particleVelocity[positionIndex] = velocity;
                    }
                    ApplyBoundersFootHolds(particlePosition, particleVelocity);
                    particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
                        _function, Constraints);
                }
                else //todo: precisa ser válida. depois tentar incluir aqui uma dica se nao der certo
                {
                    do
                    {
                        for (var positionIndex = 0; positionIndex < _function.Dimension; positionIndex++)
                        {
                            if (_functionName == BenchmarksNames.G02)
                            {
                                if (positionIndex % 2 == 0) //par
                                {
                                    //random initial position
                                    particlePosition[positionIndex] = Rnd.NextDouble() * 10;
                                }
                                else //impar
                                {
                                    //inverse of previous random initial position
                                    particlePosition[positionIndex] = 1 / particlePosition[positionIndex - 1];
                                }
                                if (positionIndex == _function.Dimension - 1) //se é último (par), multipplica por 0,75
                                {
                                    particlePosition[positionIndex] = particlePosition[positionIndex] * 0.75;
                                }
                            }
                            else
                            {
                                //random initial position
                                var variable = Rnd.NextDouble();
                                particlePosition[positionIndex] = variable;
                            }
                            //random initial velocity
                            var velocity = Rnd.NextDouble();
                            particleVelocity[positionIndex] = velocity;
                        }
                        ApplyBounders(particlePosition, particleVelocity);
                        particleTry = new OptimizationParticleNlcDouble(this, particlePosition, particleVelocity,
                            _function, Constraints);

                        ////todo: so pra teste
                        //var teste = new double[_function.Dimension];
                        //teste[0] = 590.43822465019218;
                        //teste[1] = 1123.4196139142939;
                        //teste[2] = 0.18354809765403546;
                        //teste[3] = -0.36598792956955173;
                        //particleTry = new OptimizationParticleNlcDouble(this, teste, particleVelocity,
                        //    _function);

                        //var teste = new double[_function.Dimension];
                        //teste[0] = 679.9453;
                        //teste[1] = 1026.067;
                        //teste[2] = 0.1188764;
                        //teste[3] = -0.3962336;
                        //particleTry = new OptimizationParticleNlcDouble(this, teste, particleVelocity,
                        //    _function);

                        ////todo: so pra testeeeeeee


                    } while (!Constraints.IsFeasible(particleTry));
                }

                particles[i] = (OptimizationParticleNlcDouble)particleTry;
                particles[i].UpdateHistory();
                //if (!isSearchParticle)
                //    break;
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

        /// <summary>
        /// Recalcula velocidade e posição da partícula que não obedecer às restrições lineares.
        /// </summary>
        private void RecalculateVelocityAndPosition(IConstraints constraints, IParticle particleTry)
        {
            //Pego uma partícula válida da população de referencia? Faço um crossover aritmético da melhor partícula válida com a partícula inválida
            //Objetivo é tentar aproximar a partícula inválida do espaço de soluções válidas. Refaço o procedimento até que a partícula inválida se torne válida
            var nTrials = 0;
            var isFeasible = false;
            const int maximumNumberOfTrials = 1000;
            var referenceParticle = ReferenceParticles.ElementAt(0); //primeira válida
            var positionsCount = ReferenceParticles.First().Position.Count();
            while (!isFeasible && nTrials < maximumNumberOfTrials)
            {
                var randomNumber = Sampler.NextDouble();
                var newPosition = new double[positionsCount];

                for (var positionIndex = 0; positionIndex < positionsCount; positionIndex++)
                {
                    var feasiblePosition = referenceParticle.Position.ElementAt(positionIndex);
                    var infeasiblePosition = particleTry.Position.ElementAt(positionIndex);

                    var newPositionValue = randomNumber * feasiblePosition +
                                           (1 - randomNumber) * infeasiblePosition;

                    newPosition[positionIndex] = newPositionValue;
                }
                //Atualiza partícula
                particleTry.Position = newPosition;

                //Verifica restrições lineares atualiza isFeasible.
                if (constraints.IsFeasible(particleTry))
                    isFeasible = true;
                nTrials++;
            }

            // se depois de Max tentativas nao encontrar uma partícula válida, copia a original 
            if (nTrials == maximumNumberOfTrials)
            {
                particleTry.Position = ReferenceParticles.ElementAt(0).Position;
            }
        }

        private void ApplyBounders(double[] particlePosition, double[] particleVelocity)
        {
            switch ((BenchmarksNames)Enum.Parse(typeof(BenchmarksNames),_function.GetType().Name, true))
            {
                case BenchmarksNames.G04:
                    G4Bounders(particlePosition);
                    break;
                case BenchmarksNames.G05:
                    G5Bounders(particlePosition);
                    break;
                case BenchmarksNames.G06:
                    G6Bounders(particlePosition);
                    break;
                case BenchmarksNames.G07:
                    G7Bounders(particlePosition);
                    break;
                case BenchmarksNames.G09:
                    G9Bounders(particlePosition);
                    break;
                case BenchmarksNames.G10:
                    G10Bounders(particlePosition);
                    break;
                case BenchmarksNames.G11:
                    G11Bounders(particlePosition);
                    break;
                case BenchmarksNames.G13:
                    G13Bounders(particlePosition, particleVelocity);
                    break;
                case BenchmarksNames.G02:
                    G2Bounders(particlePosition);
                    break;
                case BenchmarksNames.G08:
                    G8Bounders(particlePosition);
                    break;
                case BenchmarksNames.G14:
                    G14Bounders(particlePosition);
                    break;
                case BenchmarksNames.G15:
                    G15Bounders(particlePosition);
                    break;
                case BenchmarksNames.G16:
                    G16Bounders(particlePosition);
                    break;
                case BenchmarksNames.G17:
                    G17Bounders(particlePosition);
                    break;
                case BenchmarksNames.G18:
                    G18Bounders(particlePosition);
                    break;
                case BenchmarksNames.G19:
                    G19Bounders(particlePosition);
                    break;
                case BenchmarksNames.G20:
                    G20Bounders(particlePosition);
                    break;
                case BenchmarksNames.G21:
                    G21Bounders(particlePosition);
                    break;
                case BenchmarksNames.G22:
                    G22Bounders(particlePosition);
                    break;
                case BenchmarksNames.G23:
                    G23Bounders(particlePosition, particleVelocity);
                    break;
                case BenchmarksNames.G24:
                    G24Bounders(particlePosition);
                    break;
            }
        }

        public override void Iteration(int generation)
        {
            // Determine new velocity and position of the particles in the swarm:
            foreach (var particle in SearchParticles)
                particle.UpdateVelocityAndPosition(UseGlobalOptimum ? BestPosition : ReferenceParticles[0].Position, Constraints);

            UpdateReferencePopulation();
            UpdateCostAndHistory();

            //Atualizo os parâmetros dinâmicos: inércia, c1 e c2.
            UpdateDinamicParameters(generation);
        }

        public override void IterationCv(int generation)
        {
            // Determine new velocity and position of the particles in the swarm:
            foreach (var particle in SearchParticles)
                particle.UpdateVelocityAndPositionCv(UseGlobalOptimum ? BestPosition : SearchParticles[0].Position, Constraints);

            UpdateCostAndHistoryCv();

            //Atualizo os parâmetros dinâmicos: inércia, c1 e c2.
            UpdateDinamicParameters(generation);
        }

        private void UpdateReferencePopulation()
        {
            //Copia população de busca para um array auxiliar juntamente com as partículas de referencia
            var auxiliarParticles = new ParticleNlcDouble[SwarmSize * 2];
            var referenceParticlesClone = (ParticleNlcDouble[])ReferenceParticles.Clone();
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
                var clonedParticle = ((OptimizationParticleNlcDouble) particle).Clone();
                auxiliarParticles[index] = (ParticleNlcDouble) clonedParticle;
                index++;
            }
            //Remove repetidos
            //var uniqueParticles = auxiliarParticles.GroupBy(x => x.Position).Select(x => x.First()).ToArray();

            //Ordena a lista de partículas e copia os melhores
            Array.Sort(auxiliarParticles);
            if (IsMaximization)
                Array.Reverse(auxiliarParticles);

            var result = new ParticleNlcDouble[SwarmSize];
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
                                  (PsoConfiguration.NumberOfGenerations * PsoConfiguration.NumberOfRunsWithSteadyState)) * (generation + 1));

            TendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest +
                                   (((PsoConfiguration.FinalAccelerationCoeficientTendencyToGlobalBest -
                                      PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest) /
                                     (PsoConfiguration.NumberOfGenerations * PsoConfiguration.NumberOfRunsWithSteadyState)) * (generation + 1));

            Momentum = PsoConfiguration.InitialInertia +
                       (((PsoConfiguration.FinalInertia - PsoConfiguration.InitialInertia) /
                         (PsoConfiguration.NumberOfGenerations * PsoConfiguration.NumberOfRunsWithSteadyState)) * (generation + 1));

        }
        
        #region Bounders

        /// <summary>
        /// G04
        /// </summary>
        /// <param name="particlePosition"></param>
        private static void G4Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(78.0,102.0);

            particlePosition[1] = GetRandomNumber(33.0, 45.0);

            for (int index = 2; index < 5; index++)
            {
                particlePosition[index] = GetRandomNumber(27.0,45.0);
            }
        }
        
        /// <summary>
        /// G05
        /// </summary>
        /// <param name="particlePosition"></param>
        private void G5Bounders(double[] particlePosition)
        {
            for (int index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(0,1200);
            }
            for (int index = 2; index < 4; index++)
            {
                particlePosition[index] = GetRandomNumber(-0.55,0.55);
            }
        }

        private void G6Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(13, 100);

            particlePosition[1] = GetRandomNumber(0, 100);
        }

        private void G7Bounders(double[] particlePosition)
        {
            for (int index = 0; index < 10; index++)
            {
                particlePosition[index] = GetRandomNumber(-10,10);
            }
        }

        private void G9Bounders(double[] particlePosition)
        {
            for (int index = 0; index < 7; index++)
            {
                particlePosition[index] = GetRandomNumber(-10,10);
            }
        }

        private void G10Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(100,1000);

            for (int index = 1; index < 3; index++)
            {
                particlePosition[index] = GetRandomNumber(1000, 10000);
            }
            for (int index = 3; index < 8; index++)
            {
                particlePosition[index] = GetRandomNumber(10, 1000);
            }
        }

        private void G11Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(-1,1);

            particlePosition[1] = GetRandomNumber(-1,1);
        }

        private void G13Bounders(double[] particlePosition, double[] particleVelocity)
        {
            for (int index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(-2.3, 2.3);
                particleVelocity[index] = GetRandomNumber(-2.3, 2.3);
            }
            for (int index = 2; index < 5; index++)
            {
                particlePosition[index] = GetRandomNumber(-3.2, 3.2);
                particleVelocity[index] = GetRandomNumber(-3.2, 3.2);
            }
        }

        private static void G2Bounders(double[] particlePosition)
        {
            //comentado pq tratamento foi feito no método de geração das partículas.
            //for (var index = 0; index < 20; index++)
            //{
            //    particlePosition[index] = GetRandomNumber(0, 10);
            //}
        }

        private void G8Bounders(double[] particlePosition)
        {
            for (var index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(0, 10);
            }
        }
      

        private static double GetRandomNumber(double minimum, double maximum)
        {
            return Rnd.NextDouble() * (maximum - minimum) + minimum;
        }

        private void G14Bounders(double[] particlePosition)
        {
            for (var index = 0; index < 10; index++)
            {
                particlePosition[index] = GetRandomNumber(0, 10);
            }
        }

        private void G15Bounders(double[] particlePosition)
        {
            for (var index = 0; index < 3; index++)
            {
                particlePosition[index] = GetRandomNumber(0, 10);
            }
        }

        private void G16Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(704.4148, 906.3855);
            particlePosition[1] = GetRandomNumber(68.6, 288.88);
            particlePosition[2] = GetRandomNumber(0, 134.75);
            particlePosition[3] = GetRandomNumber(193, 287.0966);
            particlePosition[4] = GetRandomNumber(25, 84.1988);
        }

        private void G17Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0, 400);
            particlePosition[1] = GetRandomNumber(0, 1000);
            particlePosition[2] = GetRandomNumber(340, 420);
            particlePosition[3] = GetRandomNumber(340, 420);
            particlePosition[4] = GetRandomNumber(-1000, 1000);
            particlePosition[5] = GetRandomNumber(0, 0.5236);

        }

        private void G18Bounders(double[] particlePosition)
        {
            for (var index = 0; index < 8; index++)
            {
                particlePosition[index] = GetRandomNumber(-10, 10);
            }
            particlePosition[8] = GetRandomNumber(0, 20);
        }

        private void G19Bounders(double[] particlePosition)
        {
            for (var index = 0; index < 15; index++)
            {
                particlePosition[index] = GetRandomNumber(0, 10);
            }
        }

        private void G20Bounders(double[] particlePosition)
        {
            for (var index = 0; index < 24; index++)
            {
                particlePosition[index] = GetRandomNumber(0, 1);
            }
        }

        private void G21Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0,1000);
            particlePosition[1] = GetRandomNumber(0,40);
            particlePosition[2] = GetRandomNumber(0,40);
            particlePosition[3] = GetRandomNumber(0,300);
            particlePosition[4] = GetRandomNumber(0,6.7);
            particlePosition[5] = GetRandomNumber(0, 6.4);
            particlePosition[6] = GetRandomNumber(0, 6.25);
        }

        //private void G22Bounders(double[] particlePosition)
        //{
        //    particlePosition[0] = GetRandomNumber(0, 20000);

        //    for (var index = 1; index < 4; index++)
        //    {
        //        particlePosition[index] = GetRandomNumber(0, 10e6);
        //    }
        //    for (var index = 4; index < 7; index++)
        //    {
        //        particlePosition[index] = GetRandomNumber(0, 4*10e7);
        //    }
        //    particlePosition[7] = GetRandomNumber(0, 299.99);
        //    particlePosition[8] = GetRandomNumber(0, 399.99);
        //    particlePosition[9] = GetRandomNumber(0, 300);
        //    particlePosition[10] = GetRandomNumber(0, 400);
        //    particlePosition[11] = GetRandomNumber(0, 600);
        //    for (var index = 12; index < 15; index++)
        //    {
        //        particlePosition[index] = GetRandomNumber(0, 500);
        //    }
        //    particlePosition[15] = GetRandomNumber(0.01, 300);
        //    particlePosition[16] = GetRandomNumber(0.01, 400);

        //    for (var index = 17; index < 22; index++)
        //    {
        //        particlePosition[index] = GetRandomNumber(-4.7, 6.25);
        //    }
        //}

        private void G22Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(200, 300);

            for (var index = 1; index < 3; index++)
            {
                particlePosition[index] = GetRandomNumber(100, 250);
            }
            for (var index = 3; index < 4; index++)
            {
                particlePosition[index] = GetRandomNumber(6000, 7000);
            }
            for (var index = 4; index < 7; index++)
            {
                particlePosition[index] = GetRandomNumber(10e5, 4 * 10e7);
            }
            particlePosition[7] = GetRandomNumber(100, 200);
            particlePosition[8] = GetRandomNumber(100, 200);
            particlePosition[9] = GetRandomNumber(200, 300);
            particlePosition[10] = GetRandomNumber(300, 400);
            particlePosition[11] = GetRandomNumber(300, 400);
            for (var index = 12; index < 15; index++)
            {
                particlePosition[index] = GetRandomNumber(100, 250);
            }
            particlePosition[15] = GetRandomNumber(200, 300);
            particlePosition[16] = GetRandomNumber(100, 200);

            for (var index = 17; index < 22; index++)
            {
                particlePosition[index] = GetRandomNumber(5, 6);
            }
        }

        private void G23Bounders(double[] particlePosition, double[] particleVelocity)
        {
            for (var index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(0, 100);
            }
            particlePosition[2] = GetRandomNumber(0, 100);
            particlePosition[3] = GetRandomNumber(0, 100);
            particlePosition[4] = GetRandomNumber(0, 100);
            particlePosition[5] = GetRandomNumber(0, 300);
            particlePosition[6] = GetRandomNumber(0, 100);
            particlePosition[7] = GetRandomNumber(0, 200);
            particlePosition[8] = GetRandomNumber(0.01, 0.03);
        }

        private void G24Bounders(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0, 3);
            particlePosition[1] = GetRandomNumber(0, 4);
        }

        #endregion
        
        #region BoundersDoubled

        private void ApplyBoundersFootHolds(double[] particlePosition, double[] particleVelocity)
        {
            switch ((BenchmarksNames)Enum.Parse(typeof(BenchmarksNames), _function.GetType().Name, true))
            {
                case BenchmarksNames.G04:
                    G4Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G05:
                    G5Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G06:
                    G6Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G07:
                    G7Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G09:
                    G9Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G10:
                    G10Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G11:
                    G11Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G13:
                    G13Bounders2(particlePosition, particleVelocity);
                    break;
                case BenchmarksNames.G02:
                    G2Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G08:
                    G8Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G14:
                    G14Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G15:
                    G15Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G16:
                    G16Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G17:
                    G17Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G18:
                    G18Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G19:
                    G19Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G20:
                    G20Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G21:
                    G21Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G22:
                    G22Bounders2(particlePosition);
                    break;
                case BenchmarksNames.G23:
                    G23Bounders2(particlePosition, particleVelocity);
                    break;
                case BenchmarksNames.G24:
                    G24Bounders2(particlePosition);
                    break;
            }

        }

        /// <summary>
        /// G04
        /// </summary>
        /// <param name="particlePosition"></param>
        private static void G4Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(78.0 - (78 * PsoConfiguration.FootHoldsMultiplier), 102.0 * PsoConfiguration.FootHoldsMultiplier);

            particlePosition[1] = GetRandomNumber(33.0 - (33 * PsoConfiguration.FootHoldsMultiplier), 45.0 * PsoConfiguration.FootHoldsMultiplier);

            for (int index = 2; index < 5; index++)
            {
                particlePosition[index] = GetRandomNumber(27.0 - (27 * PsoConfiguration.FootHoldsMultiplier), 45.0 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        /// <summary>
        /// G05
        /// </summary>
        /// <param name="particlePosition"></param>
        private void G5Bounders2(double[] particlePosition)
        {
            for (int index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 1200 * PsoConfiguration.FootHoldsMultiplier);
            }
            for (int index = 2; index < 4; index++)
            {
                particlePosition[index] = GetRandomNumber(-0.55 * PsoConfiguration.FootHoldsMultiplier, 0.55 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G6Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(13 - (13 * PsoConfiguration.FootHoldsMultiplier), 100 * PsoConfiguration.FootHoldsMultiplier);

            particlePosition[1] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 100 * PsoConfiguration.FootHoldsMultiplier);
        }

        private void G7Bounders2(double[] particlePosition)
        {
            for (int index = 0; index < 10; index++)
            {
                particlePosition[index] = GetRandomNumber(-10 * PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G9Bounders2(double[] particlePosition)
        {
            for (int index = 0; index < 7; index++)
            {
                particlePosition[index] = GetRandomNumber(-10 * PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G10Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(100 -( 100*PsoConfiguration.FootHoldsMultiplier), 1000 * PsoConfiguration.FootHoldsMultiplier);

            for (int index = 1; index < 3; index++)
            {
                particlePosition[index] = GetRandomNumber(1000 - (1000 * PsoConfiguration.FootHoldsMultiplier), 10000 * PsoConfiguration.FootHoldsMultiplier);
            }
            for (int index = 3; index < 8; index++)
            {
                particlePosition[index] = GetRandomNumber(10 - (10 * PsoConfiguration.FootHoldsMultiplier), 1000 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G11Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(-1 * PsoConfiguration.FootHoldsMultiplier, 1 * PsoConfiguration.FootHoldsMultiplier);

            particlePosition[1] = GetRandomNumber(-1 * PsoConfiguration.FootHoldsMultiplier, 1 * PsoConfiguration.FootHoldsMultiplier);
        }

        private void G13Bounders2(double[] particlePosition, double[] particleVelocity)
        {
            for (int index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(-2.3 * PsoConfiguration.FootHoldsMultiplier, 2.3 * PsoConfiguration.FootHoldsMultiplier);
                //particleVelocity[index] = GetRandomNumber(-2.3 * PsoConfiguration.FootHoldsMultiplier, 2.3 * PsoConfiguration.FootHoldsMultiplier);
            }
            for (int index = 2; index < 5; index++)
            {
                particlePosition[index] = GetRandomNumber(-3.2 * PsoConfiguration.FootHoldsMultiplier, 3.2 * PsoConfiguration.FootHoldsMultiplier);
                //particleVelocity[index] = GetRandomNumber(-3.2 * PsoConfiguration.FootHoldsMultiplier, 3.2 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private static void G2Bounders2(double[] particlePosition)
        {
           for (var index = 0; index < 20; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G8Bounders2(double[] particlePosition)
        {
            for (var index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G14Bounders2(double[] particlePosition)
        {
            for (var index = 0; index < 10; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G15Bounders2(double[] particlePosition)
        {
            for (var index = 0; index < 3; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G16Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(704.4148 - (10 * PsoConfiguration.FootHoldsMultiplier), 906.3855 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[1] = GetRandomNumber(68.6 - (10 * PsoConfiguration.FootHoldsMultiplier), 288.88 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[2] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 134.75 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[3] = GetRandomNumber(193 - (10 * PsoConfiguration.FootHoldsMultiplier), 287.0966 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[4] = GetRandomNumber(25 - (10 * PsoConfiguration.FootHoldsMultiplier), 84.1988 * PsoConfiguration.FootHoldsMultiplier);
        }

        private void G17Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 400 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[1] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 1000 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[2] = GetRandomNumber(340 - (10 * PsoConfiguration.FootHoldsMultiplier), 420 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[3] = GetRandomNumber(340 - (10 * PsoConfiguration.FootHoldsMultiplier), 420 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[4] = GetRandomNumber(-1000 * PsoConfiguration.FootHoldsMultiplier, 1000 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[5] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 0.5236 * PsoConfiguration.FootHoldsMultiplier);

        }

        private void G18Bounders2(double[] particlePosition)
        {
            for (var index = 0; index < 8; index++)
            {
                particlePosition[index] = GetRandomNumber(-10 * PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
            particlePosition[8] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 20 * PsoConfiguration.FootHoldsMultiplier);
        }

        private void G19Bounders2(double[] particlePosition)
        {
            for (var index = 0; index < 15; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G20Bounders2(double[] particlePosition)
        {
            for (var index = 0; index < 24; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G21Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 1000 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[1] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 40 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[2] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 40 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[3] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 300 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[4] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 6.7 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[5] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 6.4 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[6] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 6.25 * PsoConfiguration.FootHoldsMultiplier);
        }

        private void G22Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0, 20000);

            for (var index = 1; index < 4; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 10e6);
            }
            for (var index = 4; index < 7; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 4 * 10e7);
            }
            particlePosition[7] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 299.99 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[8] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 399.99 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[9] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 300 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[10] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 400 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[11] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 600 * PsoConfiguration.FootHoldsMultiplier);
            for (var index = 12; index < 15; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 500 * PsoConfiguration.FootHoldsMultiplier);
            }
            particlePosition[15] = GetRandomNumber(0.01 - PsoConfiguration.FootHoldsMultiplier, 300 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[16] = GetRandomNumber(0.01 - PsoConfiguration.FootHoldsMultiplier, 400 * PsoConfiguration.FootHoldsMultiplier);

            for (var index = 17; index < 22; index++)
            {
                particlePosition[index] = GetRandomNumber(-4.7 * PsoConfiguration.FootHoldsMultiplier, 6.25 * PsoConfiguration.FootHoldsMultiplier);
            }
        }

        private void G23Bounders2(double[] particlePosition, double[] particleVelocity)
        {
            for (var index = 0; index < 2; index++)
            {
                particlePosition[index] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 100 * PsoConfiguration.FootHoldsMultiplier);
            }
            particlePosition[2] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 100 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[3] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 100 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[4] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 100 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[5] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 300 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[6] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 100 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[7] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 200 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[8] = GetRandomNumber(0.01 - PsoConfiguration.FootHoldsMultiplier, 0.03 * PsoConfiguration.FootHoldsMultiplier);
        }

        private void G24Bounders2(double[] particlePosition)
        {
            particlePosition[0] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 3 * PsoConfiguration.FootHoldsMultiplier);
            particlePosition[1] = GetRandomNumber(0 - PsoConfiguration.FootHoldsMultiplier, 4 * PsoConfiguration.FootHoldsMultiplier);
        }


        #endregion
    }
}
