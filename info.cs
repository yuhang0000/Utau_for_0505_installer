using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
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

        private void info_Load(object sender, EventArgs e)
        {
            this.label5.Text = "版本: v" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            this.label6.Text = "構建時間: " + System.IO.File.GetLastWriteTime(typeof(info).Assembly.Location); ;
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
    }
}
