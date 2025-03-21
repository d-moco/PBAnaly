using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PointCloudDemo
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected  void Application_Startup(object sender,StartupEventArgs e)
        {
           

            string path = "";
            // 获取命令行参数（e.Args 是字符串数组）
            if (e.Args.Length > 0)
            {
                string receivedMessage = e.Args[0];
                path = receivedMessage;
            }
            
            // 启动主窗口
            MainWindow = new MainWindow(path);
            MainWindow.Show();
        }
    }
}
