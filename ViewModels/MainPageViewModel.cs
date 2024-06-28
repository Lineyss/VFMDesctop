using System;
using System.Windows.Controls;
using VFMDesctop.ViewModels.Help;
using System.Windows.Media;
using System.Net.WebSockets;
using VFMDesctop.Models.Services;
using System.Threading.Tasks;
using System.Threading;

namespace VFMDesctop.ViewModels
{
    internal class MainWindowViewModel : NotifyPropertyChanged
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
        private readonly SolidColorBrush ConnectButton_Background_WebSocketConnecting = new SolidColorBrush(Colors.Green);
        private readonly SolidColorBrush ConnectButton_Background_WebSocketLoading = new SolidColorBrush(Colors.Gray);
        private readonly SolidColorBrush ConnectButton_Background_WebSocketClosed = new SolidColorBrush(Colors.Red);

        private static CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private static CancellationToken cancellationToken = cancellationTokenSource.Token;

        private readonly ClientWebSocketService clientWebSocketService;

        public delegate void WindowCloseEventHendler();
        public event WindowCloseEventHendler OnWindowClose;

        #endregion

        public MainWindowViewModel()
        {
            clientWebSocketService = ClientWebSocketService.Instance();

            clientWebSocketService.OnStatusChange += ClientWebSocketService_OnStatusChange;

            UpdateStatus(clientWebSocketService._ClientWebSocket.State);

            ConnectButton_Click = new BindableCommand(async _ => await _ConnectButton_Click());
            ExitButton_Click = new BindableCommand(_ => _ExitButton_Click());
            Window_Closed = new BindableCommand(async _ => await _Window_Closed());
        }


        #region [ MVVM методы]
        private async Task _Window_Closed()
        {
            await clientWebSocketService.Disconnect(cancellationToken);
        }

        private void _ExitButton_Click() => OnWindowClose?.Invoke();

        private async Task _ConnectButton_Click()
        {
            switch(clientWebSocketService._ClientWebSocket.State)
            {
                case WebSocketState.Open:
                    await clientWebSocketService.Disconnect(cancellationToken);
                    break;
                case WebSocketState.None:
                case WebSocketState.Closed:
                    await clientWebSocketService.Connect(cancellationToken);
                    break;
            }
        }
        #endregion

        #region [ Не MVVM методы ]
        private void ClientWebSocketService_OnStatusChange(WebSocketState status) => UpdateStatus(status);

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
