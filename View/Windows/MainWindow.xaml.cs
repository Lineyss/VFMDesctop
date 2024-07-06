using System.Windows;
using VFMDesctop.ViewModels.Windows;

namespace VFMDesctop.View
{
    public partial class MainWindow : Window
    {
        public MainWindow(MainWindowViewModel mainWindow)
        {
            InitializeComponent();
            DataContext = mainWindow;
        }
    }
}
