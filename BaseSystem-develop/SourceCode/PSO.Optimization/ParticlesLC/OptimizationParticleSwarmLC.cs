using System;
using PSO.Benchmarks;
using PSO.Benchmarks.LinearConstraints;
using PSO.Benchmarks.NoConstraints;
using PSO.Optimization.Model;
using PSO.ParticlesLC;

namespace PSO.Optimization.ParticlesLC
{
    public sealed class OptimizationParticleSwarmLC : ParticleSwarmLC
    {
        private readonly object _function;

        /// <summary>
        /// Constructor
        /// </summary>
        public OptimizationParticleSwarmLC(FunctionBase function)
        {
            _function = function;
            TendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest;
            TendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest;
            Momentum = PsoConfiguration.InitialInertia;
            UseGlobalOptimum = PsoConfiguration.UseGlobalOptimum;
            Constraints = new LinearConstraints();

            // Create the swarm:
            InitializeSwarm(PsoConfiguration.PopulationSize);

            // Sort according to the cost of each particle:
            SortParticles();
        }

        public OptimizationParticleSwarmLC(FunctionBase2 function)
        {
            _function = function;
            TendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest;
            TendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest;
            Momentum = PsoConfiguration.InitialInertia;
            UseGlobalOptimum = PsoConfiguration.UseGlobalOptimum;
            

            // Create the swarm:
            InitializeSwarm10(PsoConfiguration.PopulationSize);

            // Sort according to the cost of each particle:
            SortParticles();
        }
        
        /// <summary>
        /// Inicializa o enxame
        /// </summary>
        /// <param name="swarmSize"></param>
        public void InitializeSwarm(int swarmSize)
        {
            // Create the array of particles:
            Particles = new OptimizationParticleLC[swarmSize];
            for (var i = 0; i < swarmSize; i++)
            {
                // Init with random position in [-3,3]:
                var x = Rnd.NextDouble() * 6 - 3;
                var y = Rnd.NextDouble() * 6 - 3;
                double[] particlePosition = { x, y };

                // Random initial velocity [-3,3]:
                var vx = Rnd.NextDouble() * 6 - 3;
                var vy = Rnd.NextDouble() * 6 - 3;
                double[] particleVelocity = { vx, vy };

                this[i] = new OptimizationParticleLC(this, particlePosition, particleVelocity, _function);
            }
        }
        public void InitializeSwarm10(int swarmSize)
        {
            BenchmarksNames benchmarkName;
            var function = ((FunctionBase2) _function).GetType();
            Enum.TryParse(function.Name, out benchmarkName);
            var bounder = BenchmarksSolution.GetBounder(benchmarkName);

            Constraints = new LinearConstraints(bounder);

            // Create the array of particles:
            Particles = new OptimizationParticleLC[swarmSize];
            for (var i = 0; i < swarmSize; i++)
            {
                // Init with random position in [-3,3]:
                var x1 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x2 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x3 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x4 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x5 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x6 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x7 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x8 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x9 = Rnd.NextDouble() * bounder * 2 - bounder;
                var x10 = Rnd.NextDouble() * bounder * 2 - bounder;
                double[] particlePosition = { x1, x2, x3, x4, x5, x6, x7, x8, x9, x10 };

                // Random initial velocity [-3,3]:
                var vx1 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx2 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx3 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx4 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx5 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx6 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx7 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx8 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx9 = Rnd.NextDouble() * bounder * 2 - bounder;
                var vx10 = Rnd.NextDouble() * bounder * 2 - bounder;
                double[] particleVelocity = { vx1, vx2, vx3, vx4, vx5, vx6, vx7, vx8, vx9, vx10 };

                this[i] = new OptimizationParticleLC(this, particlePosition, particleVelocity, _function);
            }
        }

        public override void Iteration(int generation)
        {
            // Foreach particle calculate the cost and update the history:
            var localParticles = Particles; // Range-Check-Elimination
            foreach (var particle in localParticles)
            {
                //todo: se a iteração é a primeira, a avaliação da partícula já foi feita, então o CalculateCost não deve ser chamado de novo.
                //todo: Por outro lado a "história" da partícula ainda não foi atualizada, então está correto chamá-la em todas as iterações.
                particle.CalculateCost();
                particle.UpdateHistory();
            }

            // Sort according to fitness:
            SortParticles();

            // Update history of the swarm:
            if (Cost < BestCost)
            {
                BestCost = Cost;
                BestPosition = this[0].BestPosition;
            }

            //Determine the COnstraints
            //var linearConstraints = new LinearConstraints();
            // Determine new velocity and position of the particles in
            // the swarm:
            foreach (var particle in localParticles)
                particle.UpdateVelocityAndPosition(UseGlobalOptimum ? BestPosition : this[0].Position, Constraints);
            //Atualizo os parâmetros dinâmicos: inércia, c1 e c2.
            UpdateDinamicParameters(generation);

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
    }
}
