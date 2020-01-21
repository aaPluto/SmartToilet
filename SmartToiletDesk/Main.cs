using log4net.Config;
using SmartToiletDesk;
using SmartToiletDesk.DeskSoketHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace SmartToiletService
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            //如果不把Border设为None,则无法隐藏Windows的开始任务栏
            FormBorderStyle = FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;
             //如果不允许运行其他程序,则可设为True,屏蔽其他窗体的显示
             //但必须确保自身所有的窗体的TopMost除了子窗体外都要设置为true,否则也同样会被屏蔽
            TopMost = true;

        }
        private static ClientTCP client;
        private void Main_Load(object sender, EventArgs e)
        {
            string assemblyFilePath = Assembly.GetExecutingAssembly().Location;
            string assemblyDirPath = Path.GetDirectoryName(assemblyFilePath);
            string configFilePath = assemblyDirPath + "\\log4net.xml";
            XmlConfigurator.Configure(new FileInfo(configFilePath));
            client = new ClientTCP();
            client.Connect("127.0.0.1", 5000);
            LogHelpr.Info("客户端已启动");
        }

        private void Timer1_Tick(object sender, EventArgs e)
        {
            ucledDataTimeview.Value = DateTime.Now;
        }
    }
}
