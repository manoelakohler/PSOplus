using System;
using System.Windows;
using PSO.Optimization.Model;
using PSO.View.View.WindowsBase;
using PSO.View.ViewModel;

namespace PSO.View.View
{
    /// <summary>
    /// Interaction logic for PsoConfigurationWindow.xaml
    /// </summary>
    public partial class PsoConfigurationWindow : PsoNlcWindow
    {
        private bool _isDisposed;

        private PsoConfigurationWindowViewModel _model;
        public PsoConfigurationWindowViewModel Model
        {
            get
            {
                AssertNotDisposed();
                return _model;
            }
        }
        private void AssertNotDisposed()
        {
            if (_isDisposed) throw new ObjectDisposedException("");
        }
        
     
        public PsoConfigurationWindow()
        {
            _model = new PsoConfigurationWindowViewModel();
            InitializeComponent();

            if (!PsoConfiguration.ConfigurationSaved)
                Model.LoadDefaultConfiguration();
            else
                Model.SetCurrentConfiguration();
        }



        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                Model.AssertModelIsValid();

                PsoConfiguration.SaveConfiguration(Model.PopulationSize,
                    Model.InitialAccelerationCoeficientTendencyToOwnBest,
                    Model.FinalAccelerationCoeficientTendencyToOwnBest,
                    Model.InitialAccelerationCoeficientTendencyToGlobalBest,
                    Model.FinalAccelerationCoeficientTendencyToGlobalBest, Model.InitialInertia, Model.FinalInertia,
                    Model.NumberOfGenerations, Model.NumberOfIterations, Model.NumberOfGenerationsWithoutImprovement,
                    Model.UseGlobalOptimum, true, Model.NumberOfRunsWithSteadyState, Model.FootHoldsMultiplier);

                Close();
            }
            catch (Exception ex)
            {
                HandleErrors(ex);
            }
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }



        ~PsoConfigurationWindow()
        {
            Dispose(false);
        }
      
        protected override void Dispose(bool isDisposing)
        {
            if (_isDisposed)
                return;

            /*Se estivermos no destrutor (isDisposing==false) todos os objetos gerenciados pelo GC podem já não existir mais. 
             * Por isso, não se deve "libera-los", uma vez que não há nenhum ganho em faze-lo. Por outro lado, se estivermos
             * numa chamada do método Dispose(), então o usuário deste objeto claramente está mandando liberar a memória 
             * ocupada por este objeto antes mesmo do GC pedir isso.
             */
            if (isDisposing)
            {
                //todo: free managed resources
            }

            //free unmanaged resources: Streams, Process, etc.
            

            // faz a classe base liberar seus recursos. Não é necessário chamar o GC.SupressFinalize neste método.
            base.Dispose(isDisposing);
            _isDisposed = true;
        }
    }
}
