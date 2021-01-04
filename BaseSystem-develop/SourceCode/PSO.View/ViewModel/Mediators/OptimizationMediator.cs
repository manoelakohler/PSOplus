using PSO.Optimization;

namespace PSO.View.ViewModel.Mediators
{
    public class OptimizationMediator : MediatorBase
    {
        public enum OptimizationMessage
        {
            OptimizationStarted,
            OptimizationFinished,
            OptimizationError
        }


        #region SendMessage for Content

        public virtual void SendSimulationFinishedMessage(Optimize optimizationService)
        {
            SendMessage(OptimizationMessage.OptimizationFinished, optimizationService);
        }

        public virtual void SendSimulationStartedMessage(Optimize optimizationService)
        {
            SendMessage(OptimizationMessage.OptimizationStarted, optimizationService);
        }

        public virtual void SendSimulationErrorMessage(Optimize optimizationService)
        {
            SendMessage(OptimizationMessage.OptimizationError, optimizationService);
        }

        #endregion
    }
}
