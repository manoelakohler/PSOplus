using System;
using System.Windows;
using PSO.Optimization;
using PSO.Optimization.Utils;
using PSO.View.Properties;
using PSO.View.View;
using PSO.View.View.WindowsBase;

namespace PSO.View.ViewModel
{
    public class MainWindowViewModel 
    {
        public void StartOptimization()
        {
            try
            {
                Optimize.StartOptimization(Settings.Default.ResultsPath);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização." + exception.Message);
            }
        }

        public void StartOptimizationWithLinearConstraints()
        {
            try
            {
                Optimize.StartOptimizationWithLinearConstraints(Settings.Default.ResultsPath);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização com restrições lineares." + exception.Message);
            }
        }

        public void StartOptimizationWithNonLinearConstraints()
        {
            try
            {
                Optimize.StartOptimizationWithNonLinearConstraints(Settings.Default.ResultsPath, true);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização com restrições não lineares." + exception.Message);
            }
        }


        public void StartOptimizationWithNonLinearConstraintsG12()
        {
            try
            {
                Optimize.StartOptimizationWithNonLinearConstraintsG12(Settings.Default.ResultsPath, true);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização com restrições não lineares." + exception.Message);
            }
        }

        public void StartOptimizationWithNonLinearConstraintsRealMinimization()
        {
            try
            {
                Optimize.StartOptimizationRealMinimization(Settings.Default.ResultsPath, true);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização com restrições não lineares." + exception.Message);
            }
        }

        public void StartOptimizationWithNonLinearConstraintsRealMaximization()
        {
            try
            {
                Optimize.StartOptimizationRealMaximization(Settings.Default.ResultsPath, true);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização com restrições não lineares." + exception.Message);
            }
        }

        public void RunForrestRun()
        {
            try
            {
                Optimize.RunForrestRun(Settings.Default.ResultsPath);
            }
            catch (Exception exception)
            {
                Services.KillSpecificExcelFileProcess();
                throw new Exception("Erro durante a otimização com restrições não lineares." + exception.Message);
            }
        }

        public void LaunchWindow(WindowsToLaunchEnum windowToLaunch, bool isEditMode, Window owner)
        {
            PsoNlcWindow window = null;
            switch (windowToLaunch)
            {
                case WindowsToLaunchEnum.PsoConfigurationWindow:
                    window = new PsoConfigurationWindow();
                    break;
                case WindowsToLaunchEnum.SystemConfigurationWindow:
                    window = new SystemConfigurationWindow();
                    break;
            }

            if (window == null) return;
            using (window)
            {
                if (owner != null)
                    window.Owner = owner;

                window.ShowDialog();
            }
        }
    }
}
