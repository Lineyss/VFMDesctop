using System.Windows.Controls;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.View.Pages;
using VFMDesctop.ViewModels.Help;

namespace VFMDesctop.ViewModels.Windows
{
    public class MainWindowViewModel : NotifyPropertyChanged
    {
        private Page _SourceNavigate;
        public Page SourceNavigate
        {
            get => _SourceNavigate;
            set
            {
                _SourceNavigate = value;
                OnPropertyChanged();
            }
        }

        private readonly INavigationService navigationService;
        public MainWindowViewModel(INavigationService navigationService,
            IFactory<AuthorizationPage> factoryAuthPage)
        {
            this.navigationService = navigationService;
            SourceNavigate = factoryAuthPage.Create();
            navigationService.NavigationChange += NavigationService_NavigationChange;
        }

        private void NavigationService_NavigationChange()
        {
            SourceNavigate = navigationService.GetNavigate();
        }
    }
}
