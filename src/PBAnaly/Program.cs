using System;
using System.Threading;
using System.IO;
using System.Windows.Forms;
using PBAnaly.Module;
using PBAnaly.UI;
using PBAnaly.LoginCommon;
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

            if (Util.ViKeySoft.Instance.CheckViKey() == false) 
            {
                MessageBox.Show("你没有权限，请检查加密狗是否插入","警告");
                return;
            }
#if true
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //数据库操作
            string dbPath = "UserManage.db";
            string connectionString = $"Data Source={dbPath};Version=3;";
            UserManage.ConnectDb();

            var login = new LoginForm();
            login.StartPosition = FormStartPosition.CenterScreen;
            Application.Run(new MainForm());
#endif

        }
    }
}
