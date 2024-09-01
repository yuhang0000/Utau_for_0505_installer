using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utau_for_0505_installer
{
    public partial class exit : Form
    {
        public exit()
        {
            InitializeComponent();
        }

        public static class exit1
        {
            public static bool exit2 = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            exit1.exit2 = true;
        }

        //设定光标样式
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        private void exit_Load(object sender, EventArgs e)
        {
            // 从资源中读取光标文件
            byte[] cursorData1 = Utau_for_0505_installer.Resource.默认;
            byte[] cursorData2 = Utau_for_0505_installer.Resource.错误;

            // 创建一个临时文件来保存光标数据
            string tempFileName1 = Path.GetTempFileName();
            string tempFileName2 = Path.GetTempFileName();
            File.WriteAllBytes(tempFileName1, cursorData1);
            File.WriteAllBytes(tempFileName2, cursorData2);

            // 加载光标
            IntPtr Defaultcursor = LoadCursorFromFile(tempFileName1);
            IntPtr errcursor = LoadCursorFromFile(tempFileName2);

            // 设置光标
            Cursor Default = new Cursor(Defaultcursor);
            Cursor err = new Cursor(errcursor);
            this.Cursor = Default;
            this.button2.Cursor = err;
        }
    }
}
