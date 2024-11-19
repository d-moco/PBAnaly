using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using MaterialSkin;
using MaterialSkin.Controls;
using Microsoft.VisualBasic.FileIO;

namespace PBAnaly
{

    public partial class SignInForm : MaterialForm
    {
        private MaterialSkinManager materialSkinManager;
        private LoginForm loginForm;

        bool confirmPCheck = false;

        public SignInForm(LoginForm PassloginForm)
        {
            InitializeComponent();
            loginForm = PassloginForm;
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint, true);
            materialSkinManager = MaterialSkinManager.Instance; // 初始化 MaterialSkinManager 实例
            materialSkinManager.AddFormToManage(this);  // 将要应用 Material Design 的窗体添加到管理列表中
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;   // Theme 属性用来设置整体的主题
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.Red700, TextShade.WHITE);
        }

        private void userName_materialTextBox_Click(object sender, EventArgs e)
        {
            userName_materialTextBox.Text = "";
        }

        private void email_materialTextBox_Click(object sender, EventArgs e)
        {
            email_materialTextBox.Text = "";
        }

        private void password_materialTextBox_Click(object sender, EventArgs e)
        {
            password_materialTextBox.Text = "";
            password_materialTextBox.PasswordChar = '*';
            password_materialTextBox.UseSystemPasswordChar = true;
        }

        private void confirmPassword_materialTextBox_Click(object sender, EventArgs e)
        {
            confirmPassword_materialTextBox.Text = "";
            confirmPassword_materialTextBox.PasswordChar = '*';
            confirmPassword_materialTextBox.UseSystemPasswordChar = true;
        }

        private void signIn_materialButton_Click(object sender, EventArgs e)
        {
            bool signInSuccess = true;

            string UserID_str = userName_materialTextBox.Text;
            string E_mail_str = email_materialTextBox.Text;
            string Password_str = password_materialTextBox.Text;
            string Con_Password_str = confirmPassword_materialTextBox.Text;

            List<UserInfo> userInfos = new List<UserInfo>();
            //Check Is Null Or Not
            if (string.IsNullOrEmpty(UserID_str))
            {
                materialLabel_ID.Text = "Please Type User Name Or ID!!";
                signInSuccess = false;
            }
            else
            {
                materialLabel_ID.Text = "";
            }

            if (string.IsNullOrEmpty(E_mail_str))
            {
                materialLabel_email.Text = "Please Type E-Mail Address!!";
                signInSuccess = false;
            }
            else
            {
                materialLabel_email.Text = "";
            }

            if (string.IsNullOrEmpty(Password_str))
            {
                materialLabel_PS.Text = "Please Type Password!!";
                signInSuccess = false;
            }
            else
            {
                materialLabel_PS.Text = "";
            }

            if (string.IsNullOrEmpty(Con_Password_str))
            {
                materialLabel_PW.Text = "Please Type Confirm Password!!";
                signInSuccess = false;
            }
            else
            {
                materialLabel_PW.Text = "";
            }

            if (!signInSuccess)
            {
                return;
            }
            else
            {
                //Check Format
                if (!Password_str.Equals(Con_Password_str))
                {
                    materialLabel_PW.Text = "*Please Type Same Password!!";
                    signInSuccess = false;
                }
                else
                {
                    materialLabel_PW.Text = "";
                }

                if (!E_mail_str.Contains("@"))
                {
                    materialLabel_email.Text = "*Please Check E-mail Address!!";
                    signInSuccess = false;
                }
                else
                {
                    materialLabel_email.Text = "";
                }


                if (signInSuccess)
                {
                    //Read UserInfo CSV
                    string FileName = Global.mDataUser + "UserInfo.csv";
                    if (!File.Exists(FileName))
                    {
                        if (!Directory.Exists(Global.mDataUser))
                        {
                            Directory.CreateDirectory(Global.mDataUser);
                        }

                        userInfos.Add(new UserInfo() { autholity = Autholity.Normal, UserID = UserID_str, E_Mail = E_mail_str, Password = Password_str });
                        WriteCsv(FileName, userInfos);
                    }
                    else
                    {
                        var GetCurrentUserInfo = ReadCsv(FileName);

                        foreach(var item in GetCurrentUserInfo)
                        {
                            if(item.UserID.Equals(UserID_str))
                            {
                                MessageBox.Show("This UserID has been registered, please change another one!!");
                                return;
                            }
                        }
                        GetCurrentUserInfo.Add(new UserInfo() { autholity = Autholity.Normal, UserID = UserID_str, E_Mail = E_mail_str, Password = Password_str });
                        WriteCsv(FileName, GetCurrentUserInfo);
                    }

                    loginForm.Visible = true;
                    this.Visible = false;
                    this.Close();
                }
                else
                {
                    return;
                }
            }



        }

        private void WriteCsv(string FilePath, List<UserInfo> userInfos)
        {
            using (var file = new StreamWriter(FilePath))
            {
                foreach (var item in userInfos)
                {
                    file.WriteLineAsync($"{item.autholity},{item.UserID},{item.E_Mail},{item.Password}");
                }

                file.Close();
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
                            if ( (count%4) == 0)
                            {
                                Enum.TryParse(item, out autholity);
                            }
                            else if ((count%4) == 1)
                            {
                                UserId = item;
                            }
                            else if ((count%4) == 2)
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


        private void confirmPassword_materialTextBox_TextChanged(object sender, EventArgs e)
        {
            string Password_str = password_materialTextBox.Text;
            string Confirm_Password_str = confirmPassword_materialTextBox.Text;

            if ((Password_str != Confirm_Password_str) && Password_str != null && Confirm_Password_str != null)
            {
                materialLabel_PW.Text = "*Please Type Same Password!!";
            }
            else
            {
                materialLabel_PW.Text = "";
            }
        }
    }
}
