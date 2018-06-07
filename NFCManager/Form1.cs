using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

using SkinSharp;

namespace NFCManager
{
    public partial class Form1 : Form
    {
        public SkinH_Net skin;
        IntPtr g_hDevice = (IntPtr)(-1); //g_hDevice must init as -

        [DllImport("kernel32.dll")]
        public static extern void Sleep(int dwMilliseconds);

        private class SkinItem
        {
            public string Name;
            public string Value;

            public SkinItem(string name, string value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString()
            {
                return Value;
            }
        }

        public static byte GetHexBitsValue(byte ch)
        {
            byte sz = 0;
            if (ch <= '9' && ch >= '0')
                sz = (byte)(ch - 0x30);
            if (ch <= 'F' && ch >= 'A')
                sz = (byte)(ch - 0x37);
            if (ch <= 'f' && ch >= 'a')
                sz = (byte)(ch - 0x57);

            return sz;
        }

        public static byte[] ToDigitsBytes(string theHex)
        {
            byte[] bytes = new byte[theHex.Length / 2 + (((theHex.Length % 2) > 0) ? 1 : 0)];
            for (int i = 0; i < bytes.Length; i++)
            {
                char lowbits = theHex[i * 2];
                char highbits;

                if ((i * 2 + 1) < theHex.Length)
                    highbits = theHex[i * 2 + 1];
                else
                    highbits = '0';

                int a = (int)GetHexBitsValue((byte)lowbits);
                int b = (int)GetHexBitsValue((byte)highbits);
                bytes[i] = (byte)((a << 4) + b);
            }

            return bytes;
        }

        public static String byteHEX(Byte ib)
        {
            String _str = String.Empty;
            try
            {
                char[] Digit = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A',
                'B', 'C', 'D', 'E', 'F' };
                char[] ob = new char[2];
                ob[0] = Digit[(ib >> 4) & 0X0F];
                ob[1] = Digit[ib & 0X0F];
                _str = new String(ob);
            }
            catch (Exception)
            {
                new Exception("对不起有错。");
            }
            return _str;

        }

        public static string ToHexString(byte[] bytes)
        {
            String hexString = String.Empty;
            for (int i = 0; i < bytes.Length; i++)
                hexString += byteHEX(bytes[i]);

            return hexString;
        }

        public Form1()
        {
            skin = new SkinH_Net();
            //skin.AttachEx("Skins\\skinh.she", "");
            InitializeComponent();
        }

        public void LoadSkins()
        {
            JustForSkin.Items.Add(new SkinItem("不使用皮肤", "不使用皮肤"));
            JustForSkin.SelectedIndex = 0;

            DirectoryInfo dir = new DirectoryInfo("Skins");
            FileInfo[] fileList = dir.GetFiles();
            foreach(FileInfo f in fileList)
            {
                JustForSkin.Items.Add(new SkinItem(f.Name, f.Name));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadSkins();
            memberNo.Text = "";
            memberNo.ReadOnly = true;
            devNo.Text = "";
            devNo.ReadOnly = true;
        }

        private void JustForSkin_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(JustForSkin.SelectedIndex == 0)
            {
                skin.Detach();
            }

            string skinFileName = "Skins\\" + JustForSkin.Text;
            skin.AttachEx(skinFileName, "");
        }

        private void radioChecked(object sender, EventArgs e)
        {
            memberNo.Text = "";
            devNo.Text = "";
            if(radioButton1.Checked)
            {
                memberNo.ReadOnly = false;
                devNo.ReadOnly = true;
            }
            if(radioButton2.Checked)
            {
                memberNo.ReadOnly = false;
                devNo.ReadOnly = false;
            }
        }

        private void onlyNumberKeyPress(object sender, KeyPressEventArgs e)
        {
            if(e.KeyChar != 8 && !Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void setNumberRange(object sender, EventArgs e)
        {
            TextBox _textBox = (TextBox)sender;
            int iMax = 0;
            switch(_textBox.Name)
            {
                case "memberNo":
                    iMax = 9999;
                    break;
                case "devNo":
                    iMax = 99999;
                    break;
                default:
                    iMax = 0;
                    break;
            }
            if(_textBox.Text != null && _textBox.Text != "")
            {
                if(int.Parse(_textBox.Text) > iMax)
                {
                    _textBox.Text = iMax.ToString();
                }
            }
        }

        private bool openDevice()
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.Sys_Open(ref g_hDevice, 0, 0x0416, 0x8020);
            if (0 != status)
            {
                strError = "打开读卡器失败" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool closeDevice()
        {
            int status;         //返回状态
            string strError;    //错误信息
            if (hfrdApi.Sys_IsOpen(g_hDevice))
            {
                status = hfrdApi.Sys_Close(ref g_hDevice);
                if (0 != status)
                {
                    strError = "关闭读卡器失败" + status.ToString();
                    MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            return true;
        }

        private bool setAntenna(byte mode)  //mode   [IN] 天线状态, 0 = 关闭天线, 1 = 开启天线
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.Sys_SetAntenna(g_hDevice, mode);
            if (0 != status)
            {
                if (mode == (byte)0)
                    strError = "关闭读卡器天线失败" + status.ToString();
                else
                    strError = "打开读卡器天线失败" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool initType(byte type)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.Sys_InitType(g_hDevice, type);
            if (0 != status)
            {
                strError = "初始化读卡器失败" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool findCardA(byte mode, ref ushort tagType)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.TyA_Request(g_hDevice, mode, ref tagType);
            if (0 != status)
            {
                strError = "寻卡失败，请把卡片放在读卡器上" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool anticollisionCardA(byte bcnt, byte[] snr, ref byte len)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.TyA_Anticollision(g_hDevice, bcnt, snr, ref len);
            if (0 != status)
            {
                strError = "防冲突失败，请重新读卡" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool selectCardA(byte[] snr, byte len, ref byte sak)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.TyA_Select(g_hDevice, snr, len, ref sak);
            if (0 != status)
            {
                strError = "选定卡片失败，请重新读卡" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool authenticationCardA(byte mode, byte block, byte[] bytesKey)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.TyA_CS_Authentication2(g_hDevice, mode, block, bytesKey);
            if (0 != status)
            {
                strError = "验证密码失败，请重新读卡验证" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool readCardA(byte block, byte[] dataBuffer, ref byte len)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.TyA_CS_Read(g_hDevice, block, dataBuffer, ref len);
            if(0 != status || len != 16)
            {
                strError = "读卡失败，请重新读卡" + status.ToString();
                if (block == BLOCK_MEMBER_NO)
                    strError = "读工号失败，请重新读卡" + status.ToString();
                else if (block == BLOCK_DEV_NO)
                    strError = "读设备号失败，请重新读卡" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool writeCardA(byte block, byte[] dataBuffer)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.TyA_CS_Write(g_hDevice, block, dataBuffer);
            if (0 != status)
            {
                strError = "写卡失败，请重新写卡" + status.ToString();
                if (block == BLOCK_MEMBER_NO)
                    strError = "写工号失败，请重新写卡" + status.ToString();
                else if (block == BLOCK_DEV_NO)
                    strError = "写设备号失败，请重新写卡" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        private bool setBuzzer(byte msec)
        {
            int status;         //返回状态
            string strError;    //错误信息
            status = hfrdApi.Sys_SetBuzzer(g_hDevice, msec);
            if (0 != status)
            {
                strError = "蜂鸣失败" + status.ToString();
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

        //卡片存储空间定义
        private const byte BLOCK_MEMBER_NO = (byte)1;
        private const byte BLOCK_DEV_NO = (byte)2;

        private void readCard_Click(object sender, EventArgs e)
        {
            memberNo.Text = "";
            devNo.Text = "";

            string strError;                //错误信息
            byte mode = 0x52;               //寻卡模式 -- WUPA方式，寻所有状态的卡，所有在感应区的卡都可以作出响应
            /*  pTagType
             *  0x0044 = Ultralight/Ultralight C/NTAG203/NTAG210/NTAG212/NTAG213/NTAG215/NTAG216
             *  0x0004 = Mifare Classic 1K/FM11RF08
             *  0x0002 = Mifare Classic 4K/FM11RF32
             *  0x0344 = Mifare_DESFire
             *  0x0008 = Mifare_Pro
             *  0x0304 = Mifare_ProX
             *  0x3300 = SHC1102
             */
            ushort tagType = 0;                 //响应信息，可用来指示卡类型
            byte bcnt = 0;                      //无特殊含义，设置为0即可
            byte[] snr = new byte[256];         //返回的卡序列号
            byte len = 255;                     //返回序列号的长度
            byte sak = 0;                       //1字节的SAK响应信息
            byte[] dataBuffer = new byte[256];  //数据缓存
            byte cLen = 0;                      //数据长度

            //=================== Connect the reader ===================
            //Check whether the reader is connected or not
            //If the reader is already open , close it firstly
            if (!closeDevice())
                return;

            //Connect
            if (!openDevice())
                return;

            //============= Init the reader before operating the card ============
            //Close antenna of the reader
            if (!setAntenna(0))
                return;
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

            //Set the reader's working mode
            if (!initType((byte)'A'))
                return;
            Sleep(5); //Appropriate delay after Sys_InitType operating

            //Open antenna of the reader
            if (!setAntenna(1))
                return;
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating

            //Check whether the reader is connected or not
            if (!hfrdApi.Sys_IsOpen(g_hDevice))
            {
                strError = "读卡器没有打开，可能打开失败了！";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //搜寻所有的卡
            if (!findCardA(mode, ref tagType))
                return;

            //TYPE_A卡防冲撞(Level 1) 返回卡的序列号
            if (!anticollisionCardA(bcnt, snr, ref len))
                return;

            //锁定一张ISO14443-3 TYPE_A 卡 选卡
            if (!selectCardA(snr, len, ref sak))
                return;

            //Check whether the reader is connected or not
            if (!hfrdApi.Sys_IsOpen(g_hDevice))
            {
                strError = "读卡器没有打开，可能打开失败了！";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //用当前发送的密钥验证Mifare One卡
            byte[] bytesKey = ToDigitsBytes("FFFFFFFFFFFF");
            byte[] card_key = new byte[6];
            card_key[0] = 0xFF;
            card_key[1] = 0xFF;
            card_key[2] = 0xFF;
            card_key[3] = 0xFF;
            card_key[4] = 0xFF;
            card_key[5] = 0xFF;
            if (!authenticationCardA(mode, BLOCK_MEMBER_NO, card_key))
                return;

            //开始读取卡片数据
            if (!readCardA(BLOCK_MEMBER_NO, dataBuffer, ref cLen))
                return;

            int i;
            byte[] memBytesData = new byte[16];
            for (i = 0; i < memBytesData.Length; i++)
                memBytesData[i] = Marshal.ReadByte(dataBuffer, i);

            if (!readCardA(BLOCK_DEV_NO, dataBuffer, ref cLen))
                return;

            byte[] devBytesData = new byte[16];
            for (i = 0; i < devBytesData.Length; i++)
                devBytesData[i] = Marshal.ReadByte(dataBuffer, i);

            if (devBytesData[0] == 0 && devBytesData[1] == 0 && devBytesData[2] == 0x27 && devBytesData[3] == 0x10)
            {
                radioButton1.Checked = true;
                radioButton2.Checked = false;
                memberNo.Text = ToHexString(memBytesData);
                devNo.Text = "";
            }
            else
            {
                radioButton1.Checked = false;
                radioButton2.Checked = true;
                memberNo.Text = ToHexString(memBytesData);
                devNo.Text = ToHexString(devBytesData);
            }

            //==================== Success Tips ====================
            //Beep 200 ms
            if (!setBuzzer(200))
                return;
        }

        private void writeCard_Click(object sender, EventArgs e)
        {
            string strError;                //错误信息
            byte mode = 0x52;               //寻卡模式 -- WUPA方式，寻所有状态的卡，所有在感应区的卡都可以作出响应
            /*  pTagType
             *  0x0044 = Ultralight/Ultralight C/NTAG203/NTAG210/NTAG212/NTAG213/NTAG215/NTAG216
             *  0x0004 = Mifare Classic 1K/FM11RF08
             *  0x0002 = Mifare Classic 4K/FM11RF32
             *  0x0344 = Mifare_DESFire
             *  0x0008 = Mifare_Pro
             *  0x0304 = Mifare_ProX
             *  0x3300 = SHC1102
             */
            ushort tagType = 0;                 //响应信息，可用来指示卡类型
            byte bcnt = 0;                      //无特殊含义，设置为0即可
            byte[] snr = new byte[256];         //返回的卡序列号
            byte len = 255;                     //返回序列号的长度
            byte sak = 0;                       //1字节的SAK响应信息
            byte[] dataBuffer = new byte[256];  //数据缓存
            byte cLen = 0;                      //数据长度

            byte[] memBytesData = new byte[16];
            byte[] devBytesData = new byte[16];
            //判断员工号和设备号是否输入正确
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                strError = "请选择卡片类型";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (memberNo.Text == null || memberNo.Text == "")
            {
                strError = "请输入员工号";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            memBytesData = ToDigitsBytes(memberNo.Text);
            
            if (radioButton1.Checked)
            {
                devBytesData[0] = 0;
                devBytesData[1] = 0;
                devBytesData[2] = 0x27;
                devBytesData[3] = 0x10;
            }
            else if (radioButton2.Checked)
            {
                if (devNo.Text == null || devNo.Text == "")
                {
                    strError = "请输入设备号";
                    MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                devBytesData = ToDigitsBytes(devNo.Text);
            }

            //=================== Connect the writer ===================
            //Check whether the reader is connected or not
            //If the reader is already open , close it firstly
            if (!closeDevice())
                return;

            //Connect
            if (!openDevice())
                return;

            //============= Init the reader before operating the card ============
            //Close antenna of the reader
            if (!setAntenna(0))
                return;
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

            //Set the reader's working mode
            if (!initType((byte)'A'))
                return;
            Sleep(5); //Appropriate delay after Sys_InitType operating

            //Open antenna of the reader
            if (!setAntenna(1))
                return;
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating

            //Check whether the reader is connected or not
            if (!hfrdApi.Sys_IsOpen(g_hDevice))
            {
                strError = "读卡器没有打开，可能打开失败了！";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //搜寻所有的卡
            if (!findCardA(mode, ref tagType))
                return;

            //TYPE_A卡防冲撞(Level 1) 返回卡的序列号
            if (!anticollisionCardA(bcnt, snr, ref len))
                return;

            //锁定一张ISO14443-3 TYPE_A 卡 选卡
            if (!selectCardA(snr, len, ref sak))
                return;

            //Check whether the reader is connected or not
            if (!hfrdApi.Sys_IsOpen(g_hDevice))
            {
                strError = "读卡器没有打开，可能打开失败了！";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //用当前发送的密钥验证Mifare One卡
            byte[] bytesKey = ToDigitsBytes("FFFFFFFFFFFF");
            //byte[] card_key = { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF };
            byte[] card_key = new byte[6];
            card_key[0] = 0xFF;
            card_key[1] = 0xFF;
            card_key[2] = 0xFF;
            card_key[3] = 0xFF;
            card_key[4] = 0xFF;
            card_key[5] = 0xFF;
            if (!authenticationCardA(mode, BLOCK_MEMBER_NO, card_key)) Console.WriteLine("~~~");
                //return;

            //开始写入卡片数据
            if (!writeCardA(BLOCK_MEMBER_NO, memBytesData))
                return;

            if (!writeCardA(BLOCK_DEV_NO, devBytesData))
                return;

            //==================== Success Tips ====================
            //Beep 200 ms
            if (!setBuzzer(200))
                return;
        }
    }
}
