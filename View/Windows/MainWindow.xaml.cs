using System.Runtime.Remoting.Contexts;
using System.Windows;
using VFMDesctop.ViewModels;

namespace VFMDesctop.View
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainWindowViewModel mainWindow = new MainWindowViewModel();
            DataContext = mainWindow;

            mainWindow.OnWindowClose += MainWindow_OnWindowClose;
        }

        private void MainWindow_OnWindowClose() => Close();
    }
}
