using DotNetEnv;
using System;
using System.IO;
using System.Windows;
using WedDevStackController.Common.Commands;
using WedDevStackController.Common.ViewModels;

namespace WedDevStackController.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Window _window;

        private string envFile;
        public string EnvFile 
        {
            get { return envFile; }
            set
            {
                envFile = value;
                OnPropertyChanged();
                SaveEnvFile();
            }
        }

        private string envFilePath;
        public string EnvFilePath
        {
            get { return envFilePath; }
            set
            {
                envFilePath = value;
                OnPropertyChanged();
                LoadEnvFile();
                OnPropertyChanged("EnvFile");
            }
        }

        public MainViewModel(Window window)
        {
            _window = window;

            string userPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile);
            EnvFilePath = userPath + "\\Code\\WebDevStack\\.env";
            LoadEnvFile();
        }

        private void LoadEnvFile()
        {
            if(File.Exists(EnvFilePath) == true)
            {
                EnvFile = File.ReadAllText(EnvFilePath);
            }
            else
            {
                EnvFile = "";
            }
        }

        private void SaveEnvFile()
        {
            try
            {
                if (File.Exists(EnvFilePath) == true)
                {
                    File.WriteAllText(EnvFilePath, EnvFile);
                    Env.TraversePath().Load();
                }
            }
            catch (Sprache.ParseException exception)
            {
                
            }
            catch (System.UnauthorizedAccessException exception)
            {

            }
        }
    }
}
