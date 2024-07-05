using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Services
{
    internal class ClientWebSocketService
    {

        #region [ Singleton мешура ]
        private static ClientWebSocketService _ClientWebSocketService { get; set; }
        private ClientWebSocketService()
        {
            _ClientWebSocket = new ClientWebSocket();
        }

        public static ClientWebSocketService Instance()
        {
            _ClientWebSocketService = _ClientWebSocketService ?? new ClientWebSocketService();
            return _ClientWebSocketService;
        }
        #endregion


        #region [ События и делегаты для них ]

        public delegate void StatusChangeEventHendler(WebSocketState status);
        public event StatusChangeEventHendler OnStatusChange;

        #endregion

        public readonly ClientWebSocket _ClientWebSocket;

        private Uri Uri = new Uri("ws://localhost:5285/WebSocket");

        #region [ Методы ]

        private async Task SendAsync(ResponceFileSystemElement element)
        {
            string json = JsonConverterServices.Serialize(element);

            var buffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(json));
            await _ClientWebSocket.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
        }

        private async Task<ResponceFileSystemElement> ReceiveAsync()
        {
            byte[] buffer = new byte[1024];
            if (_ClientWebSocket.State == WebSocketState.Open)
            {
                WebSocketReceiveResult result = await _ClientWebSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    await _ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "", CancellationToken.None);
                }
                else
                {
                    string receivedMessage = Encoding.UTF8.GetString(buffer, 0, result.Count);

                    try
                    {
                        ResponceFileSystemElement element = JsonConverterServices.Deserialize<ResponceFileSystemElement>(receivedMessage);
                        return element;
                    }
                    catch
                    {
                        return null;
                    }
                }
            }

            return null;
        }

        public async Task Connect(CancellationToken token)
        {
            try
            {
                await _ClientWebSocket.ConnectAsync(Uri, token);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                OnStatusChange?.Invoke(_ClientWebSocket.State);
            }
        }

        public async Task Disconnect(CancellationToken token)
        {
            try
            {
                await _ClientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Клиент завершил сеанс", token);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                OnStatusChange?.Invoke(_ClientWebSocket.State);
            }
        }

        #endregion
    }
}
