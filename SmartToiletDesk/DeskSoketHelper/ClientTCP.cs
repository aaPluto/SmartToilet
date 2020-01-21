using SmartToiletDesk.DeskSoketHelper.AsyncSocket;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToiletDesk.DeskSoketHelper
{
    class ClientTCP
    {
        public AsyncSocketComm Client = null;
        public ClientTCP()
        {
            Client = new AsyncSocketComm();
            Client.ClientConnected += Client_ClientConnected;
            Client.DataReceived += Client_DataReceived;
            Client.DataSend += Client_DataSend;
            Client.SocketException += Client_OnSocketException;
        }
        void Client_OnSocketException(object sender, AsyncTCPClientEventArgs e)
        {
            LogHelpr.Info("远程服务器已断开连接");
        }

        void Client_DataSend(object sender, AsyncTCPClientEventArgs e)
        {

        }

        void Client_DataReceived(object sender, AsyncTCPClientEventArgs e)
        {
            byte[] data = sender as byte[];
            Console.WriteLine(System.Text.Encoding.Default.GetString(data));

            //System.Threading.Thread.Sleep(200);
        }

        void Client_ClientConnected(object sender, AsyncTCPClientEventArgs e)
        {
            Console.WriteLine("客户端连接成功," + "客户端地址：" + e._state.LocalEndPoint.ToString() + "  服务器地址：" + e._state.RemoteEndPoint.ToString());
            Client.SendData("16514651651");
        }

        public void Connect(string ip, int port)
        {
            Client.BuildServerSocket(ip, port);
            Client.MaxBufLen = 48;
        }
    }
}
