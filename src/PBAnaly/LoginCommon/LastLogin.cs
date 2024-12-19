using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.LoginCommon
{
    public class LastLogin
    {
        #region Name 上一次登录的名称
        /// <summary>
        /// 上一次登录的名称
        /// </summary>
        public string UserName { get; set; }
        #endregion

        #region Password 上一次登录的密码
        /// <summary>
        /// 上一次登录的密码
        /// </summary>
        public string Password { get; set; }
        #endregion

        #region Remember 是否记住本次登录信息   1：记住 0：不记住
        /// <summary>
        /// 是否记住本次登录信息   1：记住 0：不记住
        /// </summary>
        public string Remember { get; set; }
        #endregion
    }
}
