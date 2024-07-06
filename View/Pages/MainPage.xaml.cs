using System.Windows.Controls;
using VFMDesctop.ViewModels;

namespace VFMDesctop.View.Pages
{
    /// <summary>
    /// Логика взаимодействия для MainPage.xaml
    /// </summary>
    public partial class MainPage : Page
    {
        public MainPage(MainPageViewModel mainPageViewModel)
        {
            InitializeComponent();
            DataContext = mainPageViewModel;
        }
    }
}
