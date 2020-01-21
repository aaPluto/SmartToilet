using SmartToiletService.BasicHelper;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SmartToiletService
{
    class SerialPortHelper
    {
        SerialPortModel SerialPortModel = new SerialPortModel();
        /// <summary>
        /// 检测频率【检测等待时间，毫秒】
        /// </summary>
        readonly int _jcpl = 100;

        /// <summary>
        /// 保持读取开关
        /// </summary>
        private  bool _keepReading = true;
        /// <summary>
        /// 读取线程初始化
        /// </summary>
        Thread[] Readthread = new Thread[int.Parse(ConfigurationManager.AppSettings["PortNum"])];
        /// <summary>
        /// 实例化端口
        /// </summary>
        /// <param name="keepReading">是否</param>
        /// <param name="PortName"></param>
        public SerialPortHelper(List<SerialPortModel.PortConfigs> Configs)
        {
            //存储记录
            SerialPortModel.ProconfigList = Configs;
            for (int i = 0; i < Configs.Count; i++)
            {
                //是否开关读取数据的线程                                                                                                                                                                                                           
                _keepReading = Configs.FirstOrDefault().Closvalue;
                //实例化并赋值给SerialPortModel中的串口数组
                SerialPortModel.SerialPorts[i] = new SerialPort
                {
                    DtrEnable = true,
                    RtsEnable = true,
                    //波特率
                    BaudRate = 9600,
                    //数据位
                    DataBits = 8,
                    //端口
                    PortName = Configs.FirstOrDefault().Comvalue,
                    //停止位
                    StopBits = StopBits.One,
                    //奇偶校验
                    Parity = Parity.Even
                };
                //提前关闭一下
                SerialPortModel.SerialPorts[i].Close();
            }
        }

        /// <summary>
        /// 开启串口通信
        /// </summary>
        public void OnStart()
        {
            for (int i = 0; i < SerialPortModel.ProconfigList.Count(); i++)
            {
                if (SerialPortModel.SerialPorts[i].IsOpen)
                {
                    SerialPortModel.SerialPorts[i].Close();
                }
                SerialPortModel.SerialPorts[i].Open();
                Readthread[i] = new Thread(new ParameterizedThreadStart(SerialPortRead))
                {
                    IsBackground = true
                };
                Readthread[i].Start(SerialPortModel.SerialPorts[i]);
            }
        }
        public void OnStop()
        {
            for (int i = 0; i < SerialPortModel.ProconfigList.Count(); i++)
            {
                if (SerialPortModel.SerialPorts[i].IsOpen)
                {
                    SerialPortModel.SerialPorts[i].Close();
                    Readthread[i].Abort();
                }
            }
        }
        /// <summary>
        /// 发送数据
        /// </summary>
        /// <param name="Code"></param>
        public void Sent(string Code,int PortIndex)
        {
            try
            {
                byte[] sendData = null;
                sendData = ConvertHelper.StrToHexByte(Code);
                //写入数据
                if (SerialPortModel.SerialPorts[PortIndex].IsOpen)
                {
                    SerialPortModel.SerialPorts[PortIndex].Write(sendData, 0, sendData.Length);
                }
            }
            catch (Exception ex)
            {
                LogHelpr.Error("错误:" + ex.ToString());
            }
        }
        /// <summary>
        /// 读取串口数据
        /// </summary>
        public void SerialPortRead(object _serialPort)
        {
            SerialPort serialPort = (SerialPort)_serialPort;
            while (_keepReading)
            {
                //间隔时间
                if (_jcpl > 0)
                {
                    Thread.Sleep(_jcpl);
                }
                //2秒后重新检测  串口对象未实例化
                if (serialPort == null)
                {
                    Thread.Sleep(2000);
                    continue;
                }
                //串口未打开
                if (!serialPort.IsOpen)
                {
                    Thread.Sleep(2000);
                    continue;
                }
                try
                {
                    #region 字节读取
                    byte[] readBuffer = new byte[serialPort.BytesToRead];
                    int count = serialPort.Read(readBuffer, 0, readBuffer.Length);
                    if (count != 0)
                    {
                        //转为16进制字符
                        string Code = ConvertHelper.ByteToHexStr(readBuffer);
                        //触发接收数据事件
                        ResolveDevdisplay(serialPort, Code);
                    }
                    #endregion
                }
                catch (TimeoutException)
                {
                    LogHelpr.Info("串口读取超时");
                }
                catch (Exception ex)
                {
                    LogHelpr.Info("串口读取发生错误:"+ex.ToString());
                }
            }
        }
        public event EventHandler<string> ClientDisconnected;
        private void ResolveDevdisplay(object sender,string z_Code)
        {
            ClientDisconnected?.Invoke(sender, z_Code);
        }
    }
}
