using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using PSO.View.Annotations;
using PSO.View.Properties;
using Forms = System.Windows.Forms;

namespace PSO.View.ViewModel
{
    public class SystemConfigurationWindowViewModel : INotifyPropertyChanged
    {

        /// <summary>
        /// Objeto de configurações.
        /// </summary>
        private readonly Settings _settings;

        /// <summary>
        /// Caminho da pasta de configuração. Nesta pasta será armazenada 
        /// todas as outras pastas, como as que contém o estudo e suas sub-pastas.
        /// </summary>
        private string _configurationsPath;

        public SystemConfigurationWindowViewModel()
        {
            _settings = Settings.Default;
        }

        public string ConfigurationsPath
        {
            get
            {
                return _configurationsPath;
            }
            set
            {
                _configurationsPath = value;
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Abre uma caixa de diálogo para a escolha de uma pasta.
        /// </summary>
        public void OpenFolderBrowserDialog()
        {
            var folderBrowserDialog = new Forms.FolderBrowserDialog();
            var folderBrowserDialogShowDialogResult = folderBrowserDialog.ShowDialog();
            if (folderBrowserDialogShowDialogResult == Forms.DialogResult.OK)
            {
                ConfigurationsPath = folderBrowserDialog.SelectedPath;
            }
        }

        

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        public void Save()
        {
            if (ConfigurationsPath == null)
            {
                throw new Exception("O caminho para a pasta deve ser fornecido.");
            }
            _settings.ResultsPath = ConfigurationsPath;
            _settings.Save();
        }
    }
}
