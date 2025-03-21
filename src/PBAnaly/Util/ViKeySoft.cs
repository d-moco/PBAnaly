using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace PBAnaly.Util
{
    public class ViKeySoft
    {
        private static ViKeySoft instance = null;

        public static ViKeySoft Instance 
        {
            get 
            {
                if (instance == null) 
                {
                    instance = new ViKeySoft();
                }
                return instance;
            }
        }

        // 错误代码
        const long VIKEY_SUCCESS = 0x00000000; //成功
        const long VIKEY_ERROR_NO_VIKEY = 0x80000001; //没有找到ViKey加密锁
        const long VIKEY_ERROR_INVALID_PASSWORD = 0x80000002; //密码错误
        const long VIKEY_ERROR_NEED_FIND = 0x80000003; //请先查找加密锁
        const long VIKEY_ERROR_INVALID_INDEX = 0x80000004; //无效的句柄
        const long VIKEY_ERROR_INVALID_VALUE = 0x80000005; //数值错误
        const long VIKEY_ERROR_INVALID_KEY = 0x80000006; //秘钥无效
        const long VIKEY_ERROR_GET_VALUE = 0x80000007; //读取信息错误
        const long VIKEY_ERROR_SET_VALUE = 0x80000008; //设置信息错误
        const long VIKEY_ERROR_NO_CHANCE = 0x80000009; //没有机会
        const long VIKEY_ERROR_NO_TAUTHORITY = 0x8000000A; //权限不足
        const long VIKEY_ERROR_INVALID_ADDR_OR_SIZE = 0x8000000B; //地址或长度错误
        const long VIKEY_ERROR_RANDOM = 0x8000000C; //获取随机数错误
        const long VIKEY_ERROR_SEED = 0x8000000D; //获取种子错误
        const long VIKEY_ERROR_CONNECTION = 0x8000000E; //通信错误
        const long VIKEY_ERROR_CALCULATE = 0x8000000F; //算法或计算错误
        const long VIKEY_ERROR_MODULE = 0x80000010; //计数器错误
        const long VIKEY_ERROR_GENERATE_NEW_PASSWORD = 0x80000011; //产生密码错误
        const long VIKEY_ERROR_ENCRYPT_FAILED = 0x80000012; //加密数据错误
        const long VIKEY_ERROR_DECRYPT_FAILED = 0x80000013; //解密数据错误
        const long VIKEY_ERROR_ALREADY_LOCKED = 0x80000014; //ViKey加密锁已经被锁定
        const long VIKEY_ERROR_UNKNOWN_COMMAND = 0x80000015; //无效的命令
        const long VIKEY_ERROR_NO_SUPPORT = 0x80000016; //当前ViKey加密锁不支持此功能
        const long VIKEY_ERROR_CATCH = 0x80000017; //发生异常
        const long VIKEY_ERROR_GET_USBDATA = 0x80000018; //读取USB数据错误
        const long VIKEY_ERROR_SET_USBDATA = 0x80000019; //设置USB数据错误
        const long VIKEY_ERROR_TIME_MODULE = 0x8000001A; //硬件时钟模块异常
        const long VIKEY_ERROR_TIME_OUTAGE = 0x8000001B; //硬件时钟发生过断电，时钟可能不准确，需校准时钟
        const long VIKEY_ERROR_MAX_CONNECTION = 0x8000001C; //加密狗达到最大通信链接数
        const long VIKEY_ERROR_COMMUNICATION = 0x8000001D; //加密狗通信数据错误
        const long VIKEY_ERROR_TIME = 0x8000001E; //加密狗时钟错误
        const long VIKEY_ERROR_TIME_EXPIRE = 0x8000001F; //加密狗时钟限制已经到期
        const long VIKEY_ERROR_TIME_DISORDER = 0x80000020; //加密狗时钟失调
        const long VIKEY_ERROR_INIT_USB = 0x80000021; //安卓系统-初始化USB失败
        const long VIKEY_ERROR_OPEN_USB = 0x80000022; //安卓系统-打开USB设备失败
        const long VIKEY_ERROR_UNKNOWN_ERROR = 0xFFFFFFFF; //未知错误

        //ViKey加密狗类型  VikeyGetType返回值代表的类型
        const uint ViKeyAPP = 0;   					//实用型加密狗ViKeyAPP
        const uint ViKeySTD = 1;   					//标准型加密狗ViKeySTD
        const uint ViKeyNET = 2;   					//网络型加密狗ViKeyNET
        const uint ViKeyPRO = 3;   					//专业型加密狗ViKeyPRO     
        const uint ViKeyWEB = 4;   					//身份认证型加密狗ViKeyWEB
        const uint ViKeyTIME = 5;  					//时间型加密狗ViKeyTIME
        const uint ViKeyMultiFunctional = 10;  		//多功能加密狗  支持软件加密 支持文档加密
        const uint ViKeyMultiFunctionalTime = 11;   //多功能时钟加密狗

        //LED灯状态定义
        const uint LED_OFF = 0;   					//常灭
        const uint LED_ON = 1;   					//常亮
        const uint LED_BLINK = 2;   				//灯闪
        const uint LED_OFF_BLINK = 3;   			//平时常灭-通信时灯闪
        const uint LED_ON_BLINK = 4;                //平时常亮-通信时灯闪

        // 函数引用声明
        [DllImport("ViKey")]
        public static extern uint VikeyGetLibraryVersion(Byte[] pVersion);
        [DllImport("ViKey")]
        public static extern uint VikeyFind(ref uint pdwCount);
        [DllImport("ViKey")]
        public static extern uint VikeyFindEx(ref uint pdwCount);
        [DllImport("ViKey")]
        public static extern uint VikeyUninitialization();
        [DllImport("ViKey")]
        public static extern uint VikeyGetHID(ushort Index, ref uint pdwHID);
        [DllImport("ViKey")]
        public static extern uint VikeyGetType(ushort Index, ref uint pType);
        [DllImport("ViKey")]
        public static extern uint VikeyGetLevel(ushort Index, ref Byte pLevel);
        [DllImport("ViKey")]
        public static extern uint VikeyGetFirmVersion(Byte[] pVersion);
        [DllImport("ViKey")]
        public static extern uint VikeyGetUserDataSize(ushort Index, ref uint pBytes);
        [DllImport("ViKey")]
        public static extern uint VikeyGetAdminDataSize(ushort Index, ref uint pBytes);
        [DllImport("ViKey")]
        public static extern uint VikeyUserLogin(ushort Index, Byte[] pUserPassword);
        [DllImport("ViKey")]
        public static extern uint VikeyAdminLogin(ushort Index, Byte[] pAdminPassword);
        [DllImport("ViKey")]
        public static extern uint VikeyResetPassword(ushort Index, Byte[] pNewUserPassword, Byte[] pNewAdminPassword);
        [DllImport("ViKey")]
        public static extern uint VikeyLogoff(ushort Index);
        [DllImport("ViKey")]
        public static extern uint VikeyReadData(ushort Index, ushort pStartAddress, ushort pBufferLength, Byte[] pBuffer);
        [DllImport("ViKey")]
        public static extern uint VikeyWriteData(ushort Index, ushort pStartAddress, ushort pBufferLength, Byte[] pBuffer);
        [DllImport("ViKey")]
        public static extern uint ViKeyRandom(ushort Index, ref ushort pReturn1, ref ushort pReturn2, ref ushort pReturn3, ref ushort pReturn4);
        [DllImport("ViKey")]
        public static extern uint VikeySeed(ushort Index, ref uint pSeed, ref ushort pReturn1, ref ushort pReturn2, ref ushort pReturn3, ref ushort pReturn4);
        [DllImport("ViKey")]
        public static extern uint VikeySetSoftIDString(ushort Index, Byte[] SoftIDString);
        [DllImport("ViKey")]
        public static extern uint VikeyGetSoftIDString(ushort Index, Byte[] SoftIDString);
        [DllImport("ViKey")]
        public static extern uint ViKeySetModule(ushort Index, ushort ModelueIndex, ushort pValue, ushort pDecrease);
        [DllImport("ViKey")]
        public static extern uint ViKeyCheckModule(ushort Index, ushort ModelueIndex, ref ushort pIsZero, ref ushort pCanDecrase);
        [DllImport("ViKey")]
        public static extern uint ViKeyDecraseModule(ushort Index, ushort ModelueIndex);

        [DllImport("ViKey")]
        public static extern uint VikeySetPtroductName(ushort Index, Byte[] szName);
        [DllImport("ViKey")]
        public static extern uint VikeyGetPtroductName(ushort Index, Byte[] szName);
        [DllImport("ViKey")]
        public static extern uint VikeyGetTime(ushort Index, Byte[] pTime);


        public void Uninitializatio() 
        {
            VikeyUninitialization(); //程序退出之前一定要调用此函数
        }
        public bool CheckViKey() 
        {

            uint HID, Count, ViKeyType = 0;
            Byte[] buffer = new Byte[256];
            uint retcode;
            ushort j;
            ushort Addr, Length;
            ushort data1, data2, data3, data4;

            string str1 = "1234567890123456";
            string DefaultUserPassword = "wwwbgsgs";
            string DefaultAdminPassword = "wwwbgsgs";
            string strSoftIDString = "1234ABCD";

            HID = Count = 0;
            data1 = data2 = data3 = data4 = 0;

            //查找加密狗
            retcode = VikeyFind(ref Count);
            if (retcode != 0)
            {
                System.Console.WriteLine("查找ViKey加密狗错误 error code: 0x{0:X4}", retcode);
                return false;
            }

            for (j = 0; j < Count; j++)
            {

                //获取加密狗硬件序列号(HID)
                retcode = VikeyGetHID(j, ref HID);
                if (retcode != 0)
                {
                    System.Console.WriteLine("获取硬件序列号错误 error code: 0x{0:x}", retcode);
                    return false;
                }
                System.Console.WriteLine("ViKey加密狗硬件序列号:" + HID);


                retcode = VikeyGetType(j, ref ViKeyType);
                if (retcode != 0)
                {
                    System.Console.WriteLine("获取ViKey加密狗类型 error code: 0x{0:x}", retcode);
                    return false;
                }
                System.Console.WriteLine("获取ViKey加密狗类型:" + ViKeyType);




                // 用户登录ViKey加密狗
                buffer = System.Text.Encoding.Default.GetBytes(DefaultUserPassword); // convert unicode to asccii
                retcode = VikeyUserLogin(j, buffer);
                if (retcode != 0)
                {
                    System.Console.WriteLine("用户登陆ViKey加密狗失败 error code: ", retcode);
                    return false;
                }
                //管理员登陆加密狗

                buffer = System.Text.Encoding.Default.GetBytes(DefaultAdminPassword); // convert unicode to asccii
                retcode = VikeyAdminLogin(j, buffer);
                if (retcode != 0)
                {
                    System.Console.WriteLine("管理员登陆ViKey加密狗失败 error code: ", retcode);
                    return false;
                }

      

                buffer = System.Text.Encoding.Default.GetBytes(str1); // convert unicode to asccii
                // write data to Vikey
                Addr = 0;
                Length = 16;
                retcode = VikeyWriteData(j, Addr, Length, buffer);
                if (retcode != 0)
                {
                    System.Console.WriteLine("写数据失败 error code: ", retcode);
                    return false;
                }
                System.Console.WriteLine("Write:" + str1);

                //		p1 = 4;
                //		p2 = 26;
                buffer = new Byte[str1.Length];

                // read dongle memory
                retcode = VikeyReadData(j, Addr, Length, buffer);
                if (retcode != 0)
                {
                    System.Console.WriteLine("读数据失败 error code: ", retcode);
                    return false;
                }
                str1 = System.Text.Encoding.ASCII.GetString(buffer);
                System.Console.WriteLine("Read:" + str1);

                // random generation function
                retcode = ViKeyRandom(j, ref data1, ref data2, ref data3, ref data4);
                if (retcode != 0)
                {
                    System.Console.WriteLine("获取随机数失败 error code: ", retcode);
                    return false;
                }
                System.Console.WriteLine("Random: " + data1);

                // write SoftID
                buffer = System.Text.Encoding.Default.GetBytes(strSoftIDString); // convert unicode to asccii

                retcode = VikeySetSoftIDString(j, buffer);
                if (retcode != 0)
                {
                    System.Console.WriteLine("设置软件ID error code: ", retcode);
                    return false;
                }
                System.Console.WriteLine("设置软件ID: " + strSoftIDString);

                // read SoftID
                buffer = new Byte[strSoftIDString.Length];
                retcode = VikeyGetSoftIDString(j, buffer);
                if (retcode != 0)
                {
                    System.Console.WriteLine("读取软件ID error code: ", retcode);
                    return false;
                }
                strSoftIDString = System.Text.Encoding.ASCII.GetString(buffer);
                System.Console.WriteLine("读取软件ID: " + strSoftIDString);

              


                // Logoff ViKey
                retcode = VikeyLogoff(j);
                if (retcode != 0)
                {
                    System.Console.WriteLine("注销ViKey加密狗 error code: ", retcode);
                    return false;
                }
            }

            return true;
        }
    }
}
