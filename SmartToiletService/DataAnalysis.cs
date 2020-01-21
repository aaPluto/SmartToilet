using SmartToiletService.BasicHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToiletService
{
    public class DataAnalysis
    {
        SerialPortHelper SerialPort;
        public DataAnalysis()
        {
            List<SerialPortModel.PortConfigs> configList = new List<SerialPortModel.PortConfigs>()
            {
               new SerialPortModel.PortConfigs{ Closvalue = true,Comvalue = "COM1"},
            };
           SerialPort = new SerialPortHelper(configList);
        }
        public void Start()
        {
            SerialPort.OnStart();
            //绑定接收数据事件
            SerialPort.ClientDisconnected += Server_DataReceived;
        }
        public void Stop()
        {
            SerialPort.OnStop();
        }
        //接收数据并解析
        public static void Server_DataReceived(object sender, string Code)
        {
            //解析数据
        }
    }
}
