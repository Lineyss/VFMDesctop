using System;
using System.Windows.Controls;
using VFMDesctop.View.Pages;
using VFMDesctop.ViewModels.Help;
using System.Windows.Media;

namespace VFMDesctop.ViewModels.Windows
{
    internal class MainWindowController : propertyChanged
    {
        public MainWindowController()
        {
            
        }

        private Page _frameContent = new MainPage();
        public Page frameContent
        {
            get => _frameContent;
            set
            {
                _frameContent = value;
                OnPropertyChanged();
            }
        }

        private Uri _sourceFrame = new Uri("/View/Pages/MainPage.xaml", UriKind.Relative);
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

        private bool _isEnabledButtonMainPage = false;

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
