using PBAnaly.Assist;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PBAnaly.LoginCommon
{
    public partial class UserManageForm : Form
    {
        public UserManageForm()
        {
            InitializeComponent();

            panel1.BringToFront();

            SetMainMenuButtonCilkeColor("btn_editRole");

            if (GlobalData.GetProperty("Language") == "Chinese")
            {
                SetLanguage("zh-CN");
            }
            else
            {
                SetLanguage("en-US");
            }
        }

        #region 中英文切换模块
        ResourceManager resourceManager;
        private void SetLanguage(string cultureCode)
        {
            resourceManager = new ResourceManager("PBAnaly.Properties.Resources", typeof(MainForm).Assembly);

            // 设置当前线程的文化信息
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(cultureCode);

            // 更新所有控件的文本
            UpdateControlsText();
        }

        // 更新所有控件的文本
        private void UpdateControlsText()
        {
            //// 遍历所有控件并更新文本
            foreach (Control control in this.Controls)
            {
                UpdateControlText(control);
            }
        }
        // 更新单个控件的文本
        private void UpdateControlText(Control control)
        {
            //// 直接通过控件的 Name 属性获取资源字符串
            string resourceText = resourceManager.GetString(control.Name);
            if (!string.IsNullOrEmpty(resourceText))
            {
                control.Text = resourceText;
            }

            // 如果控件包含子控件，则递归更新子控件
            foreach (Control subControl in control.Controls)
            {
                UpdateControlText(subControl);
            }
        }
        #endregion

        #region InitUser 加载全部的用户  root除外
        /// <summary>
        /// 加载全部的用户  root除外
        /// </summary>
        public void InitUser()
        {
            try
            {
                int index = 1;
                dataGridView1.Rows.Clear();

                foreach (User user in UserManage.UsersKeyValuePairs.Values)
                {
                    if (user.Name != "root")
                    {
                        DataGridViewRow dr = new DataGridViewRow();
                        dr.CreateCells(dataGridView1);
                        dr.Cells[0].Value = index.ToString();
                        dr.Cells[1].Value = user.Name;
                        dr.Cells[2].Value = user.CreatedBy;
                        dr.Cells[3].Value = user.CreatedDate;
                        dr.Cells[4].Value = user.Role.ToString();
                        dataGridView1.Rows.Add(dr);

                        index++;
                    }
                }

                if (UserManage.IsLogined)
                {
                    if(UserManage.LogionUser.Role == UserRole.SuperAdministrator)
                    {
                        btn_role_manage.Visible = true;
                    }
                    else
                    {
                        btn_role_manage.Visible = false;
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        #endregion

        #region btn_fix_role_Click 修改权限按钮
        private void btn_fix_role_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show($"确定要用户:{txt_UserName.Text}的权限吗?", "提示", MessageBoxButtons.YesNo);
            if (result == DialogResult.Yes)
            {
                try
                {
                    if (!string.IsNullOrEmpty(txt_UserName.Text))
                    {
                        User user = new User();
                        user.Name = txt_UserName.Text;
                        user.Role = (UserRole)Enum.Parse(typeof(UserRole), cbx_role_role.Text);
                        user.CreatedDate = UserManage.UsersKeyValuePairs[txt_UserName.Text].CreatedDate;
                        user.CreatedBy = UserManage.UsersKeyValuePairs[txt_UserName.Text].CreatedBy;
                        user.PasswordQuestion = UserManage.UsersKeyValuePairs[txt_UserName.Text].PasswordQuestion;
                        user.QuestionAnswer = UserManage.UsersKeyValuePairs[txt_UserName.Text].QuestionAnswer;
                        user.Password = UserManage.UsersKeyValuePairs[txt_UserName.Text].Password;

                        UserManage.UsersKeyValuePairs[user.Name] = user;

                        if(UserManage.FixUserRole(txt_UserName.Text, cbx_role_role.Text))
                        {
                            InitUser();
                            MessageBox.Show("修改成功");
                        }
                        else
                        {
                            MessageBox.Show("修改失败");
                        }
                    }


                }
                catch (Exception ex)
                {
                    MessageBox.Show($"权限修改失败，原因：{ex.Message.ToString()}");
                }
            }
        }
        #endregion

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                DataGridViewRow row = dataGridView1.SelectedRows[0];
                txt_UserName.Text = row.Cells[1].Value.ToString();
                cbx_role_role.Text = row.Cells[4].Value.ToString();

                txt_fix_p_UserName.Text=row.Cells[1].Value.ToString();
                txt_password.Text = UserManage.UsersKeyValuePairs[txt_fix_p_UserName.Text].Password;
            }
        }


        private void btn_delete_user_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows.Count > 0 )
                {
                    if(dataGridView1.SelectedRows.Count >= 2) { MessageBox.Show("一次只能删除一个用户");return; }

                    DataGridViewRow row = dataGridView1.SelectedRows[0];
                    string UserName = row.Cells[1].Value.ToString();

                    if (UserManage.DeleteUser(UserName))
                    {
                        InitUser();
                    }

                }
                else
                {
                    MessageBox.Show("请在表格内选择一行要删除的登记信息，且一次只能删除一行");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("删除失败，原因：" + ex.Message);
            }
        }

        private void btn_FixPassword_Click(object sender, EventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txt_UserName.Text))
                {
                    User user = new User();
                    user.Name = txt_UserName.Text;
                    user.Role = (UserRole)Enum.Parse(typeof(UserRole), cbx_role_role.Text);
                    user.CreatedDate = UserManage.UsersKeyValuePairs[txt_UserName.Text].CreatedDate;
                    user.CreatedBy = UserManage.UsersKeyValuePairs[txt_UserName.Text].CreatedBy;
                    user.PasswordQuestion = UserManage.UsersKeyValuePairs[txt_UserName.Text].PasswordQuestion;
                    user.QuestionAnswer = UserManage.UsersKeyValuePairs[txt_UserName.Text].QuestionAnswer;
                    user.Password = txt_password.Text;

                    UserManage.UsersKeyValuePairs[user.Name] = user;

                    if (UserManage.FixUserPassword(txt_UserName.Text, txt_password.Text))
                    {
                        MessageBox.Show("密码修改成功");
                    }
                    else
                    {
                        MessageBox.Show("密码修改失败");
                    }
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"密码修改失败，原因：{ex.Message.ToString()}");
            }
        }

        private void btn_editRole_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 0;
            SetMainMenuButtonCilkeColor(((Button)sender).Name);
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 1;
            SetMainMenuButtonCilkeColor(((Button)sender).Name);
        }

        private void btn_edit_password_Click(object sender, EventArgs e)
        {
            this.tabControl1.SelectedIndex = 2;
            SetMainMenuButtonCilkeColor(((Button)sender).Name);
        }

        #region  SetMainMenuButtonCilkeColor 主菜单中按钮点击之后，设置按钮的前景色和背景色
        /// <summary>
        /// 主菜单中按钮点击之后，设置按钮的前景色和背景色
        /// </summary>
        /// <param name="strBtnName">点击的按钮的名称</param>
        private void SetMainMenuButtonCilkeColor(string strBtnName)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control.Name == panel1.Name)
                {
                    continue;
                }
                else if (control.Name == strBtnName)
                {
                    control.BackColor = Color.Aqua;
                    control.ForeColor = Color.Black;
                }
                else
                {
                    control.BackColor = Color.FromArgb(0, 32, 96);
                    control.ForeColor = Color.White;
                }
            }
        }
        #endregion

        private void btn_role_manage_Click(object sender, EventArgs e)
        {
            RoleManageForm roleManageForm = new RoleManageForm();
            roleManageForm.ShowDialog();
        }
    }
}
