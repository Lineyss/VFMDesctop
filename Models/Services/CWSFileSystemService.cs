using Newtonsoft.Json.Bson;
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
using VFMDesctop.Events;
using VFMDesctop.Models.Interfaces;
using VFMDesctop.Models.ReceiveModels;
using VFMDesctop.Models.ResponceModels;

namespace VFMDesctop.Models.Services
{
    internal class CWSFileSystemService : IWebSocketService
    {
        private CancellationTokenSource cancellationTokenSource;
        private CancellationToken cancellationToken;
        private Uri WebSocketIp = new Uri("asdadsasd");
        private ClientWebSocket clientWebSocket;
        
        public CWSFileSystemService()
        {
            clientWebSocket = new ClientWebSocket();

            cancellationTokenSource = new CancellationTokenSource();
            cancellationToken = cancellationTokenSource.Token;
        }

        public event WebSocketStatusChangeEventHendler StatusChanged;

        public async Task Connect()
        {
            try
            {
                await clientWebSocket.ConnectAsync(WebSocketIp, cancellationToken);
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
                clientWebSocket = new ClientWebSocket();
            }
            finally
            {
                StatusChanged.Invoke();
            }
        }

        public async Task Disconnect()
        {
            await clientWebSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Клиент прeкротил соединение", cancellationToken);
            cancellationTokenSource.Cancel();
        }

        public WebSocketState GetWebSocketStatus() => clientWebSocket.State;

        public async Task<string> Receive()
        {
            try
            {
                ArraySegment<byte> buffer = new ArraySegment<byte>(new byte[1024]);
                WebSocketReceiveResult result = await clientWebSocket.ReceiveAsync(buffer, cancellationToken);

                string message = Encoding.UTF8.GetString(buffer.Array, 0, result.Count);

                return message;
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                StatusChanged.Invoke();
            }

            return string.Empty;
        }

        public async Task Responce(string message)
        {
            try
            {
                byte[] buffer = Encoding.UTF8.GetBytes(message);
                await clientWebSocket.SendAsync(new ArraySegment<byte>(buffer), WebSocketMessageType.Binary, true, cancellationToken);

            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message);
            }
            finally
            {
                StatusChanged.Invoke();
            }
        }
    }
}

