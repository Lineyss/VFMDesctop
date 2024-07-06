using VFMDesctop.Models.Interfaces;
using VFMDesctop.View.Pages;
using VFMDesctop.ViewModels.Help;

namespace VFMDesctop.ViewModels.Pages
{
    public class AuthorizationPageViewModel
    {
        private readonly INavigationService navigationService;
        private readonly IFactory<MainPage> factoryMainPage;
        public AuthorizationPageViewModel(INavigationService navigationService, 
            IFactory<MainPage> factoryMainPage)
        {
            this.navigationService = navigationService;
            this.factoryMainPage = factoryMainPage;

            LoginButton_Click = new BindableCommand(_ => Login());
        }

        public BindableCommand LoginButton_Click { get; set; }

        private void Login() => navigationService.SetNavigate(factoryMainPage.Create());
    }
}
