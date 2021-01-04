using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using PSO.Optimization.Model;
using PSO.View.Annotations;
using PSO.View.ViewModel;

namespace PSO.View.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : INotifyPropertyChanged
    {
        private MainWindowViewModel _model;
        public MainWindowViewModel Model
        {
            get { return _model; }
            set
            {
                _model = value;
                OnPropertyChanged();
            }
        }
        
        private PsoConfiguration PsoConfiguration { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            _model = new MainWindowViewModel();
        }
        
        private void ModifyPsoConfigurationEvent(object sender, RoutedEventArgs e)
        {
            LaunchWindow(WindowsToLaunchEnum.PsoConfigurationWindow, true);
        }

        private void OpenSystemConfigurationWindow_OnClick(object sender, RoutedEventArgs e)
        {
            LaunchWindow(WindowsToLaunchEnum.SystemConfigurationWindow, true);
        }

        private void HandleCloseEvent(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Optimize_OnClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if(!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();    
                    Model.StartOptimization();
                    MessageBox.Show("Otimização Concluída com Sucesso!", "Otimização Concluída", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void OptimizeWithLinearConstraints_OnClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if (!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();
                    Model.StartOptimizationWithLinearConstraints();
                    MessageBox.Show("Otimização Concluída com Sucesso!", "Otimização Concluída", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void LaunchWindow(WindowsToLaunchEnum windowToLaunch, bool isEditMode)
        {
            try
            {
                Model.LaunchWindow(windowToLaunch, isEditMode, this);
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private static void ShowError(string message)
        {
            MessageBox.Show(message, "NLC_PSO", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void Process(Action<CancellationToken> taskAction)
        {
            Process(taskAction, null);
        }

        private readonly HashSet<CancellationTokenSource> _taskCancellationSource =
          new HashSet<CancellationTokenSource>();

        /// <summary>
        /// Executa um processamento custoso sem bloquear a UI.
        /// </summary>
        /// <param name="taskAction">Ação a ser executada em outra thread.</param>
        /// <param name="afterTaskAction">Ação executada na thread da UI após o término de taskAction</param>
        protected void Process(Action<CancellationToken> taskAction, Action<Exception> afterTaskAction)
        {
            Cursor = Cursors.AppStarting;

            var cancellation = new CancellationTokenSource();
            var token = cancellation.Token;
            _taskCancellationSource.Add(cancellation);

            Action<Task> after = t => Dispatcher.Invoke(

                () =>
                {
                    // remove o token
                    _taskCancellationSource.Remove(cancellation);

                    if (t.IsCompleted && !t.IsCanceled && afterTaskAction != null)
                    {
                        afterTaskAction(t.Exception);
                    }

                    Cursor = Cursors.Arrow;
                });

            var task = new Task(() =>
            {
                taskAction(token);
                token.ThrowIfCancellationRequested();
            }, token);
            task.ContinueWith(after, token);

            task.Start();
        }

        private void OptimizeWithNonLinearConstraints_OnClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if (!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();
                    Model.StartOptimizationWithNonLinearConstraints();
                    MessageBox.Show("Otimização Concluída com Sucesso!", "Otimização Concluída", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void OptimizeWithNonLinearConstraints2_OnClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if (!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();
                    Model.StartOptimizationWithNonLinearConstraintsG12();
                    MessageBox.Show("Otimização Concluída com Sucesso!", "Otimização Concluída", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void OptimizeWithNonLinearConstraints3_OnClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if (!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();
                    Model.StartOptimizationWithNonLinearConstraintsRealMinimization();
                    MessageBox.Show("Otimização Concluída com Sucesso!", "Otimização Concluída", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void OptimizeWithNonLinearConstraints4_OnClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if (!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();
                    Model.StartOptimizationWithNonLinearConstraintsRealMaximization();
                    MessageBox.Show("Otimização Concluída com Sucesso!", "Otimização Concluída", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show(exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }

        private void RunForrestRun(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                try
                {
                    if (!PsoConfiguration.ConfigurationSaved)
                        PsoConfiguration.SetDefaultConfiguration();
                    Model.RunForrestRun();
                    MessageBox.Show("Otimizações Concluídas com Sucesso!", "Otimizações Concluídas", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                }
                catch (Exception exception)
                {
                    MessageBox.Show( exception.Message, "Erro na otimização", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            });
        }
    }
}
