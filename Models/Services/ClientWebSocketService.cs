using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

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

        private Uri Uri;

        #region [ Методы ]

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
                await _ClientWebSocket.CloseAsync(WebSocketCloseStatus.Empty, "Клиент завершил сеанс", token);
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
