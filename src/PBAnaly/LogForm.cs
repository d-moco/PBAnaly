using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace PBAnaly
{
    public partial class LogForm : MaterialForm
    {
        string Inner_UserID;
        public LogForm(MaterialSkinManager materialSkinManager,string UserID)
        {
            InitializeComponent();
            Inner_UserID = UserID;
            UIInit();
            LoadLogToList();
        }

        public MaterialSkinManager Inn_materialSkinManager;

        public void UIInit()
        {
            this.Text = string.Format("Current User : {0}", Inner_UserID);
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            Inn_materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            //Inn_materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            //Inn_materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            //Inn_materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Cyan700, TextShade.WHITE);    // ColorScheme 属性来设置配色方案

            if (Inn_materialSkinManager.Theme == MaterialSkinManager.Themes.DARK)
            {
                Log_dataGridView.DefaultCellStyle.BackColor = Color.DimGray;
            }
            else
            {
                Log_dataGridView.DefaultCellStyle.BackColor = Color.LightGray;
            }
        }

        private void LoadLogToList()
        {
            // Save Log Information
            Read_Write_Log read_Write_Log = new Read_Write_Log();
            string SaveLogFile = read_Write_Log.LogFile;

            List<Log> OldLog = new List<Log>();
            if (File.Exists(SaveLogFile))
            {
                OldLog = read_Write_Log.ReadCsv(SaveLogFile);
            }

            if (OldLog != null && OldLog.Count > 0)
            {
                int count = 0;
                foreach (var item in OldLog)
                {
                    if(Inner_UserID == item.UserID)
                    {
                        Log_dataGridView.Rows.Insert(count, item.ITEM, item.Description, item.Time);
                        count++;
                    }                
                }
            }
        }
    }
}
