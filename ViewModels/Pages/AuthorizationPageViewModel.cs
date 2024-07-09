using VFMDesctop.Models.Interfaces;
using VFMDesctop.View.Pages;
using VFMDesctop.ViewModels.Help;

namespace VFMDesctop.ViewModels.Pages
{
    public class AuthorizationPageViewModel : NotifyPropertyChanged
    {
        private const string ErrorWrongPassword = "Не верный пароль";
        private const string ErrorWrongLogin = "Не верный логин";

        private string _LoginText;
        public string LoginText
        {
            get => _LoginText;
            set
            {
                _LoginText = value;
                OnPropertyChanged();
            }
        }

        private string _LoginHelperText;
        public string LoginHelperText
        {
            get => _LoginHelperText;
            set
            {
                _LoginHelperText = value;
                OnPropertyChanged();
            }
        }

        private string _PasswordPassword;
        public string PasswordPassword
        {
            get => _PasswordPassword;
            set
            {
                _PasswordPassword = value;
                OnPropertyChanged();
            }
        }

        private string _PasswordHelperText;
        public string PasswordHelperText
        {
            get => _PasswordHelperText;
            set
            {
                _PasswordHelperText = value;
                OnPropertyChanged();
            }
        }

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
