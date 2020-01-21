namespace SmartToiletService
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
            this.SmartToiletServices = new System.ServiceProcess.ServiceProcessInstaller();
            this.SmartToileServices_COT = new System.ServiceProcess.ServiceInstaller();
            // 
            // SmartToiletServices
            // 
            this.SmartToiletServices.Account = System.ServiceProcess.ServiceAccount.LocalSystem;
            this.SmartToiletServices.Password = null;
            this.SmartToiletServices.Username = null;
            // 
            // SmartToileServices_COT
            // 
            this.SmartToileServices_COT.Description = "设备控制基础服务";
            this.SmartToileServices_COT.ServiceName = "SmartService";
            // 
            // ProjectInstaller
            // 
            this.Installers.AddRange(new System.Configuration.Install.Installer[] {
            this.SmartToiletServices,
            this.SmartToileServices_COT});

        }

        #endregion

        private System.ServiceProcess.ServiceProcessInstaller SmartToiletServices;
        private System.ServiceProcess.ServiceInstaller SmartToileServices_COT;
    }
}