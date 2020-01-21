using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CCWin;
using static SmartToileConfigDev.SmartModel;

namespace SmartToileConfigDev
{
    public partial class Main : Skin_Color
    {
        public Main()
        {
            InitializeComponent();
        }
        /// <summary>
        /// 加载表格并加载数据，为下拉框赋初始值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Main_Load(object sender, EventArgs e)
        {
            //第一个列表 skinDataGridView1
            ComNum.SelectedIndex = 0;
            IoNum.SelectedIndex = 0;
            UseNum.SelectedIndex = 0;
            IoNode.SelectedIndex = 0;
            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn
            {
                Name = "ComAddr",
                DataPropertyName = "ComAddr",//对应数据源的字段
                HeaderText = "端口地址",
                Width = 80
            };
            skinDataGridView1.Columns.Add(column);
            List<string> ListDates = new List<string> { "COM1", "COM2","COM3","COM4","COM5","COM6"};
            column.DataSource = ListDates;
            DataGridViewComboBoxColumn column1 = new DataGridViewComboBoxColumn
            {
                Name = "ComAction",
                DataPropertyName = "ComAction",//对应数据源的字段
                HeaderText = "端口功能",
                Width = 80
            };
            skinDataGridView1.Columns.Add(column1);
            List<string> ListData = new List<string> { "IO模块", "空气检测器", "空气净化器"," "};
            column1.DataSource = ListData;
            //第二个列表 skinDataGridView2

            DataGridViewComboBoxColumn Datecolumn1 = new DataGridViewComboBoxColumn
            {
                Name = "ComId",
                DataPropertyName = "ComId",//对应数据源的字段
                HeaderText = "IO模块",
                Width = 80
            };
            skinDataGridView2.Columns.Add(Datecolumn1);
            List<string> ListDev1 = new List<string> { "IO1", "IO2"};
            Datecolumn1.DataSource = ListDev1;
            DataGridViewComboBoxColumn Datecolumn12 = new DataGridViewComboBoxColumn
            {
                Name = "IOAddr",
                DataPropertyName = "IOAddr",//对应数据源的字段
                HeaderText = "节点",
                Width = 80
            };
            skinDataGridView2.Columns.Add(Datecolumn12);
            List<string> ListDev12 = new List<string> { "0001", "0002", "0003", "0004" };
            Datecolumn12.DataSource = ListDev12;
            DataGridViewComboBoxColumn Datecolumn13 = new DataGridViewComboBoxColumn
            {
                Name = "DeviceAddr",
                DataPropertyName = "DeviceAddr",//对应数据源的字段
                HeaderText = "厕位",
                Width = 80
            };
            skinDataGridView2.Columns.Add(Datecolumn13);
            List<string> ListDev13 = new List<string> { "1号", "2号", "3号", "4号" };
            Datecolumn13.DataSource = ListDev13;
            DataGridViewComboBoxColumn Datecolumnl4 = new DataGridViewComboBoxColumn
            {
                Name = "Sex",
                DataPropertyName = "Sex",//对应数据源的字段
                HeaderText = "性别",
                Width = 80
            };
            skinDataGridView2.Columns.Add(Datecolumnl4);
            List<string> ListDev14 = new List<string> { "男", "女"};
            Datecolumnl4.DataSource = ListDev14;

        }
        private DataTable CreateTable(List<Comact> comacts)
        {
            DataTable dt = new DataTable();                         //创建一个空表
            dt.Columns.Add(new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "ComAddr"
            });
            dt.Columns.Add(new DataColumn
            {
                DataType = Type.GetType("System.String"),
                ColumnName = "ComAction"
            });
            foreach (var item in comacts)
            {
                DataRow row = dt.NewRow();                              //创建行
                row["ComAddr"] = item.ComAddr;
                row["ComAction"] =item.ComAction;
                dt.Rows.Add(row);
            }
            return dt;
        }
        /// <summary>
        /// 端口配置保存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinButton1_Click(object sender, EventArgs e)
        {
            Basicconfig basicconfig = new Basicconfig()
            {
                ComNum = int.Parse(ComNum.Text),
                IoNode = int.Parse(IoNode.Text),
                IoNum = int.Parse(IoNum.Text),
                UseNum = int.Parse(UseNum.Text)
            };
            List<Comact> comacts = new List<Comact>();
            for (int i = 1; i <= basicconfig.UseNum; i++)
            {
                comacts.Add(new Comact()
                {
                    ComAddr = "COM" + i.ToString(),
                    ComAction = i == 1 ? "IO模块" : i == 2 && basicconfig.IoNum == 2 ? "IO模块" : i == 2 ? "空气检测器" : i == 3 && basicconfig.IoNum == 2 ? "空气检测器" : "空气净化器"
                });
            }
            skinDataGridView1.DataSource = CreateTable(comacts);
        }
        /// <summary>
        /// 生成配置 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SkinButton3_Click(object sender, EventArgs e)
        {
            int mannum = int.Parse(ManNum.Text);
            int womannum = int.Parse(WomanNum.Text);
            //如果用一个IO模块，则男女测试分开对应
            if (int.Parse(IoNum.Text)<2)
            {
                
            }
        }
    }
}
