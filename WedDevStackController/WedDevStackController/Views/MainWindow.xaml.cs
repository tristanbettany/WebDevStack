using System.Windows;
using WedDevStackController.ViewModels;

namespace WedDevStackController.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel(this);
        }
    }
}
