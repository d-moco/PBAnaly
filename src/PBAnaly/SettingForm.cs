using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace PBAnaly
{
    public partial class SettingForm : MaterialForm
    {
        public SettingForm(MaterialSkinManager materialSkinManager)
        {
            InitializeComponent();
            UIInit();
            Inn_materialSkinManager = materialSkinManager;
        }

        public MaterialSkinManager Inn_materialSkinManager;

        public void UIInit()
        {
            //this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            //Inn_materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            //Inn_materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            //Inn_materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            //Inn_materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Cyan700, TextShade.WHITE);    // ColorScheme 属性来设置配色方案
        }

        private void materialSwitch_UI_CheckedChanged(object sender, EventArgs e)
        {
            if (materialSwitch_UI.Checked == true)
            {
                Inn_materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;   // Theme 属性用来设置整体的主题
                Inn_materialSkinManager.ColorScheme = new ColorScheme(Primary.Purple900, Primary.Purple800, Primary.BlueGrey300, Accent.Purple700, TextShade.WHITE);	// ColorScheme 属性来设置配色方案
            }
            else
            {
                Inn_materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
                Inn_materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Indigo700, TextShade.WHITE);	// ColorScheme 属性来设置配色方案
            }
        }
    }
}
