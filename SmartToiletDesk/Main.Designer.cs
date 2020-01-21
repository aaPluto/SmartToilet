namespace SmartToiletService
{
    partial class Main
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

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
            this.ucledDataTimeview = new HZH_Controls.Controls.UCLEDDataTime();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // ucledDataTimeview
            // 
            this.ucledDataTimeview.BackColor = System.Drawing.Color.Transparent;
            this.ucledDataTimeview.Font = new System.Drawing.Font("宋体", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ucledDataTimeview.ForeColor = System.Drawing.SystemColors.ActiveCaption;
            this.ucledDataTimeview.LineWidth = 8;
            this.ucledDataTimeview.Location = new System.Drawing.Point(38, 217);
            this.ucledDataTimeview.Name = "ucledDataTimeview";
            this.ucledDataTimeview.Size = new System.Drawing.Size(650, 58);
            this.ucledDataTimeview.TabIndex = 1;
            this.ucledDataTimeview.Value = new System.DateTime(2020, 1, 15, 15, 42, 49, 636);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.Timer1_Tick);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1904, 1039);
            this.Controls.Add(this.ucledDataTimeview);
            this.Name = "Main";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Main_Load);
            this.ResumeLayout(false);

        }

        #endregion
        private HZH_Controls.Controls.UCLEDDataTime ucledDataTimeview;
        private System.Windows.Forms.Timer timer1;
    }
}

