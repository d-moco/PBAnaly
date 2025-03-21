using PBAnaly.LoginCommon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.Assist
{
    #region OperatingRecord 操作记录
    /// <summary>
    /// 操作记录
    /// </summary>
    public static class OperatingRecord
    {
        public static event EventHandler OperatingChanged;
        private static FileWriter OpWriter = new FileWriter();
        private static int OperatingIndex = 0;

        private const string FILE_HEADER = "ID,Time,Operator,Role,Description,Action";

        /// <summary>
        /// 写操作日志
        /// </summary>
        /// <param name="s1">描述</param>
        /// <param name="s2">动作</param>
        public static void CreateRecord(string s1, string s2)
        {
            lock (lockObj)
            {
                try
                {
                    //获取程序运行的根目录
                    string directory = AppDomain.CurrentDomain.BaseDirectory + "//OperatingRecord//";


                    //判断目录是否存在，如果目录不存在，则创建目录
                    if (!System.IO.Directory.Exists(directory))
                    {
                        System.IO.Directory.CreateDirectory(directory);
                    }

                    //拼接文件名称
                    string fileName = directory + DateTime.Now.ToString("yyyyMMdd") + ".csv";


                    if (!File.Exists(fileName))
                    {
                        StreamWriter sw = new StreamWriter(fileName, true, Encoding.UTF8);
                        sw.WriteLine(FILE_HEADER);
                        sw.Flush();
                        sw.Close();
                        sw.Dispose();
                    }

                    if (OpWriter.FileName != fileName)
                    {
                        OperatingIndex = File.ReadAllLines(fileName).Length - 1;
                        OpWriter.Close();
                        OpWriter.SetFileName(fileName);
                    }

                    string strlog = string.Format("{0},{1}',{2},{3},{4},{5}",
                        OperatingIndex, DateTime.Now.ToString("G"), UserManage.LogionUser.Name,
                        UserManage.LogionUser.Role.ToString(), s1, s2);

                    OpWriter.WriteLine(strlog);
                    OpWriter.Close();
                    
                    OperatingIndex++;

                    if (OperatingChanged != null)
                    {
                        OperatingChanged(null, EventArgs.Empty);
                    }
                }
                catch (Exception ex)
                {

                }
            }
        }

        #region lockObj 创建操作记录方法使用的线程锁对象
        /// <summary>
        /// 创建操作记录方法使用的线程锁对象
        /// </summary>
        private static readonly object lockObj = new object();
        #endregion
    }
    #endregion


    #region FileWriter 文件操作类，主要用于写MESLog文件，和本地数据保存
    /// <summary>
    /// 文件操作类，主要用于写MESLog文件，和本地数据保存
    /// 使用方法：
    /// 1.实例化对象
    /// 2.调用SetFileName设置文件名
    /// 3.调用WriteLine写文件
    /// 4.调用Close关闭文件
    /// </summary>
    public class FileWriter
    {
        public string FileName
        {
            get { return mStrFileName; }
        }
        /// <summary>
        /// 保存文件名
        /// </summary>
        private string mStrFileName = "";
        /// <summary>
        /// 用于写文件的写入流
        /// </summary>
        private System.IO.StreamWriter mStreamWriter = null;

        #region SetFileName 设置文件路径
        /// <summary>
        /// 设置文件路径
        /// </summary>
        /// <param name="filePath">文件路径</param>
        public void SetFileName(string filePath)
        {
            mStrFileName = filePath;
            if (mStreamWriter == null)
            {
                mStreamWriter = new System.IO.StreamWriter(mStrFileName, true, Encoding.UTF8);
            }
        }
        #endregion

        #region CheckFileExist 检查文件是否存在
        /// <summary>
        /// 检查文件是否存在
        /// </summary>
        /// <param name="filePath">文件路</param>
        /// <returns>true-文件存在，false-文件不存在</returns>
        public bool CheckFileExist(string filePath)
        {
            return System.IO.File.Exists(filePath);
        }
        #endregion

        #region WriteLine 向文件中写入一行数据，此方法用于写CSV文件
        /// <summary>
        /// 向文件中写入一行数据，此方法用于写CSV文件
        /// </summary>
        /// <param name="values">将要写入的字符串以“,”号分割</param>
        /// <param name="isFlush">清理缓存数据，并写入流</param>
        /// <returns>true写入成功，false写入失败</returns>
        public bool WriteLine(string[] values, bool isFlush = true)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int index = 0; index < values.Length; index++)
            {
                stringBuilder.Append(values[index]);
                if (index != values.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            return WriteLine(stringBuilder.ToString(), isFlush);
        }
        #endregion

        #region WriteLine 向文件中写入一行数据
        /// <summary>
        /// 向文件中写入一行数据
        /// </summary>
        /// <param name="s">向文件中写入的字符串</param>
        /// <param name="isFlush">是否立即刷新到文件中，默认为立即写入文件，true-立即写入文件</param>
        /// <returns>true-写入成功，false-写入失败</returns>
        public bool WriteLine(string s, bool isFlush = true)
        {
            bool isWriteOk;

            //判断文件是否已经打开过，当mStreamWriter==null时，说明流未打开，需要初始化
            if (mStreamWriter == null)
            {
                //初始化文件流，并打开文件
                mStreamWriter = new System.IO.StreamWriter(mStrFileName, true, Encoding.UTF8);
            }

            try
            {
                //将s写入到文件流中
                mStreamWriter.WriteLine(s);
                //是否立即刷新到文件，true为立即写入，false不写入
                if (isFlush)
                {
                    //清理当前缓存，并将缓存数据希尔文件
                    mStreamWriter.Flush();
                }
                isWriteOk = true;
            }
            catch (Exception ex)
            {
                isWriteOk = false;
            }
            return isWriteOk;
        }
        #endregion

        #region Close 关闭文件
        /// <summary>
        /// 关闭文件
        /// </summary>
        public void Close()
        {
            //判断文件流是否打开，如果为null则没有打开，如果不为null则文件已打开
            if (mStreamWriter != null)
            {
                //将缓存数据刷入到文件中
                mStreamWriter.Flush();
                //关闭文件流
                mStreamWriter.Close();
                //将文件流指向null，有利于下次打开时做判断
                mStreamWriter = null;
            }
        }
        #endregion
    }
    #endregion
}
