using Cjwdev.WindowsApi;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Daemon_SmartToilet
{
    public partial class Daemon_SmartToilet : ServiceBase
    {
        public Daemon_SmartToilet()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 检查间隔
        /// </summary>
        private static readonly int _timerInterval = Convert.ToInt32(ConfigurationManager.AppSettings["timerInterval"]) * 1000;
        /// <summary>
        /// 要守护的服务名
        /// </summary>
        private static readonly string toWatchServiceName = ConfigurationManager.AppSettings["toWatchServiceName"];        
        /// <summary>
        /// 要守护的应用程序名称
        /// </summary>
        private static readonly string toWatchAppName = ConfigurationManager.AppSettings["toWatchAppName"];
        /// <summary>
        /// 应用程序所在文件夹
        /// </summary>
        private static readonly string FilePath = ConfigurationManager.AppSettings["FilePath"];
        private System.Timers.Timer _timer;
        protected override void OnStart(string[] args)
        {
            //服务启动时开启定时器
            _timer = new System.Timers.Timer
            {
                Interval = _timerInterval,
                Enabled = true,
                AutoReset = true
            };
            _timer.Elapsed += Timer_Elapsed;
            _timer.Elapsed += Timer_AppSmart;
            LogHelpr.Info("守护服务开启");
        }
        protected override void OnStop()
        {
            if (_timer != null)
            {
                _timer.Stop();
                _timer.Dispose();
                LogHelpr.Info("守护服务停止");
            }
        }
        //监测服务运行状态
        void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //如果服务状态为停止，则重新启动服务
            if (!CheckSericeStart(toWatchServiceName))
            {
                StartService(toWatchServiceName);
            }
        }
        /// <summary>
        /// 启动服务
        /// </summary>
        /// <param name="serviceName">要启动的服务名称</param>
        private void StartService(string serviceName)
        {
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName.Trim() == serviceName.Trim())
                    {
                        service.Start();
                        //直到服务启动
                        service.WaitForStatus(ServiceControllerStatus.Running, new TimeSpan(0, 0, 30));
                        LogHelpr.Info(string.Format("启动服务:{0}", serviceName));
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelpr.Error(ex.ToString());
            }
        }
        private bool CheckSericeStart(string serviceName)
        {
            bool result = true;
            try
            {
                ServiceController[] services = ServiceController.GetServices();
                foreach (ServiceController service in services)
                {
                    if (service.ServiceName.Trim() == serviceName.Trim())
                    {
                        if ((service.Status == ServiceControllerStatus.Stopped)
                            || (service.Status == ServiceControllerStatus.StopPending))
                        {
                            result = false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelpr.Error(ex.ToString());
            }
            return result;
        }
        /// <summary>
        /// 监测应用程序运行状态
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Timer_AppSmart(object sender, System.Timers.ElapsedEventArgs e)
        {

            if (Process.GetProcessesByName(toWatchAppName).ToList().Count < 1)

            {
                LogHelpr.Info("监测到应用程序已经关闭......");
                LogHelpr.Info("正在重新启动应用程序......");
                AppStart(@FilePath+toWatchAppName + ".exe");
            }
        }
        /// <summary>
        /// 打开应用程序
        /// </summary>
        /// <param name="appPath"></param>
        public static void AppStart(string appPath)
        {
            try
            {
                string appStartPath = appPath;
                IntPtr userTokenHandle = IntPtr.Zero;
                ApiDefinitions.WTSQueryUserToken(ApiDefinitions.WTSGetActiveConsoleSessionId(), ref userTokenHandle);

                ApiDefinitions.PROCESS_INFORMATION procInfo = new ApiDefinitions.PROCESS_INFORMATION();
                ApiDefinitions.STARTUPINFO startInfo = new ApiDefinitions.STARTUPINFO();
                startInfo.cb = (uint)Marshal.SizeOf(startInfo);

                ApiDefinitions.CreateProcessAsUser(
                    userTokenHandle,
                    appStartPath,
                    "",
                    IntPtr.Zero,
                    IntPtr.Zero,
                    false,
                    0,
                    IntPtr.Zero,
                    null,
                    ref startInfo,
                    out procInfo);

                if (userTokenHandle != IntPtr.Zero)
                    ApiDefinitions.CloseHandle(userTokenHandle);

                int _currentAquariusProcessId = (int)procInfo.dwProcessId;
            }
            catch (Exception ex)
            {
                LogHelpr.Error("打开程序时出错：" + ex.ToString());
            }
        }
        }
    }
