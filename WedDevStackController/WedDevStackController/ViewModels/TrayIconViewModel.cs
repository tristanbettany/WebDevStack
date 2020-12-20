using DotNetEnv;
using System;
using System.Diagnostics;
using System.Windows;
using WedDevStackController.Common.Commands;
using WedDevStackController.Common.ViewModels;
using WedDevStackController.Views;

namespace WedDevStackController.ViewModels
{
    public class TrayIconViewModel : BaseViewModel
    {
        public RelayCommand ShowMainWindowCommand { get; set; }
        public RelayCommand HideMainWindowCommand { get; set; }
        public RelayCommand ExitApplicationCommand { get; set; }
        public RelayCommand ToggleMainWindowCommand { get; set; }

        public RelayCommand PlaceholderCommand { get; set; }

        public RelayCommand OpenPHPContainerCommand { get; set; }

        public TrayIconViewModel()
        {
            ShowMainWindowCommand = new RelayCommand(ShowMainWindow, CanShowMainWindow);
            HideMainWindowCommand = new RelayCommand(HideMainWindow, CanHideMainWindow);
            ToggleMainWindowCommand = new RelayCommand(ToggleMainWindow);
            ExitApplicationCommand = new RelayCommand(ExitApplication);

            PlaceholderCommand = new RelayCommand(Placeholder);

            OpenPHPContainerCommand = new RelayCommand(OpenPHPContainer);
        }

        private void OpenPHPContainer(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " bash"
            );
        }

        private void Placeholder(object obj)
        {
            //
        }

        private bool CanShowMainWindow(object obj)
        {
            return Application.Current.MainWindow == null;
        }

        private void ShowMainWindow(object obj)
        {
            Application.Current.MainWindow = new MainWindow();
            Application.Current.MainWindow.Show();
        }

        private bool CanHideMainWindow(object obj)
        {
            return Application.Current.MainWindow != null;
        }

        private void HideMainWindow(object obj)
        {
            Application.Current.MainWindow.Close();
        }

        private void ToggleMainWindow(object obj)
        {
            if (CanShowMainWindow(obj) == true)
            {
                ShowMainWindow(obj);
            }
            else
            {
                HideMainWindow(obj);
            }
        }

        private void ExitApplication(object obj)
        {
            Application.Current.Shutdown();
        }
    }
}
