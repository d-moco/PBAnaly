using System;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using PBAnaly.Module;
using PBAnaly.UI;
namespace PBAnaly
{
    public static class Global
    {
        public static String mDataDirUp = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static String mDataParent = Directory.GetParent(mDataDirUp).FullName;
        public static String mDataUser = mDataParent + "\\PBAnaly\\UserData\\";

        //public static String mDataDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\TYRAFOS\\data\\Blood\\";

    }

    public enum Autholity
    {
        admin, Normal, Visitor
    }

    public class UserInfo
    {
        //0: Admin //1: Normal User //2 :Visitor
        public Autholity autholity{ get; set; }
        public string UserID { get; set; }
        public string E_Mail { get; set; }
        public string Password { get; set; }
    }

    public class Log
    {
        public string UserID { get; set; }
        public string ITEM { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
    }

    internal static class Program
    {
        private static Mutex mutex;
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {


            const string appName = "PBAnaly";
            bool createdNew;

            mutex = new Mutex(true, appName, out createdNew);

            // 如果已经有一个实例在运行，退出程序
            if (!createdNew)
            {
                MessageBox.Show("应用程序已经在运行。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
#if true
            string macAddress =util.GetMotherboardSerial();
            if (macAddress == "PM00P0209N000037" || macAddress == "07D4822_M81D023244" || macAddress == "YQ1711233HY01423" || macAddress == "S312NXCV0056AZMB" || macAddress == "PM82L0235P000452"|| macAddress == "MP2M55J0" 
                    || macAddress == "S936NXCV000SJ2MB"|| macAddress == "S730NXCV009371MB" || macAddress == "/47Z87N3/CNFCW0023303RG/" || true)
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                var login = new LoginForm();
                login.StartPosition = FormStartPosition.CenterScreen;
                Application.Run(login);
                
            }
            else 
            {
                MessageBox.Show("你没有权限");
                return;
            }
#endif

        }
    }
}
