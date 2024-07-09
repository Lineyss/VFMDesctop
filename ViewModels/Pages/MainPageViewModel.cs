using VFMDesctop.ViewModels.Help;
using System.Windows.Media;
using System.Net.WebSockets;
using VFMDesctop.Models.Services;
using System.Threading.Tasks;
using System.Threading;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.View.Pages;
using VFMDesctop.Models.ReceiveModels;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.ViewModels
{
    public class MainPageViewModel : NotifyPropertyChanged
    {
        #region [ MVVM свойств]

        private Brush _ButtonConnect_Background;
        public Brush ButtonConnect_Background
        {
            get => _ButtonConnect_Background;
            set
            {
                _ButtonConnect_Background = value;
                OnPropertyChanged();
            }
        }

        private string _ConnectionStatus_Text;
        public string ConnectionStatus_Text
        {
            get => _ConnectionStatus_Text;
            set
            {
                _ConnectionStatus_Text = value;
                OnPropertyChanged();
            }
        }

        public BindableCommand ConnectButton_Click { get; set; }
        public BindableCommand ExitButton_Click { get; set; }
        public BindableCommand Window_Closed { get; set; }

        #endregion

        #region [ Не MVVM свойста ]
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

        private readonly SolidColorBrush ConnectButton_Background_WebSocketConnecting = new SolidColorBrush(Colors.Green);
        private readonly SolidColorBrush ConnectButton_Background_WebSocketLoading = new SolidColorBrush(Colors.Gray);
        private readonly SolidColorBrush ConnectButton_Background_WebSocketClosed = new SolidColorBrush(Colors.Red);

        private readonly IFactory<AuthorizationPage> factoryAuthPage;
        private readonly INavigationService navigationService;
        private readonly IWebSocketService webSocketService;
        private readonly IFileSystemService fileSystemService;
        #endregion

        public MainPageViewModel(INavigationService navigationService,
                   IFactory<AuthorizationPage> factoryAuthPage,
                   IFileSystemService fileSystemService,
                   IWebSocketService webSocketService)
        {
            this.navigationService = navigationService;
            this.factoryAuthPage = factoryAuthPage;
            this.webSocketService = webSocketService;
            this.fileSystemService = fileSystemService;

            this.webSocketService.StatusChanged += WebSocketService_StatusChanged;

            ConnectButton_Click = new BindableCommand(async _ => await _ConnectButton_Click());
            ExitButton_Click = new BindableCommand(_ => _ExitButton_Click());

            UpdateStatus(webSocketService.GetWebSocketStatus());
        }

        ~MainPageViewModel()
        {
            if(webSocketService.GetWebSocketStatus() == WebSocketState.Open)
                webSocketService.Disconnect();
        }

        private void WebSocketService_StatusChanged() => UpdateStatus(webSocketService.GetWebSocketStatus());

        #region [ MVVM методы]

        private void _ExitButton_Click()
        {
            navigationService.SetNavigate(factoryAuthPage.Create());
            webSocketService.Disconnect();
        }

        private async Task _ConnectButton_Click()
        {
            switch (webSocketService.GetWebSocketStatus())
            {
                case WebSocketState.Open:
                    await webSocketService.Disconnect();
                    break;
                case WebSocketState.None:
                case WebSocketState.Closed:
                    await webSocketService.Connect();
                    break;
            }
        }
        #endregion

        #region [ Не MVVM методы ]

        private async Task ReceiveMessage(CancellationToken token)
        {
            while(!token.IsCancellationRequested)
            {
                try
                {
                    string json = await webSocketService.Receive();
                    Receive receive = JsonConverterServices.Deserialize<Receive>(json);

                    switch(receive.Action)
                    {
                        case("POST"):
                            var elementsPost = fileSystemService.Create(receive.Path);
                            await ResponceMessage(new Responce
                            {
                                Error = elementsPost.Item2,
                                fileSystemElement = elementsPost.Item1
                            });
                            break;
                        case("GET"):
                            var elementsGet = fileSystemService.Open(receive.Path);
                            await ResponceMessage(new Responce
                            {
                                Error = elementsGet.Item2,
                                fileSystemElement = elementsGet.Item1
                            });
                            break;
                        case ("PUT"):
                            var elementsPut = fileSystemService.Update(receive.UpdateName ,receive.Path);
                            await ResponceMessage(new Responce
                            {
                                Error = elementsPut.Item2,
                                fileSystemElement = elementsPut.Item1
                            });
                            break;
                        case ("DELETE"):
                            var elementsDelete = fileSystemService.Delete(receive.Path);
                            await ResponceMessage(new Responce
                            {
                                Error = elementsDelete.Item2,
                                fileSystemElement = elementsDelete.Item1
                            });
                            break;
                    }
                }
                catch
                {

                }
            }
        }

        private async Task ResponceMessage(Responce responceFileSystemElement)
        {
            string jsonMessage = JsonConverterServices.Serialize(responceFileSystemElement);

            await webSocketService.Responce(jsonMessage);
        }

        private void UpdateStatus(WebSocketState status)
        {
            switch (status)
            {
                case WebSocketState.Open:
                    ButtonConnect_Background = ConnectButton_Background_WebSocketConnecting;
                    ConnectionStatus_Text = "On";
                    break;
                case WebSocketState.None:
                case WebSocketState.Closed:
                    ButtonConnect_Background = ConnectButton_Background_WebSocketClosed;
                    ConnectionStatus_Text = "Off";
                    cancellationTokenSource.Cancel();
                    break;
                default:
                    ButtonConnect_Background = ConnectButton_Background_WebSocketLoading;
                    ConnectionStatus_Text = "Loading";
                    break;
            }
        }
        #endregion
    }
}
