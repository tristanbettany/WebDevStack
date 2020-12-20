using System;
using System.Windows;
using WedDevStackController.Common.Commands;
using WedDevStackController.Common.ViewModels;

namespace WedDevStackController.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private Window _window;

        public MainViewModel(Window window)
        {
            _window = window;
        }
    }
}
