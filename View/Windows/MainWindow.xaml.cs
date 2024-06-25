using System.Runtime.Remoting.Contexts;
using System.Windows;
using VFMDesctop.ViewModels.Windows;

namespace VFMDesctop.View
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
        }
    }
}
