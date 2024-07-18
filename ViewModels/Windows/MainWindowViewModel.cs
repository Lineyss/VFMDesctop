using System;
using System.Windows;
using System.Windows.Controls;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.Services;
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

        public BindableCommand Close_Click { get; set; }
        public BindableCommand Minimize_Click { get; set; }
        public BindableCommand ToolBar_MouseDown { get; set; }
        public BindableCommand ChangeTheme_Click { get; set; }

        private readonly INavigationService navigationService;

        public MainWindowViewModel(INavigationService navigationService,
            IFactory<AuthorizationPage> factoryAuthPage)
        {
            this.navigationService = navigationService;
            SourceNavigate = factoryAuthPage.Create();
            
            navigationService.NavigationChange += NavigationService_NavigationChange;

            Close_Click = new BindableCommand(_ => CloseWindow());
            Minimize_Click = new BindableCommand(_ => MinimizeWindow());
            ToolBar_MouseDown = new BindableCommand(_ => DragMoveWindow());
            ChangeTheme_Click = new BindableCommand(_ => ChangeTheme());
        }

        private void CloseWindow() => Application.Current.Shutdown();
        private void MinimizeWindow() => Application.Current.MainWindow.WindowState = WindowState.Minimized;
        private void DragMoveWindow() => Application.Current.MainWindow.DragMove();
        private void ChangeTheme()
        {
            switch(SettingService.CurrentTheme)
            {
                case "Ligth":
                    SettingService.CurrentTheme = "Dark";
                    break;
                case "Dark":
                    SettingService.CurrentTheme = "Ligth";
                    break;
            }

            Application.Current.Resources.MergedDictionaries.RemoveAt(0);
            Application.Current.Resources.MergedDictionaries.Insert(0, new ResourceDictionary
            {
                Source = new Uri($"/Themes/{SettingService.CurrentTheme}Theme.xaml", UriKind.Relative)
            });
        }
        private void NavigationService_NavigationChange() => SourceNavigate = navigationService.GetNavigate();
    }
}
