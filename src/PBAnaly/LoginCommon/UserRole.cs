﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.LoginCommon
{
    #region UserRole 用户权限枚举
    /// <summary>
    /// 用户权限枚举
    /// </summary>
    public enum UserRole
    {
        /// <summary>
        /// 操作员权限
        /// </summary>
        Operator,
        /// <summary>
        /// 工程师
        /// </summary>
        Engineer,
        /// <summary>
        /// 管理员权限
        /// </summary>
        Administrator,
        /// <summary>
        /// 超级管理员
        /// </summary>
        SuperAdministrator
    }
    #endregion
}
