using PSO.Benchmarks.NoConstraints;

namespace PSO.Optimization.Model
{
    public class PsoConfiguration
    {
        public static int PopulationSize { get; set; }

        public static double InitialAccelerationCoeficientTendencyToOwnBest { get; set; }
        public static double FinalAccelerationCoeficientTendencyToOwnBest { get; set; }

        public static double InitialAccelerationCoeficientTendencyToGlobalBest { get; set; }
        public static double FinalAccelerationCoeficientTendencyToGlobalBest { get; set; }

        public static double InitialInertia { get; set; }
        public static double FinalInertia { get; set; }

        public static int NumberOfGenerations { get; set; }
        public static int NumberOfIterations { get; set; }
        public static int NumberOfRunsWithSteadyState { get; set; }

        public static int FootHoldsMultiplier { get; set; }

        public static int NumberOfGenerationsWithoutImprovement { get; set; }

        public static bool UseGlobalOptimum { get; set; }

        public static bool ConfigurationSaved { get; set; }

        public static string ResultsPath { get; set; }


        public static void SetDefaultConfiguration()
        {
            InitialAccelerationCoeficientTendencyToOwnBest = 1.5;
            FinalAccelerationCoeficientTendencyToOwnBest = 3.5;
            InitialAccelerationCoeficientTendencyToGlobalBest = 2.5;
            FinalAccelerationCoeficientTendencyToGlobalBest = 0.5;
            InitialInertia = 1.0;
            FinalInertia = 0.7;
            NumberOfGenerations = 100;
            NumberOfIterations = 1;
            NumberOfRunsWithSteadyState = 3;
            FootHoldsMultiplier = 10;
            //NumberOfGenerationsWithoutImprovement = 0;
            PopulationSize = 70;
            UseGlobalOptimum = false;
            ConfigurationSaved = true;
        }

        public static void SaveConfiguration(int populationSize, double intialAccelerationCoeficientTendencyToOwnBest,
            double finalAccelerationCoeficientTendencyToOwnBest,
            double initialAccelerationCoeficientTendencyToGlobalBest,
            double finalAccelerationCoeficientTendencyToGlobalBest, double intialInertia, double finalInertia,
            int numberOfGenerations, int numberOfIterations, int numberOfGenerationsWithoutImprovement,
            bool useGlobalOptimum, bool configurationSaved, int numberOfRunsWithSteadyState, int footHoldsMultiplier)

        {
            PopulationSize = populationSize;
            InitialAccelerationCoeficientTendencyToOwnBest = intialAccelerationCoeficientTendencyToOwnBest;
            FinalAccelerationCoeficientTendencyToOwnBest = finalAccelerationCoeficientTendencyToOwnBest;
            InitialAccelerationCoeficientTendencyToGlobalBest = initialAccelerationCoeficientTendencyToGlobalBest;
            FinalAccelerationCoeficientTendencyToGlobalBest = finalAccelerationCoeficientTendencyToGlobalBest;
            InitialInertia = intialInertia;
            FinalInertia = finalInertia;
            NumberOfGenerations = numberOfGenerations;
            NumberOfIterations = numberOfIterations;
            NumberOfRunsWithSteadyState = numberOfRunsWithSteadyState;
            FootHoldsMultiplier = footHoldsMultiplier;
            NumberOfGenerationsWithoutImprovement = numberOfGenerationsWithoutImprovement;
            UseGlobalOptimum = useGlobalOptimum;
            ConfigurationSaved = configurationSaved;
        }
    }
}
