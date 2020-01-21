namespace Daemon_SmartToilet
{
    partial class ProjectInstaller
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.Daemon_SmartToiletInstaller = new System.ServiceProcess.ServiceProcessInstaller();
            this.Daemon_SmartToilet = new System.ServiceProcess.ServiceInstaller();
            // 
            // Daemon_SmartToiletInstaller
            // 
            this.Daemon_SmartToiletInstaller.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.Daemon_SmartToiletInstaller.Password = null;
            this.Daemon_SmartToiletInstaller.Username = null;
            // 
            // Daemon_SmartToilet
            // 
            this.Daemon_SmartToilet.Description = "智慧厕所守护服务";
            this.Daemon_SmartToilet.ServiceName = "Daemon_SmartToilet";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.Daemon_SmartToiletInstaller,
            this.Daemon_SmartToilet});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller Daemon_SmartToiletInstaller;
        private System.ServiceProcess.ServiceInstaller Daemon_SmartToilet;
    }
}