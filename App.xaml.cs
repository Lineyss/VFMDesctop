using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting; 
using System.Windows;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.Repository;
using VFMDesctop.Models.Services;
using VFMDesctop.View;
using VFMDesctop.View.Pages;
using VFMDesctop.ViewModels;
using VFMDesctop.ViewModels.Pages;
using VFMDesctop.ViewModels.Windows;

namespace VFMDesctop
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost host;

        public App()
        {
            host = Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    ConfigureServices(services);
                })
                .Build();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            #region [ View ]
            services.AddSingleton<MainWindow>();
            services.AddFormFactory<MainPage>();
            services.AddFormFactory<AuthorizationPage>();
            #endregion

            #region [ ViewModel ]
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainPageViewModel>();
            services.AddTransient<AuthorizationPageViewModel>();
            #endregion

            #region [ Services ]
            services.AddSingleton<INavigationService, NavigationService>();

            services.AddTransient<IFileSystemRepository, FileRepository>();
            services.AddTransient<IFileSystemRepository, FolderRepository>();

            services.AddTransient<IWebSocketService, CWSFileSystemService>();
            #endregion
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await host.StartAsync();

            var mainWindow = host.Services.GetRequiredService<MainWindow>();
            mainWindow.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            using(host)
            {
                await host.StopAsync();
            }

            base.OnExit(e);
        }
    }
}
