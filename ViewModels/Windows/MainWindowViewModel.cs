using System;
using System.Windows.Controls;
using VFMDesctop.ViewModels.Help;
using System.Windows.Media;

namespace VFMDesctop.ViewModels.Windows
{
    internal class MainWindowViewModel : NotifyPropertyChanged
    {
        public BindableCommand homeButton_mouseClick { get; set; }
        public BindableCommand profileButton_mouseClick { get; set; }
        public BindableCommand exitButton_mouseClick { get; set; }

        public MainWindowViewModel()
        {
            homeButton_mouseClick = new BindableCommand(_ => GoToHome());
            profileButton_mouseClick = new BindableCommand(_ => GoToProfile());
            exitButton_mouseClick = new BindableCommand(_ => GoToProfile());

            _isEnabledButtonMainPage = true;
        }

        private void GoToHome() => sourceFrame = new Uri("/View/Pages/MainPage.xaml", UriKind.Relative);
        private void GoToProfile() => sourceFrame = new Uri("/View/Pages/ProfilePage.xaml", UriKind.Relative);

        private void Exit() { }

        private Uri _sourceFrame;
        public Uri sourceFrame
        {
            get => _sourceFrame;
            set
            {
                _sourceFrame = value;
                OnPropertyChanged();
            }
        }

        private Brush _backgroundButtonMainPage;

        public Brush backgroundButtonMainPage
        {
            get => _backgroundButtonMainPage;
            set
            {
                _backgroundButtonMainPage = value;
                OnPropertyChanged();
            }
        }

        private bool _isEnabledButtonMainPage;

        public bool isEnabledButtonMainPage
        {
            get => _isEnabledButtonMainPage;
            set
            {
                _isEnabledButtonMainPage = false;
                OnPropertyChanged(nameof(isEnabledButtonMainPage));
            }
        }
    }

}
