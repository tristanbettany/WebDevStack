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

        public RelayCommand BuildStackCommand { get; set; }
        public RelayCommand RebuildStackCommand { get; set; }

        public RelayCommand RemoveStackCommand { get; set; }
        public RelayCommand StartStackCommand { get; set; }
        public RelayCommand StopStackCommand { get; set; }
        public RelayCommand RestartStackCommand { get; set; }

        public RelayCommand StartNginxCommand { get; set; }
        public RelayCommand StopNginxCommand { get; set; }
        public RelayCommand RestartNginxCommand { get; set; }

        public RelayCommand CopySSHKeysCommand { get; set; }

        public RelayCommand ComposerInstallCommand { get; set; }
        public RelayCommand ComposerUpdateCommand { get; set; }
        public RelayCommand ComposerDumpCommand { get; set; }

        public RelayCommand LaravelKeygenCommand { get; set; }

        public RelayCommand NpmInstallCommand { get; set; }
        public RelayCommand NpmUpdateCommand { get; set; }

        public RelayCommand OpenPHPContainerCommand { get; set; }
        public RelayCommand OpenNodeContainerCommand { get; set; }

        public TrayIconViewModel()
        {
            ShowMainWindowCommand = new RelayCommand(ShowMainWindow, CanShowMainWindow);
            HideMainWindowCommand = new RelayCommand(HideMainWindow, CanHideMainWindow);
            ToggleMainWindowCommand = new RelayCommand(ToggleMainWindow);
            ExitApplicationCommand = new RelayCommand(ExitApplication);

            PlaceholderCommand = new RelayCommand(Placeholder);

            BuildStackCommand = new RelayCommand(BuildStack);
            RebuildStackCommand = new RelayCommand(RebuildStack);

            RemoveStackCommand = new RelayCommand(RemoveStack);
            StartStackCommand = new RelayCommand(StartStack);
            StopStackCommand = new RelayCommand(StopStack);
            RestartStackCommand = new RelayCommand(RestartStack);

            StartNginxCommand = new RelayCommand(StartNginx);
            StopNginxCommand = new RelayCommand(StopNginx);
            RestartNginxCommand = new RelayCommand(RestartNginx);

            CopySSHKeysCommand = new RelayCommand(CopySSHKeys);

            ComposerInstallCommand = new RelayCommand(ComposerInstall);
            ComposerUpdateCommand = new RelayCommand(ComposerUpdate);
            ComposerDumpCommand = new RelayCommand(ComposerDump);

            LaravelKeygenCommand = new RelayCommand(LaravelKeygen);

            NpmInstallCommand = new RelayCommand(NpmInstall);
            NpmUpdateCommand = new RelayCommand(NpmUpdate);

            OpenPHPContainerCommand = new RelayCommand(OpenPHPContainer);
            OpenNodeContainerCommand = new RelayCommand(OpenNodeContainer);
        }

        private void NpmInstall(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " npm install"
            );
        }

        private void NpmUpdate(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " npm update"
            );
        }

        private void ComposerInstall(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " composer install"
            );
        }

        private void ComposerUpdate(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " composer update"
            );
        }

        private void ComposerDump(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " composer dump-autoload"
            );
        }

        private void LaravelKeygen(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " php artisan key:generate"
            );
        }

        private void StartNginx(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose start nginx"
            );
        }

        private void StopNginx(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose stop nginx"
            );
        }

        private void RestartNginx(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose stop nginx ; docker-compose start nginx"
            );
        }

        private void BuildStack(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose up -d --build ;" + GetSSHKeyCommands()
            );
        }

        private void RebuildStack(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose down -v ; docker-compose up -d --build ;" + GetSSHKeyCommands()
            );
        }

        private void RemoveStack(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose down -v"
            );
        }

        private void StartStack(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose up -d ;" + GetSSHKeyCommands()
            );
        }

        private void StopStack(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose down"
            );
        }

        private void RestartStack(object obj)
        {
            Process.Start(
                "powershell.exe",
                "cd " + Env.GetString("THIS_DIR") + "; docker-compose down ; docker-compose up -d ;" + GetSSHKeyCommands()
            );
        }

        private void CopySSHKeys(object obj)
        {
            Process.Start(
                "powershell.exe",
                GetSSHKeyCommands()
            );
        }

        private void OpenPHPContainer(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_PHP_CONTAINER") + " bash"
            );
        }

        private void OpenNodeContainer(object obj)
        {
            Process.Start(
                "powershell.exe",
                "docker container exec -it -w " + Env.GetString("WORKING_DIR") + " " + Env.GetString("CLI_NODE_CONTAINER") + " bash"
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

        private string GetSSHKeyCommands()
        {
            return "docker container exec -w /root php70 ./keys.sh ;" +
            "docker container exec -w /root php71 ./keys.sh ;" +
            "docker container exec -w /root php72 ./keys.sh ;" +
            "docker container exec -w /root php73 ./keys.sh ;" +
            "docker container exec -w /root php74 ./keys.sh ;" +
            "docker container exec -w /root node ./keys.sh";
        }
    }
}
