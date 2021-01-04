using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using PSO.Optimization;
using PSO.Optimization.Model;
using PSO.View.Annotations;
using PSO.View.ViewModel.Mediators;

namespace PSO.View.ViewModel
{
    public class PsoConfigurationWindowViewModel : INotifyPropertyChanged
    {
        #region PrivateVariables

        private int _populationSize;
        private double _initialAccelarationCoeficientTendencyToOwnBest;
        private double _finalAccelarationCoeficientTendencyToOwnBest;
        private double _intialAccelarationCoeficientTendencyToGlobalBest;
        private double _finalAccelarationCoeficientTendencyToGlobalBest;
        private double _initialInertia;
        private double _finalInertia;
        private int _numberOfGenerations;
        private int _numberOfIterations;
        private int _numberOfGenerationsWithoutImprovement;
        private bool _useGlobalOptimum;

        #endregion

        #region Properties

        public int PopulationSize
        {
            get { return _populationSize; }
            set
            {
                _populationSize = value;
                OnPropertyChanged();
            }
        }

        public double InitialAccelerationCoeficientTendencyToOwnBest
        {
            get { return _initialAccelarationCoeficientTendencyToOwnBest; }
            set
            {
                _initialAccelarationCoeficientTendencyToOwnBest = value;
                OnPropertyChanged();
            }
        }
        public double FinalAccelerationCoeficientTendencyToOwnBest
        {
            get { return _finalAccelarationCoeficientTendencyToOwnBest; }
            set
            {
                _finalAccelarationCoeficientTendencyToOwnBest = value;
                OnPropertyChanged();
            }
        }

        public double InitialAccelerationCoeficientTendencyToGlobalBest
        {
            get { return _intialAccelarationCoeficientTendencyToGlobalBest; }
            set
            {
                _intialAccelarationCoeficientTendencyToGlobalBest = value;
                OnPropertyChanged();
            }
        }
        public double FinalAccelerationCoeficientTendencyToGlobalBest
        {
            get { return _finalAccelarationCoeficientTendencyToGlobalBest; }
            set
            {
                _finalAccelarationCoeficientTendencyToGlobalBest = value;
                OnPropertyChanged();
            }
        }

        public double InitialInertia
        {
            get { return _initialInertia; }
            set
            {
               _initialInertia = value;
                OnPropertyChanged();
            }
        }
        public double FinalInertia
        {
            get { return _finalInertia; }
            set
            {
                _finalInertia = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfGenerations
        {
            get { return _numberOfGenerations; }
            set
            {
                _numberOfGenerations = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfIterations
        {
            get { return _numberOfIterations; }
            set
            {
                _numberOfIterations = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfRunsWithSteadyState
        {
            get { return _numberOfRunsWithSteadyState; }
            set
            {
                _numberOfRunsWithSteadyState = value;
                OnPropertyChanged();
            }
        }

        public int FootHoldsMultiplier
        {
            get { return _footHoldsMultiplier; }
            set
            {
                _footHoldsMultiplier = value;
                OnPropertyChanged();
            }
        }

        public int NumberOfGenerationsWithoutImprovement
        {
            get { return _numberOfGenerationsWithoutImprovement; }
            set
            {
                _numberOfGenerationsWithoutImprovement = value;
                OnPropertyChanged();
            }
        }

        public bool UseGlobalOptimum
        {
            get { return _useGlobalOptimum; }
            set
            {
                _useGlobalOptimum = value;
                OnPropertyChanged();
            }
        }

        private bool _isNotProcessing = true;

        public bool IsNotProcessing
        {
            get { return _isNotProcessing; }
            set
            {
                _isNotProcessing = value;
                OnPropertyChanged();
                OnPropertyChanged("IsProcessing");
            }
        }
        public bool IsProcessing
        {
            get { return !IsNotProcessing; }
        }


        private OptimizationMediator _optimizationMediator;
        private int _numberOfRunsWithSteadyState;
        private int _footHoldsMultiplier;

        public OptimizationMediator SimulationMediator
        {
            get { return _optimizationMediator; }
        }

        #endregion

        public void LoadDefaultConfiguration()
        {
            PsoConfiguration.SetDefaultConfiguration();

            InitialAccelerationCoeficientTendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest;
            FinalAccelerationCoeficientTendencyToOwnBest = PsoConfiguration.FinalAccelerationCoeficientTendencyToOwnBest;
            InitialAccelerationCoeficientTendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest;
            FinalAccelerationCoeficientTendencyToGlobalBest = PsoConfiguration.FinalAccelerationCoeficientTendencyToGlobalBest;
            InitialInertia = PsoConfiguration.InitialInertia;
            FinalInertia = PsoConfiguration.FinalInertia;
            NumberOfGenerations = PsoConfiguration.NumberOfGenerations;
            NumberOfIterations = PsoConfiguration.NumberOfIterations;
            NumberOfRunsWithSteadyState = PsoConfiguration.NumberOfRunsWithSteadyState;
            FootHoldsMultiplier = PsoConfiguration.FootHoldsMultiplier;
            //NumberOfGenerationsWithoutImprovement = 0;
            PopulationSize = PsoConfiguration.PopulationSize;
            UseGlobalOptimum = PsoConfiguration.UseGlobalOptimum;

            PsoConfiguration.SaveConfiguration(PopulationSize, InitialAccelerationCoeficientTendencyToOwnBest, FinalAccelerationCoeficientTendencyToOwnBest,
                InitialAccelerationCoeficientTendencyToGlobalBest, FinalAccelerationCoeficientTendencyToGlobalBest, InitialInertia, FinalInertia, NumberOfGenerations,
                NumberOfIterations, NumberOfGenerationsWithoutImprovement, UseGlobalOptimum, true, NumberOfRunsWithSteadyState, FootHoldsMultiplier);
        }


        public void SetCurrentConfiguration()
        {
            InitialAccelerationCoeficientTendencyToOwnBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToOwnBest;
            FinalAccelerationCoeficientTendencyToOwnBest = PsoConfiguration.FinalAccelerationCoeficientTendencyToOwnBest;
            InitialAccelerationCoeficientTendencyToGlobalBest = PsoConfiguration.InitialAccelerationCoeficientTendencyToGlobalBest;
            FinalAccelerationCoeficientTendencyToGlobalBest = PsoConfiguration.FinalAccelerationCoeficientTendencyToGlobalBest;
            InitialInertia = PsoConfiguration.InitialInertia;
            FinalInertia = PsoConfiguration.FinalInertia;
            NumberOfGenerations = PsoConfiguration.NumberOfGenerations;
            NumberOfIterations = PsoConfiguration.NumberOfIterations;
            NumberOfRunsWithSteadyState = PsoConfiguration.NumberOfRunsWithSteadyState;
            FootHoldsMultiplier = PsoConfiguration.FootHoldsMultiplier;
            //NumberOfGenerationsWithoutImprovement = 0;
            PopulationSize = PsoConfiguration.PopulationSize;
            UseGlobalOptimum = PsoConfiguration.UseGlobalOptimum;
        }

        public void AssertModelIsValid()
        {
            if (CheckForNullValues())
                throw new InvalidOperationException(
                    "Todos os parâmetros do PSO devem ser preenchidos para realizar uma otimização");

        }

        /// <summary>
        /// Verifica se algum parâmetro da otimização está zerado
        /// </summary>
        /// <returns></returns>
        private bool CheckForNullValues()
        {
            return InitialAccelerationCoeficientTendencyToGlobalBest <= 0.001 ||
                   FinalAccelerationCoeficientTendencyToGlobalBest <= 0.001 ||
                   InitialAccelerationCoeficientTendencyToOwnBest <= 0.001 ||
                   FinalAccelerationCoeficientTendencyToOwnBest <= 0.001 || InitialInertia <= 0.001 ||
                   FinalInertia <= 0.001 || PopulationSize == 0 || NumberOfGenerations == 0 ||
                   NumberOfRunsWithSteadyState == 0 || _footHoldsMultiplier == 0;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
