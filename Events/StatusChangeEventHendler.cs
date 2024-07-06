using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace VFMDesctop.Events
{
    public delegate void StatusChangeEventHendler(WebSocketState status);
}
