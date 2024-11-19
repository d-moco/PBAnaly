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
using Microsoft.VisualBasic.FileIO;

namespace PBAnaly
{
    public partial class LoginForm : MaterialForm
    {
        private MaterialSkinManager materialSkinManager;
        private SignInForm signInForm;
        private MainForm mainForm;

        public LoginForm()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Indigo700, TextShade.WHITE);    // ColorScheme 属性来设置配色方案

            signInForm = null;
            mainForm = null;

            string LoginFile = Global.mDataUser + "UserLogin.csv";

            if (File.Exists(LoginFile))
                ReadCsv_Per(LoginFile);

            this.FormClosing += LoginForm_FormClosing;
        }

        private void userName_materialTextBox_Click(object sender, EventArgs e)
        {
            userName_materialTextBox.Text = "";
        }

        private void password_materialTextBox_Click(object sender, EventArgs e)
        {
            password_materialTextBox.Text = "";
            password_materialTextBox.PasswordChar = '*';
            password_materialTextBox.UseSystemPasswordChar = true;
        }

        private void SIGNIN_materialButton_Click(object sender, EventArgs e)
        {
            if (signInForm == null)
            {
                signInForm = new SignInForm(this);
                signInForm.FormClosing += SignInForm_FormClosing;
                signInForm.TopMost = true;
                signInForm?.Show();

                this.Visible = false;
                //this.Close();
            }
        }

        private void SignInForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            signInForm?.Dispose();
            signInForm = null;
        }

        private void LoginForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this?.Dispose();
            this.Close();
        }

        private void Login_materialButton_Click(object sender, EventArgs e)
        {
            bool LoginSuccess = true;

            string userName_str = userName_materialTextBox.Text;
            string password_str = password_materialTextBox.Text;

            List<UserInfo> loginUser = new List<UserInfo>();

            if (string.IsNullOrEmpty(userName_str) || string.IsNullOrEmpty(password_str))
            {
                MessageBox.Show("User ID or Password is empty ,Please Check!!", "Login Error");
                LoginSuccess = false;
            }

            if (!LoginSuccess)
            {
                return;
            }
            else
            {
                if (userName_str.Equals("admin") && password_str.Equals("000000"))
                {
                    Autholity autholity = Autholity.admin;
                    if (mainForm == null)
                    {
                        mainForm = new MainForm(this, autholity, "admin");
                        mainForm.TopMost = true;
                        mainForm?.Show();
                        this.Close();
                    }
                }
                else
                {
                    string FileName = Global.mDataUser + "UserInfo.csv";

                    if (!File.Exists(FileName))
                    {
                        MessageBox.Show("No signin User Data, please sign in first!!", "Login Error");
                        LoginSuccess = false;
                    }
                    else
                    {
                        var GetCurrentUserInfo = ReadCsv(FileName);

                        bool Find_Str = false;
                        bool Find_Password = false;

                        string Find_UserID = "";
                        Autholity Find_autholity = Autholity.Normal;

                        foreach (var item in GetCurrentUserInfo)
                        {
                            if (item.UserID.Equals(userName_str))
                            {
                                Find_Str = true;

                                if (item.Password.Equals(password_str))
                                {
                                    Find_Password = true;
                                    Find_UserID = item.UserID;
                                    Find_autholity = item.autholity;

                                    loginUser.Add(item);
                                }
                            }

                        }

                        if (!Find_Str && !Find_Password)
                        {
                            MessageBox.Show("This User ID hasn't been registered, please sign in first!!", "Login Error");
                            LoginSuccess = false;
                        }
                        else if (Find_Str && !Find_Password)
                        {
                            MessageBox.Show("Wrong ID or Password, please check!!", "Login Error");
                            LoginSuccess = false;
                        }
                        else if (!Find_Str && Find_Password)
                        {
                            MessageBox.Show("Wrong ID or Password, please check!!", "Login Error");
                            LoginSuccess = false;
                        }
                        else
                        {
                            LoginSuccess = true;
                        }

                        if (!LoginSuccess)
                        {
                            return;
                        }
                        else
                        {
                            //Login Success

                            //Save Login Information
                            string LoginFile = Global.mDataUser + "UserLogin.csv";
                            if (!File.Exists(FileName))
                            {
                                if (!Directory.Exists(Global.mDataUser))
                                {
                                    Directory.CreateDirectory(Global.mDataUser);
                                }
                            }
                            WriteCsv(LoginFile, loginUser, rememberme_materialCheckbox.Checked);


                            // Save Log Information
                            Read_Write_Log read_Write_Log = new Read_Write_Log();
                            string SaveLogFile = read_Write_Log.LogFile;


                            List<Log> OldLog = new List<Log>();
                            if (File.Exists(SaveLogFile))
                            {
                                OldLog = read_Write_Log.ReadCsv(SaveLogFile);
                            }

                            string dateTime = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
                            OldLog.Add(new Log() { UserID = Find_UserID, ITEM = "登入", Description = "登入系统", Time = dateTime });

                            read_Write_Log.WriteCsv(SaveLogFile, OldLog);

                            if (mainForm == null)
                            {
                                mainForm = new MainForm(this, Find_autholity, Find_UserID);
                                mainForm.TopMost = false;
                                mainForm?.Show();
                            }
                        }
                    }
                }
            }
        }

        private List<UserInfo> ReadCsv_Per(string FilePath)
        {
            List<UserInfo> returnInfo = new List<UserInfo>();

            using (TextFieldParser parser = new TextFieldParser(FilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                // Read and parse each line of the CSV file
                try
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        int count = 0;
                        Autholity autholity = Autholity.admin;
                        string UserId = "";
                        string Email = "";
                        string PassWord = "";
                        foreach (var item in fields)
                        {
                            if ((count % 5) == 0)
                            {
                                Enum.TryParse(item, out autholity);
                            }
                            else if ((count % 5) == 1)
                            {
                                UserId = item;
                            }
                            else if ((count % 5) == 2)
                            {
                                Email = item;
                            }
                            else if ((count % 5) == 3)
                            {
                                PassWord = item;
                                returnInfo.Add(new UserInfo() { autholity = autholity, UserID = UserId, E_Mail = Email, Password = PassWord });
                            }
                            else
                            {
                                bool.TryParse(item, out bool result);

                                if (result)
                                {
                                    userName_materialTextBox.Text = UserId;
                                    password_materialTextBox.Text = PassWord;

                                    rememberme_materialCheckbox.Checked = true;
                                }
                            }

                            count++;
                        }
                    }
                    parser.Close();
                    return returnInfo;
                }
                catch
                {
                    return null;
                }

            }
        }

        private List<UserInfo> ReadCsv(string FilePath)
        {
            List<UserInfo> returnInfo = new List<UserInfo>();

            using (TextFieldParser parser = new TextFieldParser(FilePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                // Read and parse each line of the CSV file
                try
                {
                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();
                        int count = 0;
                        Autholity autholity = Autholity.admin;
                        string UserId = "";
                        string Email = "";
                        string PassWord = "";
                        foreach (var item in fields)
                        {
                            if ((count % 4) == 0)
                            {
                                Enum.TryParse(item, out autholity);
                            }
                            else if ((count % 4) == 1)
                            {
                                UserId = item;
                            }
                            else if ((count % 4) == 2)
                            {
                                Email = item;
                            }
                            else
                            {
                                PassWord = item;
                                returnInfo.Add(new UserInfo() { autholity = autholity, UserID = UserId, E_Mail = Email, Password = PassWord });
                            }

                            count++;
                        }
                    }
                    parser.Close();
                    return returnInfo;
                }
                catch
                {
                    return null;
                }
            }
        }

        private void WriteCsv(string FilePath, List<UserInfo> userInfos, bool RememberOrNot)
        {
            using (var file = new StreamWriter(FilePath))
            {
                foreach (var item in userInfos)
                {
                    file.WriteLineAsync($"{item.autholity},{item.UserID},{item.E_Mail},{item.Password},{RememberOrNot}");
                }

                file.Close();
            }
        }

    }
}
