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

        private void readCard_Click(object sender, EventArgs e)
        {
            memberNo.Text = "";
            devNo.Text = "";

            int status;
            string strError;

            //=================== Connect the reader ===================
            //Check whether the reader is connected or not
            //If the reader is already open , close it firstly
            if (hfrdApi.Sys_IsOpen(g_hDevice))
            {
                status = hfrdApi.Sys_Close(ref g_hDevice);
                if(0 != status)
                {
                    strError = "关闭读卡器失败";
                    MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            //Connect
            status = hfrdApi.Sys_Open(ref g_hDevice, 0, 0x0146, 0x8020);
            if(0 != status)
            {
                strError = "打开读卡器失败";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            //============= Init the reader before operating the card ============
            //Close antenna of the reader
            status = hfrdApi.Sys_SetAntenna(g_hDevice, 0);
            if (0 != status)
            {
                strError = "关闭读卡器天线失败";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating 

            //Set the reader's working mode
            status = hfrdApi.Sys_InitType(g_hDevice, (byte)'A');
            if (0 != status)
            {
                strError = "初始化读卡器失败";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_InitType operating

            //Open antenna of the reader
            status = hfrdApi.Sys_SetAntenna(g_hDevice, 1);
            if (0 != status)
            {
                strError = "打开读卡器天线失败";
                MessageBox.Show(strError, "error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            Sleep(5); //Appropriate delay after Sys_SetAntenna operating


        }

        private void writeCard_Click(object sender, EventArgs e)
        {

        }
    }
}
