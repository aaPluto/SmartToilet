using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using SmartToiletService.ServiceHelper.Asynchronous;

namespace SmartToiletService
{
    public class TCPServers
    {
        private static ServerTCP server;
        private static IPEndPoint localPoint;
        private static AsyncSocketState socket;
        private static System.Timers.Timer timer;
        public static string GetLocalIP()
        {
            try
            {
                string HostName = Dns.GetHostName(); //得到主机名
                IPHostEntry IpEntry = Dns.GetHostEntry(HostName);
                for (int i = 0; i < IpEntry.AddressList.Length; i++)
                {
                 
                    if (IpEntry.AddressList[i].AddressFamily == AddressFamily.InterNetwork)
                    {
                        return IpEntry.AddressList[i].ToString();
                    }
                }
                return "";
            }
            catch (Exception ex)
            {
                return "获取本机IP出错:" + ex.Message.ToString();
            }
        }
        public void ServiceStart()
        {
            localPoint = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 5000);
            server = new ServerTCP(localPoint);
            server.Server.ClientConnected += Server_ClientConnected;
            server.Server.ClientDisconnected += Server_ClientDisconnected;
            server.Server.DataReceived += Server_DataReceived;
            server.Server.CompletedSend += Server_CompletedSend;
            server.Start();
            LogHelpr.Info("已启动");
        }
        /// <summary>
        /// 数据发送完毕事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Server_CompletedSend(object sender,AsyncSocketEventArgs e)
        {
            LogHelpr.Info("数据发送完毕" + DateTime.Now.ToString());
        }
        /// <summary>
        /// 接收到数据事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Server_DataReceived(object sender, AsyncSocketEventArgs e)
        {
            byte[] data = sender as byte[];
            LogHelpr.Info(Encoding.Default.GetString(data));
        }
        /// <summary>
        /// 连接已断开事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Server_ClientDisconnected(object sender,AsyncSocketEventArgs e)
        {
            LogHelpr.Info("连接已断开" + DateTime.Now.ToString());
        }
        /// <summary>
        /// 连接已建立事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        static void Server_ClientConnected(object sender,AsyncSocketEventArgs e)
        {
            LogHelpr.Info("客户端连接成功" + e._state.ClientSocket.RemoteEndPoint.ToString());
            socket = e._state;
            timer = new System.Timers.Timer();
            timer.Elapsed += Timer_Elapsed;
            timer.Interval = 60000000;
            timer.Enabled = true;
        }

        private static int num = 0;

        static void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            num++;
            byte[] n = IntToBytes2(num);
            byte[] sendbyte = n.Concat(new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09,
            0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16, 0x17, 0x18, 0x19,
            0x20, 0x21, 0x22, 0x23, 0x24, 0x25, 0x26, 0x27, 0x28, 0x29 ,
            0x30, 0x31, 0x32, 0x33, 0x34, 0x35, 0x36, 0x37, 0x38, 0x39 }).ToArray();

            server.Server.Send(socket, sendbyte);
        }

        public static byte[] IntToBytes2(int value)
        {
            byte[] src = new byte[4];
            src[0] = (byte)((value >> 24) & 0xFF);
            src[1] = (byte)((value >> 16) & 0xFF);
            src[2] = (byte)((value >> 8) & 0xFF);
            src[3] = (byte)(value & 0xFF);
            return src;
        }
    }
}
