using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartToileConfigDev
{
    class SmartModel
    {
        public class Basicconfig
        {
            public int ID { get; set; }
            /// <summary>
            /// 端口个数
            /// </summary>
            public int ComNum { get; set; }
            /// <summary>
            /// 节点个数
            /// </summary>
            public int IoNode { get; set; }
            /// <summary>
            /// IO模块个数
            /// </summary>
            public int IoNum { get; set; }
            /// <summary>
            /// 端口使用个数
            /// </summary>
            public int UseNum { get; set; }
        }
        public class Comact
        {
            public int ID { get; set; }
            /// <summary>
            /// COM口地址
            /// </summary>
            public string ComAddr { get; set; }
            /// <summary>
            /// COM口功能
            /// </summary>
            public string ComAction { get; set; }
        }
        public class UseioDevice
        {
            public int ID { get; set; }
            /// <summary>
            /// 设备地址
            /// </summary>
            public string DeviceAddr { get; set; }
            /// <summary>
            /// IO模块地址
            /// </summary>
            public string IOAddr { get; set; }
            /// <summary>
            /// COM口ID
            /// </summary>
            public int ComId { get; set; }
            /// <summary>
            /// 性别 
            /// </summary>
            public string Sex { get; set; }
        }
    }
}
