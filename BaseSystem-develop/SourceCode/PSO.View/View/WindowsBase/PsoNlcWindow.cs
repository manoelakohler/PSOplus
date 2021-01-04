using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PSO.View.Annotations;

namespace PSO.View.View.WindowsBase
{
    public abstract class PsoNlcWindow : Window, IDisposable, INotifyPropertyChanged
    {
        private readonly HashSet<CancellationTokenSource> _taskCancellationSource =
            new HashSet<CancellationTokenSource>();

        private bool _alreadyDisposed;
        private bool _isProcessing;

        protected virtual bool ShowQuestion(string message)
        {
            var result = MessageBox.Show(this, message, "NLC_PSO", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.No) return false;
            else return true;
        }

        protected virtual void ShowError(string message)
        {
            MessageBox.Show(this, message, "NLC_PSO", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        protected virtual void ShowError(Exception ex)
        {
            if (ex is AggregateException)
            {
                ShowError(ex.GetBaseException().Message);
            }
            else
            {
                ShowError(ex.Message);
            }
        }

        #region implementation of processing task

        protected bool AskAndCancelAllTasks(string messageBoxText, string caption)
        {
            if (_taskCancellationSource == null || _taskCancellationSource.Count <= 0) return true;

            var result = MessageBox.Show(messageBoxText, caption, MessageBoxButton.YesNo);
            if (result != MessageBoxResult.Yes) return false;

            foreach (var cancellationTokenSource in _taskCancellationSource)
            {
                cancellationTokenSource.Cancel();
            }

            return true;
        }

        protected virtual void CancelAllTasks()
        {
            foreach (var cancellationTokenSource in _taskCancellationSource)
            {
                cancellationTokenSource.Cancel();
            }
        }

        protected void Process(Action<CancellationToken> taskAction)
        {
            Process(taskAction, null);
        }

        /// <summary>
        /// Executa um processamento custoso sem bloquear a UI.
        /// </summary>
        /// <param name="taskAction">Ação a ser executada em outra thread.</param>
        /// <param name="afterTaskAction">Ação executada na thread da UI após o término de taskAction</param>
        /// <param name="isLongRunning"></param>
        protected void Process(Action<CancellationToken> taskAction, Action<Exception> afterTaskAction,
            bool isLongRunning = false)
        {
            Cursor = Cursors.AppStarting;
            SetMode(true);

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
                    SetMode(false); // reabilita tudo
                });

            var task = new Task(() =>
            {
                taskAction(token);
                token.ThrowIfCancellationRequested();
            }, token, isLongRunning ? TaskCreationOptions.LongRunning : TaskCreationOptions.None);
            task.ContinueWith(after, token);

            task.Start();
        }

        protected virtual void SetMode(bool isWait)
        {
        }

        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #region error handling

        protected virtual void HandleErrors(Exception ex)
        {
            if (ex is AggregateException)
            {
                ShowError(ex.GetBaseException().Message);
            }
            else
            {
                ShowError(ex.Message);
            }
        }

        #endregion

        #region Dispose

        /// <summary>
        /// Método que libera a memória desnecessária.
        /// </summary>
        /// <param name="isDisposing">True se foi chamado pelo método Dispose do framework. False, caso contrário.</param>
        protected virtual void Dispose(bool isDisposing)
        {
            // Don't dispose more than once.
            if (_alreadyDisposed)
                return;
            if (isDisposing)
            {
                // elided: free managed resources here.
            }

            // elided: free unmanaged resources here.

            // Set disposed flag:
            _alreadyDisposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);

        }
        #endregion
    }
}
