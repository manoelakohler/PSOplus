using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PSO.View.View.WindowsBase;
using PSO.View.ViewModel;

namespace PSO.View.View
{
    /// <summary>
    /// Interaction logic for SystemConfigurationWindow.xaml
    /// </summary>
    public partial class SystemConfigurationWindow : PsoNlcWindow
    {
        public SystemConfigurationWindowViewModel Model { get; set; }

        public SystemConfigurationWindow()
        {
            Model = new SystemConfigurationWindowViewModel();
            InitializeComponent();
        }

        private void SearchButtonClick(object sender, RoutedEventArgs e)
        {
            Model.OpenFolderBrowserDialog();
        }

        private void SaveButtonClick(object sender, RoutedEventArgs e)
        {
            Process(token =>
            {
                Model.Save();

            }, (ex) =>
            {
                
                if (ex != null)
                {
                    HandleErrors(ex);
                }
                else
                {
                    DialogResult = true;
                    Close();
                }
            });
        }
        
    }
}
