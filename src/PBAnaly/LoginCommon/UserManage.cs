using MiniExcelLibs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PBAnaly.LoginCommon
{
    public static class UserManage
    {
        public static  string dbPath = "UserManage.db";
        public static string connectionString = $"Data Source={dbPath};Version=3;";

        /// <summary>
        /// 记录上一次登录的用户名和密码
        /// </summary>
        public static Dictionary<string, LastLogin> LastLoginUser;
        /// <summary>
        /// 登录用户更改事件
        /// </summary>
        public static EventHandler LogionUserChanged;
        /// <summary>
        /// 数据库所有的用户
        /// </summary>
        public static Dictionary<string, User> UsersKeyValuePairs;
        /// <summary>
        /// 系统当前登录的用户
        /// </summary>
        public static User LogionUser { get; private set; }
        /// <summary>
        /// 系统是否已经登录
        /// </summary>
        public static bool IsLogined { get; set; }

        /// <summary>
        /// 设置当前登录的用户
        /// </summary>
        /// <param name="user">当前登录的用户信息</param>
        public static void SetLogionUser(User user)
        {
            if (LogionUser.Name != user.Name)
            {
                LogionUser = user;
                LogionUserChanged?.Invoke(null, EventArgs.Empty);
            }
        }

        #region ConnectDb 连接数据库，如果数据库不存在的话，就创建一个，数据库名：UserManage
        /// <summary>
        /// 连接数据库，如果数据库不存在的话，就创建一个，数据库名：UserManage
        /// </summary>
        /// <returns></returns>
        public static bool ConnectDb()
        {
            bool isOK = false;

            try
            {
                // 检查数据库文件是否存在
                if (!File.Exists(dbPath))
                {
                    SQLiteConnection.CreateFile(dbPath);  // 创建数据库文件
                    Console.WriteLine("数据库文件已创建。");

                    // 在新数据库上创建表
                    using (var connection = new SQLiteConnection(connectionString))
                    {
                        connection.Open();

                        string sql = @"
                                CREATE TABLE IF NOT EXISTS User (
                                    UserName VARCHAR(200) NOT NULL,
                                    Password VARCHAR(2000) NOT NULL,
                                    CreatedBy CHAR(50) NOT NULL,
                                    CreatedDate DATETIME NOT NULL,
                                    Role CHAR(200),
                                    PasswordQuestion VARCHAR(1000) DEFAULT NULL,
                                    QuestionAnswer VARCHAR(1000) DEFAULT NULL,
                                    PRIMARY KEY (UserName)
                                );";

                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();  // 执行SQL命令创建表
                            Console.WriteLine("表 'User' 已创建。");
                        }

                        sql = @"
                                CREATE TABLE IF NOT EXISTS last (
                                    ID VARCHAR(100) NOT NULL,
                                    UserName VARCHAR(200) NOT NULL,
                                    Password VARCHAR(2000) NOT NULL,
                                    Remember VARCHAR(200) NOT NULL,
                                    PRIMARY KEY (ID)
                                );";

                        using (var command = new SQLiteCommand(sql, connection))
                        {
                            command.ExecuteNonQuery();  // 执行SQL命令创建表
                            Console.WriteLine("表 'last' 已创建。");
                        }

                        // 插入数据
                        InsertDefaultUserData(connectionString);
                    }
                }
                //加载用户
                LoadUsersFromDatabase(connectionString);
                //加载上一次登录的用户
                LoadLastLoginUser();
            }
            catch (Exception ex)
            {
                isOK = false;
            }

            return isOK;

        }
        #endregion

        #region InsertDefaultUserData 创建完数据库之后需要插入一个管理员用户 root
        /// <summary>
        /// 创建完数据库之后需要插入一个管理员用户root
        /// </summary>
        /// <param name="connectionString"></param>
        private static void InsertDefaultUserData(string connectionString)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string insertSQL = @"
                        INSERT OR IGNORE INTO User (UserName, Password, CreatedBy, CreatedDate, Role, PasswordQuestion, QuestionAnswer)
                        VALUES (@UserName, @Password, @CreatedBy, @CreatedDate, @Role, @PasswordQuestion, @QuestionAnswer);";

                    using (var command = new SQLiteCommand(insertSQL, connection))
                    {
                        // 参数化查询，防止SQL注入
                        command.Parameters.AddWithValue("@UserName", "root");
                        command.Parameters.AddWithValue("@Password", "root"); // 示例密码
                        command.Parameters.AddWithValue("@CreatedBy", "System");
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Role", "SuperAdministrator");
                        command.Parameters.AddWithValue("@PasswordQuestion", "我的名字");
                        command.Parameters.AddWithValue("@QuestionAnswer", "root");

                        command.ExecuteNonQuery();
                    }

                    string insertLast = @"
                        INSERT OR IGNORE INTO last (ID,UserName, Password, Remember)
                        VALUES (@ID,@UserName, @Password, @Remember);";

                    using (var command = new SQLiteCommand(insertLast, connection))
                    {
                        // 参数化查询，防止SQL注入
                        command.Parameters.AddWithValue("@ID", "1");
                        command.Parameters.AddWithValue("@UserName", "root");
                        command.Parameters.AddWithValue("@Password", "root"); // 示例密码
                        command.Parameters.AddWithValue("@Remember", "0");


                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }
            }
            catch (Exception)
            {

            }
            
        }

        #endregion

        #region LoadUsersFromDatabase 从数据库加载 User 表数据到 UsersKeyValuePairs 字典中
        /// <summary>
        /// 从数据库加载 User 表数据到 UsersKeyValuePairs 字典中
        /// </summary>
        /// <param name="connectionString">SQLite 数据库连接字符串</param>
        private static void LoadUsersFromDatabase(string connectionString)
        {
            UsersKeyValuePairs = new Dictionary<string, User>();
            using (var connection = new SQLiteConnection(connectionString))
            {
                connection.Open();

                string query = "SELECT UserName, Password, CreatedBy, CreatedDate, Role, PasswordQuestion, QuestionAnswer FROM User";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            User user = new User
                            {
                                Name = reader["UserName"].ToString(),
                                Password = reader["Password"].ToString(),
                                CreatedBy = reader["CreatedBy"].ToString(),
                                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                                Role = (UserRole)Enum.Parse(typeof(UserRole), reader["Role"].ToString()),
                                PasswordQuestion = reader["PasswordQuestion"].ToString(),
                                QuestionAnswer = reader["QuestionAnswer"].ToString()
                            };

                            // 添加到字典中，使用 UserName 作为 Key
                            UsersKeyValuePairs[user.Name] = user;
                        }
                    }
                }

                connection.Close();
            }
        }

        #endregion

        #region LoadLastLoginUser 加载上一次登录的用户
        /// <summary>
        /// 加载上一次登录的用户
        /// </summary>
        public static void LoadLastLoginUser()
        {
            LastLoginUser = new Dictionary<string, LastLogin>();
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT * FROM last";  // 更改为您的表名和查询

                    SQLiteCommand command = new SQLiteCommand(query, connection);
                    SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(command);

                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        foreach (DataRow row in dataTable.Rows)
                        {
                            LastLogin last = new LastLogin();
                            last.UserName = row["UserName"].ToString();
                            last.Password = row["Password"].ToString();
                            last.Remember = row["Remember"].ToString();
                            LastLoginUser.Add(last.UserName, last);
                        }
                    }
                    connection.Close();
                }
                catch (Exception e)
                {
                    Console.WriteLine("数据库操作出错:");
                    Console.WriteLine(e.Message);
                }
            }
        }



        #endregion

        #region UpDateLastUser 更新上一次登录的用户
        /// <summary>
        /// 更新上一次登录的用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <param name="Remember"></param>
        public static void UpDateLastUser(string UserName,string Password,string Remember)
        {
            try
            {
                string updateQuery = "UPDATE last SET UserName = @UserName," +
                    " Password = @Password, Remember = @Remember WHERE ID = @ID";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        // 参数化查询，防止SQL注入
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Password", Password);
                        cmd.Parameters.AddWithValue("@Remember", Remember);
                        cmd.Parameters.AddWithValue("@ID", "1");

                        // 执行更新命令
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Console.WriteLine($"更新成功，受影响的行数：{rowsAffected}");
                    }

                    conn.Close();
                }
            }
            catch (Exception)
            {

            }
        }

        #endregion

        #region RegisterUser 注册一个用户
        /// <summary>
        /// 用户注册
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public static bool RegisterUser(User user)
        {
            try
            {
                using (var connection = new SQLiteConnection(connectionString))
                {
                    connection.Open();

                    string insertSQL = @"
                        INSERT OR IGNORE INTO User (UserName, Password, CreatedBy, CreatedDate, Role, PasswordQuestion, QuestionAnswer)
                        VALUES (@UserName, @Password, @CreatedBy, @CreatedDate, @Role, @PasswordQuestion, @QuestionAnswer);";

                    using (var command = new SQLiteCommand(insertSQL, connection))
                    {
                        // 参数化查询，防止SQL注入
                        command.Parameters.AddWithValue("@UserName", user.Name);
                        command.Parameters.AddWithValue("@Password", user.Password); // 示例密码
                        command.Parameters.AddWithValue("@CreatedBy", "System");
                        command.Parameters.AddWithValue("@CreatedDate", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                        command.Parameters.AddWithValue("@Role", user.Role);
                        command.Parameters.AddWithValue("@PasswordQuestion", user.PasswordQuestion);
                        command.Parameters.AddWithValue("@QuestionAnswer", user.QuestionAnswer);

                        command.ExecuteNonQuery();

                    }

                    connection.Close();
                }

                UsersKeyValuePairs[user.Name] = user;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        #endregion

        #region FixUserRole 修改权限
        /// <summary>
        /// 修改权限
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Role"></param>
        /// <returns></returns>
        public static bool FixUserRole(string UserName,string Role)
        {
            try
            {
                string updateQuery = "UPDATE User SET Role = @Role WHERE UserName = @UserName";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        // 参数化查询，防止SQL注入
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Role", Role);

                        // 执行更新命令
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Console.WriteLine($"更新成功，受影响的行数：{rowsAffected}");
                    }

                    conn.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

        #region DeleteUser 删除用户
        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public static bool DeleteUser(string UserName)
        {
            try
            {
                string sql = string.Format("delete from User where `UserName`='{0}'", UserName);

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(sql, conn))
                    {

                        var blnResult = (cmd.ExecuteNonQuery() > 0);
                    }
                    
                    conn.Close();
                    UsersKeyValuePairs.Remove(UserName);
                    return true;
                }
            }
            catch (Exception)
            {

                return false;
            }
        }

        #endregion


        #region FixUserPassword 修改密码
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static bool FixUserPassword(string UserName , string Password)
        {
            try
            {
                string updateQuery = "UPDATE User SET Password = @Password WHERE UserName = @UserName";

                using (SQLiteConnection conn = new SQLiteConnection(connectionString))
                {
                    conn.Open();

                    using (SQLiteCommand cmd = new SQLiteCommand(updateQuery, conn))
                    {
                        // 参数化查询，防止SQL注入
                        cmd.Parameters.AddWithValue("@UserName", UserName);
                        cmd.Parameters.AddWithValue("@Password", Password);

                        // 执行更新命令
                        int rowsAffected = cmd.ExecuteNonQuery();

                        Console.WriteLine($"更新成功，受影响的行数：{rowsAffected}");
                    }

                    conn.Close();

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #endregion

    }
}
