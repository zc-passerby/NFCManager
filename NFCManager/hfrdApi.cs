using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace NFCManager
{
    public class hfrdApi        //hfrdapi.dll库函数引用
    {
        //====================================系统函数=============================================
        [DllImport("hfrdapi.dll")]  //获取动态库版本号
        public static extern int Sys_GetLibVersion(ref UInt32 pVer);

        [DllImport("hfrdapi.dll")]  //获取连接到PC机的设备数
        public static extern int Sys_GetDeviceNum(UInt16 vid, UInt16 pid, ref UInt32 pNum);

        [DllImport("hfrdapi.dll")]  //通过设备索引号、VID、PID获取设备序列号字符串描述符
        public static extern int Sys_GetHidSerialNumberStr(UInt32 deviceIndex,
                                                    UInt16 vid,
                                                    UInt16 pid,
                                                    ref Char deviceString,
                                                    UInt32 deviceStringLength);

        [DllImport("hfrdapi.dll")]  //打开HID设备
        public static extern int Sys_Open(ref IntPtr device,
                                   UInt32 index,
                                   UInt16 vid,
                                   UInt16 pid);

        [DllImport("hfrdapi.dll")]  //查询设备是否已经打开
        public static extern bool Sys_IsOpen(IntPtr device);

        [DllImport("hfrdapi.dll")]  //关闭设备
        public static extern int Sys_Close(ref IntPtr device);

        [DllImport("hfrdapi.dll")]  //设置数据包传输超时等待时间
        public static extern int Sys_SetTimeouts(IntPtr device,
                                          UInt32 getRepotTimeout,
                                          UInt32 setReportTimeout);

        [DllImport("hfrdapi.dll")]  //设置指示灯颜色
        public static extern int Sys_SetLight(IntPtr device, byte color);

        [DllImport("hfrdapi.dll")]  //蜂鸣
        public static extern int Sys_SetBuzzer(IntPtr device, byte msec);

        [DllImport("hfrdapi.dll")]  //设置读写器天线状态
        public static extern int Sys_SetAntenna(IntPtr device, byte mode);

        [DllImport("hfrdapi.dll")]  //设置读写器非接触工作方式（根据卡类型不同方式不同）
        public static extern int Sys_InitType(IntPtr device, byte type);

        //==================================M1卡函数==========================================
        [DllImport("hfrdapi.dll")]  //寻TYPE_A卡
        public static extern int TyA_Request(IntPtr device, byte mode, ref UInt16 pTagType);

        [DllImport("hfrdapi.dll")]  //TYPE_A卡防冲撞(Level 1)
        public static extern int TyA_Anticollision(IntPtr device, byte bcnt, byte[] pSnr, ref byte pLen);

        [DllImport("hfrdapi.dll")]  //TYPE_A卡选卡(Level 1)，可在TyA_Request()或TyA_Anticollision()之后执行
        public static extern int TyA_Select(IntPtr device, byte[] pSnr, byte snrLen, ref byte pSak);

        [DllImport("hfrdapi.dll")]  //命令已激活的TYPE_A卡进入HALT状态
        public static extern int TyA_Halt(IntPtr device);

        [DllImport("hfrdapi.dll")]  //用当前发送的密钥验证Mifare One卡
        public static extern int TyA_CS_Authentication2(IntPtr device, byte mode, byte block, byte[] pKey);

        [DllImport("hfrdapi.dll")]  //读取Mifare One卡中的1块数据
        public static extern int TyA_CS_Read(IntPtr device, byte block, byte[] pData, ref byte pLen);

        [DllImport("hfrdapi.dll")]  //向Mifare One卡写入1块数据
        public static extern int TyA_CS_Write(IntPtr device, byte block, byte[] pData);

        [DllImport("hfrdapi.dll")]  //将Mifare One卡某一块初始化为钱包
        public static extern int TyA_CS_InitValue(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]  //读取Mifare One卡钱包值
        public static extern int TyA_CS_ReadValue(IntPtr device, byte block, ref Int32 pValue);

        [DllImport("hfrdapi.dll")]  //Mifare One卡扣款
        public static extern int TyA_CS_Decrement(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]  //Mifare One卡充值
        public static extern int TyA_CS_Increment(IntPtr device, byte block, Int32 value);

        [DllImport("hfrdapi.dll")]  //Mifare One卡数据回传
        public static extern int TyA_CS_Restore(IntPtr device, byte block);

        [DllImport("hfrdapi.dll")]  //将M1卡BUFFER中的块数据传入指定的块
        public static extern int TyA_CS_Transfer(IntPtr device, byte block);
    }
}
