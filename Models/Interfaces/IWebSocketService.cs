using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;
using VFMDesctop.Events;
using VFMDesctop.Models.ReceiveModels;

namespace VFMDesctop.Models.Interfaces
{
    internal interface IWebSocketService
    {
        event WebSocketStatusChangeEventHendler StatusChanged;
        Task Connect();
        Task Disconnect();
        Task<string> Receive();
        Task Responce(string message);
        WebSocketState GetWebSocketStatus();
    }
}
