using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.LoginCommon
{
    #region User 用户详情类，保存用户名称、密码、创建者、描述、用户权限
    /// <summary>
    /// 用户详情类，保存用户名称、密码、创建者、描述、用户权限
    /// </summary>
    public struct User
    {
        #region publice propertys

        #region Name 设置或获取用户名
        /// <summary>
        /// 设置或获取用户名
        /// </summary>
        public string Name { get; set; }
        #endregion

        #region Password 设置或获取密码
        /// <summary>
        /// 设置或获取密码
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region CreatedBy 设置或获取创建者
        /// <summary>
        /// 设置或获取创建者
        /// </summary>
        public string CreatedBy { get; set; }
        #endregion

        #region PasswordQuestion 设置或获取密保问题
        /// <summary>
        /// 设置或获取密保问题
        /// </summary>
        public string PasswordQuestion { get; set; }
        #endregion

        #region QuestionAnswer 设置或获取密保答案
        /// <summary>
        /// 设置或获取密保答案
        /// </summary>
        public string QuestionAnswer { get; set; }
        #endregion

        #region CreatedDate 设置或获取创时间
        /// <summary>
        /// 设置或获取创时间
        /// </summary>
        public DateTime CreatedDate { get; set; }
        #endregion


        #region Role 设置或获取用户权限
        /// <summary>
        /// 设置或获取用户权限
        /// </summary>
        public UserRole Role { get; set; }
        #endregion

        #endregion


        #region Init 初始化用户
        /// <summary>
        /// 初始化用户
        /// </summary>
        public void Init()
        {
            Name = "未登录";
            Password = "Op";
            CreatedBy = "System";
            CreatedDate = DateTime.Now;
            Role = UserRole.Operator;
        }
        #endregion
    }
    #endregion
}
