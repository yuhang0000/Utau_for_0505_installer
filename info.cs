using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static Utau_for_0505_installer.Form1;

namespace Utau_for_0505_installer
{
    public partial class info : Form
    {
        public info()
        {
            InitializeComponent();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer" , "http://yuhang0000.github.io/");
        }

        public void info_Load(object sender, EventArgs e)
        {
            this.label5.Text = "版本: v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.label6.Text = "組建時間: " + System.IO.File.GetLastWriteTime(typeof(info).Assembly.Location);
            // 从资源中读取光标文件
            byte[] cursorData1 = Utau_for_0505_installer.Resource.默认;
            byte[] cursorData2 = Utau_for_0505_installer.Resource.哇;

            // 创建一个临时文件来保存光标数据
            string tempFileName1 = Path.GetTempFileName();
            string tempFileName2 = Path.GetTempFileName();
            File.WriteAllBytes(tempFileName1, cursorData1);
            File.WriteAllBytes(tempFileName2, cursorData2);

            // 加载光标
            IntPtr Defaultcursor = LoadCursorFromFile(tempFileName1);
            IntPtr inkcursor = LoadCursorFromFile(tempFileName2);

            // 设置光标
            Cursor Default = new Cursor(Defaultcursor);
            Cursor ink = new Cursor(inkcursor);
            this.Cursor = Default;
            this.linkLabel1.Cursor = ink;
            this.linkLabel2.Cursor = ink;
            this.linkLabel3.Cursor = ink;
            this.linkLabel4.Cursor = ink;
            this.linkLabel5.Cursor = ink;

            // 清理临时文件
            File.Delete(tempFileName1);
            File.Delete(tempFileName2);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://x.com/MayFox555/");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://www.bilibili.com/read/cv7974422/");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://bowlroll.net/file/322764/");
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://github.com/yuhang0000/Utau_for_0505_installer/");
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("explorer", "http://www.nuget.org/packages/System.IO.Compression/");
        }
    }
}
