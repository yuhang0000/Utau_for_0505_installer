using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utau_for_0505_installer
{
    public partial class Form1 : Form
    {
        //全局访问 Form1 实例
        public static Form1 让我看看 { get; private set; }
        public Form1()
        {
            InitializeComponent();
            让我看看 = this;
        }

        //关闭按钮
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static class 全局变量
        {
            public static Byte[] utau;
            public static bool noexit = false;
            public static bool areurun = false;
            public static string[] tips = { "看俺幹嘛?", "球球你裝個屋塔屋吧。\r\n\r\nヾ(^▽^*)))", "俺要調教您!" };
        }

        //设定光标样式
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);


        //计算剩余空间
        public string 计算磁盘剩余空间(string path)
        {
            String output = "0";
            try
            {
                DriveInfo Pan = new DriveInfo(path);
                output = Pan.TotalFreeSpace.ToString();
                Double free = double.Parse(Pan.TotalFreeSpace.ToString()) / (1024 * 1024);
                string houzhui = " MB";
                if (free > 1024)
                {
                    free = free / 1024;
                    houzhui = " GB";
                }
                free = Math.Round(free, 2);
                no31.label3.Text = "磁盤剩餘大小: " + free.ToString() + houzhui;
            }
            catch
            {
                no31.label3.Text = "磁盤剩餘大小: 0 MB";
                SystemSounds.Hand.Play();
                MessageBox.Show("該路徑訪問失效: \r\n" + path, "Oops!");
            }
            long size = 全局变量.utau.LongLength;
            Console.WriteLine("剩余空间: " + output);
            Console.WriteLine("所需空间: " + size);
            if (long.Parse(output) < size)
            {
                no31.label4.Visible = true;
            }
            else
            {
                no31.label4.Visible = false;
            }
            return output;
        }

        //开始安装
        public string 安装(string path,bool go2web = false)
        {
            no51.textBox1.Text = "開始解壓...";
            no51.progressBar1.Style = ProgressBarStyle.Blocks;
            no51.progressBar1.Value = 50;
            return null;
        }

        //刚打开就运行
        private void Form1_Load(object sender, EventArgs e)
        { 
            // 从资源中读取光标文件
            byte[] cursorData1 = Utau_for_0505_installer.Resource.默认;
            byte[] cursorData2 = Utau_for_0505_installer.Resource.问号;
            byte[] cursorData3 = Utau_for_0505_installer.Resource.错误;

            // 创建一个临时文件来保存光标数据
            string tempFileName1 = Path.GetTempFileName();
            string tempFileName2 = Path.GetTempFileName();
            string tempFileName3 = Path.GetTempFileName();
            File.WriteAllBytes(tempFileName1, cursorData1);
            File.WriteAllBytes(tempFileName2, cursorData2);
            File.WriteAllBytes(tempFileName3, cursorData3);

            // 加载光标
            IntPtr Defaultcursor = LoadCursorFromFile(tempFileName1);
            IntPtr Helpcursor = LoadCursorFromFile(tempFileName2);
            IntPtr errcursor = LoadCursorFromFile(tempFileName3);

            // 设置光标
            Cursor Default = new Cursor(Defaultcursor);
            Cursor Help = new Cursor(Helpcursor);
            Cursor err = new Cursor(errcursor);
            this.Cursor = Default;
            this.label5.Cursor = Help;
            this.button1.Cursor = err;

            // 清理临时文件
            File.Delete(tempFileName1);
            File.Delete(tempFileName2);

            //计算安装压缩包大小
            全局变量.utau = Utau_for_0505_installer.Resource.UTAU;
            Double size = Math.Round(全局变量.utau.LongLength / (1024.0 * 1024.0), 2);
            no31.label2.Text = "所需大小: " + size.ToString() + " MB";

            //获取默认安装路径
            string path = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Utau\";
            if (path == null)
            {
                Console.WriteLine("Oops: 默认安装路径获取失败。");
                path = @"C:\Program Files (x86)\Utau\";
            }
            no31.textBox1.Text = path;
            计算磁盘剩余空间(path);

            //讨厌 “” , 稀饭 ""
            no21.checkedListBox1.Items[2] = "自動訂閲 \"Untitled_0505\" 嗶哩嗶哩頻道";
        }

        //下一步
        private void button2_Click(object sender, EventArgs e)
        {
            if (this.no31.Visible == false && this.no41.Visible == false)
            {
                this.no31.Visible = true;
                this.no31.Enabled = true;
                this.button3.Enabled = true;
                return;
            }
            else if (this.no31.Visible == true && this.no41.Visible == false)
            {
                this.no41.Visible = true;
                this.no41.Enabled = true;
                this.no31.Visible = false;
                this.no31.Enabled = false;
                this.button2.Text = "裝載(I)";
                if (this.no21.checkBox1.Checked == true)
                {
                    this.no41.textBox1.Text = "裝載類型：\r\n    默認裝載方案\r\n\r\n所選組件：\r\n    " +
                        "Utau 0.04.18 簡中版 主體程式\r\n    五號音源庫\r\n      " +
                        "\r\n附加事項：\r\n" +
                        "    裝載完成後跳轉至瀏覽器並自動訂閲 \"Untitled_0505\" 嗶哩嗶哩頻道\r\n";
                }
                else
                {
                    this.no41.textBox1.Text = "裝載類型：\r\n    用戶自選裝載方案\r\n\r\n所選組件：\r\n    " +
                        "Utau 0.04.18 簡中版 主體程式\r\n    五號音源庫\r\n      " +
                        "\r\n附加事項：\r\n" + "    无";
                }
            }
            else if (this.no31.Visible == false && this.no41.Visible == true)
            {
                this.no51.Visible = true;
                this.no51.Enabled = true;
                this.no41.Visible = false;
                this.no41.Enabled = false;
                this.button1.Enabled = false;
                this.button2.Enabled = false;
                this.button3.Enabled = false;
                全局变量.areurun = true;
                安装(no31.textBox1.Text);
            }

        }

        //上一步
        private void button3_Click(object sender, EventArgs e)
        {
            if (this.no31.Visible == false && this.no41.Visible == true)
            {
                this.no41.Visible = false;
                this.no41.Enabled = false;
                this.no31.Visible = true;
                this.no31.Enabled = true;
                this.button2.Text = "下一步(N)>";
                return;
            }
            else if (this.no31.Visible == true && this.no41.Visible == false)
            {
                this.no31.Visible = false;
                this.no31.Enabled = false;
                this.button3.Enabled = false;
            }
        }

        private void pictureBox2_MouseEnter(object sender, EventArgs e)
        {
            Random rm = new Random();
            no21.label4.Text = 全局变量.tips[rm.Next(0, 3)];
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            no21.label4.Text = "終止裝載。\r\n你敢點結束是吧，敢結束小心俺拿斧頭砍你!\r\n\r\n(╬▔皿▔)╯";
        }

        private void button2_MouseEnter(object sender, EventArgs e)
        {
            no21.label4.Text = "轉入下一頁面。";
        }

        private void button3_MouseEnter(object sender, EventArgs e)
        {
            no21.label4.Text = "轉入上一頁面。";
        }

        private void label5_MouseEnter(object sender, EventArgs e)
        {
            no21.label4.Text = "轉入關於頁面...";
        }

        //关于
        private void label5_Click(object sender, EventArgs e)
        {
            info infoform = new info();
            infoform.ShowDialog();
        }

        //阻止关闭
        [DllImport("user32.dll")]
        private static extern bool FlashWindow(IntPtr hwnd, bool bInvert);

        private async void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (全局变量.areurun == true)
            {
                e.Cancel = true;
                SystemSounds.Hand.Play();
                if (全局变量.noexit == false)
                {
                    全局变量.noexit = true;
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    全局变量.noexit = false;
                    FlashWindow(this.Handle, true);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, false);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, false);
                    await Task.Delay(30);
                    FlashWindow(this.Handle, false);
                    return;
                }
            }
            else
            {
                exit form = new exit();
                form.ShowDialog();
                if (exit.exit1.exit2 == false)
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
