using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using SmartToiletService;

namespace SmartToiletService
{
    public partial class SmartService : ServiceBase
    {
        private readonly TCPServers tcpServers = new TCPServers();
        DataAnalysis dataAnalysis = new DataAnalysis();
        public SmartService()
        {
            InitializeComponent();
        }
        protected override void OnStart(string[] args)
        {
            tcpServers.ServiceStart();
            dataAnalysis.Start();
        }
        protected override void OnStop()
        {
            dataAnalysis.Stop();
        }
    }
}
