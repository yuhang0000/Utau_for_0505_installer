using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Utau_for_0505_installer;
using System.IO;

namespace Utau_for_0505_installer
{
    public partial class No3 : UserControl
    {

        public No3()
        {
            InitializeComponent();
        }

        //选择文件夹
        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.Description = "選擇裝載路徑: ";

            if (dialog.ShowDialog() == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                textBox1.Text = path;
                Form1.让我看看.计算磁盘剩余空间(path);
                //Utau_for_0505_installer.Form1.让我看看.UAC(path);
            }
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            Form1.让我看看.计算磁盘剩余空间(this.textBox1.Text);
            //Utau_for_0505_installer.Form1.让我看看.UAC(this.textBox1.Text);
        }
    }
}
