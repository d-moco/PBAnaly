using AntdUI;
using PBAnaly.LoginCommon;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.Assist
{
    public static class GlobalData
    {

        public static string dbPath = "UserManage.db";
        public static string connectionString = $"Data Source={dbPath};Version=3;";


        #region Propertys 全局属性，属性名、属性值
        /// <summary>
        /// 全局属性，属性名、属性值
        /// </summary>
        private static Dictionary<string, string> Propertys;
        #endregion
        /// <summary>
        /// 全局属性更改事件
        /// </summary>
        public static event PropertyChangedHandle PropertyChanged;
        /// <summary>
        /// 带两个参数无返回值的委托
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public delegate void PropertyChangedHandle(string name, string value);

        #region SetProperty 设置全局属性
        /// <summary>
        /// 设置全局属性
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        public static void SetProperty(string name, string value)
        {
            //判断当前key是否在集合内
            if (GlobalData.Propertys.ContainsKey(name))
            {
                //当前key在集合内
                //判断要设置的属性是否相同
                if (GlobalData.Propertys[name] != value)
                {
                    //设置属性
                    GlobalData.Propertys[name] = value;

                    //根据属性名更新数据库
                    UpdateGlobalPropertyByName(name, value);

                    //触发属性更改事件
                    PropertyChanged?.Invoke(name, value);
                }
            }
            else
            {
                try
                {
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        string insertSQL = @"
                        INSERT OR IGNORE INTO global_property (property_name, property_value)
                        VALUES (@property_name, @property_value);";

                        using (var command = new SQLiteCommand(insertSQL, connection))
                        {
                            // 参数化查询，防止SQL注入
                            command.Parameters.AddWithValue("@property_name", name);
                            command.Parameters.AddWithValue("@property_value", value);
                            command.ExecuteNonQuery();
                        }

                        connection.Close();
                    }

                    //更新集合中当前key对应的值
                    GlobalData.Propertys[name] = value;

                    PropertyChanged?.Invoke(name, value);
                }
                catch (Exception ex)
                {
                }
            }
        }
        #endregion

        #region GetProperty 根据属性名获取属性值
        /// <summary>
        /// 根据属性名获取属性值
        /// </summary>
        /// <param name="name">属性名</param>
        /// <returns>属性值</returns>
        public static string GetProperty(string name)
        {
            string value = "";
            if (GlobalData.Propertys.ContainsKey(name))
            {
                value = GlobalData.Propertys[name];
            }
            return value;
        }
        #endregion

        #region  LoadGlobalPropertyFromDb 加载全局变量
        /// <summary>
        /// 加载全局变量LoadGlobalPropertyFromDb
        /// </summary>
        public static void LoadGlobalPropertyFromDb()
        {
            Propertys = new Dictionary<string, string>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "select *from global_property";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string key = reader["property_name"].ToString();
                            string value = reader["property_value"].ToString();
                            GlobalData.Propertys.Add(key, value);
                        }
                    }
                }

                connection.Close();
            }
        }
        #endregion

        #region UpdateGlobalPropertyByName 根据属性名更新属性表
        /// <summary>
        /// 根据属性名更新属性表
        /// </summary>
        /// <param name="name">属性名</param>
        /// <param name="value">属性值</param>
        private static void UpdateGlobalPropertyByName(string name, object value)
        {
            string sql = string.Format("update global_property set property_value='{0}' where property_name='{1}'", value, name);
            using (SQLiteConnection conn = new SQLiteConnection(connectionString))
            {
                conn.Open();

                using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                {
                    var blnResult = (cmd.ExecuteNonQuery() > 0);
                }
                conn.Close();
            }

        }
        #endregion

    }
}
