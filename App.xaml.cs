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
            services.AddFormFactory<AuthorizationPage>();
            services.AddSingleton<MainWindow>();
            services.AddFormFactory<MainPage>();
            #endregion

            #region [ ViewModel ]
            services.AddTransient<AuthorizationPageViewModel>();
            services.AddTransient<MainWindowViewModel>();
            services.AddTransient<MainPageViewModel>();
            #endregion

            #region [ Services ]
            services.AddTransient<IAuthorizationService, AuthorizationService>();
            services.AddTransient<IFileSystemRepository, FolderRepository>();
            services.AddTransient<IWebSocketService, CWSFileSystemService>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient<IFileSystemRepository, FileRepository>();
            services.AddTransient<IFileSystemService, FileSystemService>();
            services.AddTransient<IHttpService, HttpService>();
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
