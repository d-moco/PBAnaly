using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace PBAnaly.LoginCommon
{
    /// <summary>
    /// 权限管理
    /// </summary>
    public class AccessControl
    {
        /// <summary>
        /// 权限管理项集合
        /// </summary>
        public static List<AccessItem> AccessItems = new List<AccessItem>();
        /// <summary>
        /// 加载配置文件
        /// </summary>
        public static void LoadConfig()
        {
            try
            {
                if (File.Exists("AccessControl.xml"))
                {
                    FileStream fs = new FileStream("AccessControl.xml", FileMode.Open);
                    XmlSerializer xs = new XmlSerializer(typeof(List<AccessItem>));
                    AccessItems = xs.Deserialize(fs) as List<AccessItem>;
                    fs.Close();
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("加载配置文件AccessControl.xml不存在。即将推出程序.");
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(string.Format("加载配置文件AccessControl.xml出错。错误原因:{0}即将推出程序.", ex.Message));
                throw ex;
            }
        }
        /// <summary>
        /// 保存配置文件
        /// </summary>
        public static void SaveConfig()
        {
            FileStream fs = new FileStream("AccessControl.xml", FileMode.Create);
            XmlSerializer xs = new XmlSerializer(typeof(List<AccessItem>));
            xs.Serialize(fs, AccessItems);
            fs.Close();
        }

        public static void SaveBinaryConfig()
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream fs = new FileStream("AccessControl.bin", FileMode.Create);
            formatter.Serialize(fs, AccessItems);
            fs.Close();
        }

        public static void LoadBinaryConfig()
        {
            IFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            Stream fs = new FileStream("AccessControl.bin", FileMode.Open);
            AccessItems = (List<AccessItem>)formatter.Deserialize(fs);
            fs.Close();
        }
    }






    #region AccessItem 权限管理项
    /// <summary>
    /// 权限管理项
    /// </summary>
    [XmlType(TypeName = "item")]
    [Serializable]
    public class AccessItem
    {
        /// <summary>
        /// 编号
        /// </summary>
        [XmlAttribute]
        public int Id = 0;
        /// <summary>
        /// 操作员是否可操作，true-可操作，false-不可操作
        /// </summary>
        [XmlAttribute]
        public bool Operator = false;
        /// <summary>
        /// 工程师是否可操作，true-可操作，false-不可操作
        /// </summary>
        [XmlAttribute]
        public bool Engineer = false;
        /// <summary>
        /// 管理员是否可操作，true-可操作，false-不可操作
        /// </summary>
        [XmlAttribute]
        public bool Administrator = false;
        /// <summary>
        /// 超级管理员是否可操作，true-可操作，false-不可操作
        /// </summary>
        [XmlAttribute]
        public bool SuperAdministrator = false;
        /// <summary>
        /// 描述是否可操作，true-可操作，false-不可操作
        /// </summary>
        [XmlAttribute]
        public string Disrible = "";
    }
    #endregion
}
