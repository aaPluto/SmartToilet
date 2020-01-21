using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToiletService.BasicHelper
{
    public class SerialPortModel
    {
        static int Lenth = 6;
        //读取App.config获取端口数量
        public SerialPortModel() => Lenth = int.Parse(ConfigurationManager.AppSettings["PortNum"]);
        public static List<PortConfigs> ProconfigList { get; set; } = new List<PortConfigs>();
        public SerialPort[] SerialPorts { get; set; } = new SerialPort[Lenth];
        public class PortConfigs
        {
            public bool Closvalue { get; set; }
            public string Comvalue { get; set; }
        }
    }

}
