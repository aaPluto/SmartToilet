using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
namespace SmartToiletService.ServiceHelper.Asynchronous
{
    public class ServerTCP
    {
        public AsyncSocketTCPServer Server = null;
       
        public ServerTCP(IPEndPoint localPoint)
        {
            Server = new AsyncSocketTCPServer(localPoint);
        }

        public void Start()
        {
            if (Server != null)
            {
                Server.Start();
            }
        }
    }
}
