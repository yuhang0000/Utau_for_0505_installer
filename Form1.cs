using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.IO.Pipes;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utau_for_0505_installer
{
    public partial class Form1 : Form
    {

        //全局访问 Form1 实例
        public static Form1 让我看看 { get; private set; }
        public Form1(string[] args = null)
        {
            InitializeComponent();
            if (args.Length != 0)
            {
                //this.label1.Text = args[0];
                //MessageBox.Show(args[0]);
                //MessageBox.Show(args[1]);
                try
                {
                    if (args[1] != "true" && args[1] != "false")
                    {
                        MessageBox.Show("參數錯誤!");
                        Process.GetCurrentProcess().Kill();
                    }
                }
                catch
                {
                    MessageBox.Show("參數錯誤!");
                    Process.GetCurrentProcess().Kill();
                }
                全局变量.boot = args;
            }
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
            public static long zipsize = 0;
            public static int zipfilenum = 0;
            public static string[] tips = { "看俺幹嘛?", "球球你裝個屋塔屋吧。\r\n\r\nヾ(^▽^*)))", "俺要調教您!" };
            public static string[] boot = null;
        }

        //设定光标样式
        [DllImport("user32.dll")]
        public static extern IntPtr LoadCursorFromFile(string fileName);

        //加上小盾牌图标
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(HandleRef hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        public void UACButton(bool uac = false)
        {
            uint BCM_SETSHIELD = 0x0000160C;
            if (uac == false)
            {
                SendMessage(new HandleRef(button2, button2.Handle), BCM_SETSHIELD, new IntPtr(0), new IntPtr(0));
                return;
            }
            SendMessage(new HandleRef(button2, button2.Handle), BCM_SETSHIELD, new IntPtr(0), new IntPtr(1));
        }

        //计算剩余空间
        public string 计算磁盘剩余空间(string path)
        {
            String output = "0";
            try
            {
                DriveInfo Pan = new DriveInfo(path);
                output = Pan.TotalFreeSpace.ToString();
                Double free = double.Parse(output) / (1024 * 1024);
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
            long size = 全局变量.zipsize;
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

        //判断目标文件夹是否需要管理员权限
        public bool UAC(string path)
        {
            bool noway = false;
            try
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                DirectorySecurity do_u_need_UAC = dir.GetAccessControl(AccessControlSections.Access);
            }
            catch
            {
                noway = true;
            }
            UACButton(noway);
            Console.WriteLine("是否需要UAC: " + noway);
            return noway;
        }

        //开始安装
        public string 安装(string path,bool go2web = false)
        {
            this.no51.Visible = true;
            this.no51.Enabled = true;
            this.no41.Visible = false;
            this.no41.Enabled = false;
            this.button1.Enabled = false;
            this.button2.Enabled = false;
            this.button3.Enabled = false;
            全局变量.areurun = true;
            no51.textBox1.Text = "開始解壓...";
            if(UAC(path) == true)
            {
                //新建个启动参数
                if (path.Substring(path.Length - 1) == @"\" || path.Substring(path.Length - 1) == @"/")
                {
                    path = path.Substring(0, path.Length - 1);
                }
                string start = "\"" + path + "\" " + go2web;
                //Console.WriteLine(start);

                var startInfo = new ProcessStartInfo
                {
                    FileName = Application.ExecutablePath,
                    Verb = "runas", // 请求以提升的权限运行
                    Arguments = start // 传递参数，用于识别是否以管理员模式运行
                };

                // 使用ProcessStartInfo启动一个新的进程
                try
                {
                    if (Process.Start(startInfo) != null)
                    {
                        Console.WriteLine("进程已作为管理员启动！");
                        Process.GetCurrentProcess().Kill();
                    }
                    else
                    {
                        Console.WriteLine("未能启动进程，请确认您是否有足够的权限！");
                    }
                }
                catch 
                {
                    Console.WriteLine("未能启动进程，请确认您是否有足够的权限！");
                }
            }

            //把鸭嗦包放进内存流里
            using (var ms = new MemoryStream(全局变量.utau))
            {
                using (ZipArchive zip = new ZipArchive(ms))
                {
                    
                }
            }
            //this.pictureBox2.Image = Utau_for_0505_installer.Resource.Sprite_0002_01;
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
            using (var ms = new MemoryStream(全局变量.utau))
            {
                using (ZipArchive zip = new ZipArchive(ms))
                {
                    //文件总数
                    全局变量.zipfilenum = zip.Entries.Count;
                    foreach (var entry in zip.Entries)
                    {
                        全局变量.zipsize += entry.Length;
                    }
                }
            }
            //Double size = Math.Round(全局变量.utau.LongLength / (1024.0 * 1024.0), 2);
            Double size = Math.Round(全局变量.zipsize / (1024.0 * 1024.0), 2);
            no31.label2.Text = "解壓所需大小: " + size.ToString() + " MB";
            no51.progressBar1.Maximum = 全局变量.zipfilenum;

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

            //接受启动参数
            if (全局变量.boot != null)
            {
                //先判断目标文件夹能不能访问
                long freesize = long.Parse(计算磁盘剩余空间(全局变量.boot[0]));
                if (freesize < 全局变量.zipsize)
                {
                    Double free = freesize / (1024 * 1024);
                    string houzhui = " MB";
                    if (free > 1024)
                    {
                        free = free / 1024;
                        houzhui = " GB";
                    }
                    free = Math.Round(free, 2);
                    SystemSounds.Hand.Play();
                    MessageBox.Show("磁盤剩餘大小不足以解壓文檔!\r\n磁盤剩餘大小: " + free.ToString() + houzhui +
                        "\r\n" + no31.label2.Text, "Oops!");
                    return;
                }
                安装(全局变量.boot[0], bool.Parse(全局变量.boot[1]));
            }

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
                UAC(no31.textBox1.Text);
                if (this.no21.checkBox1.Checked == true)
                {
                    this.no41.textBox1.Text = "裝載類型：\r\n    默認裝載方案\r\n\r\n裝載路徑: \r\n    " +
                        no31.textBox1.Text + "\r\n\r\n所選組件：\r\n    " +
                        "Utau 0.04.18 簡中版 主體程式\r\n    五號音源庫\r\n      " +
                        "\r\n附加事項：\r\n" +
                        "    裝載完成後跳轉至瀏覽器並自動訂閲 \"Untitled_0505\" 嗶哩嗶哩頻道";
                }
                else
                {
                    this.no41.textBox1.Text = "裝載類型：\r\n    用戶自選裝載方案\r\n\r\n裝載路徑: \r\n    " + 
                        no31.textBox1.Text + "\r\n\r\n所選組件：\r\n    " +
                        "Utau 0.04.18 簡中版 主體程式\r\n    五號音源庫\r\n      " +
                        "\r\n附加事項：\r\n" + "    无";
                }
            }
            else if (this.no31.Visible == false && this.no41.Visible == true)
            {
                //先判断目标文件夹能不能访问
                long freesize = long.Parse(计算磁盘剩余空间(no31.textBox1.Text));
                if (freesize < 全局变量.zipsize)
                {
                    Double free = freesize / (1024 * 1024);
                    string houzhui = " MB";
                    if (free > 1024)
                    {
                        free = free / 1024;
                        houzhui = " GB";
                    }
                    free = Math.Round(free, 2);
                    SystemSounds.Hand.Play();
                    MessageBox.Show("磁盤剩餘大小不足以解壓文檔!\r\n磁盤剩餘大小: " + free.ToString() + houzhui + 
                        "\r\n" + no31.label2.Text,"Oops!");
                    return;
                }
                安装(no31.textBox1.Text, no21.checkBox1.Checked);
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
                UACButton(false);
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

        //强制退出
        private void label6_DoubleClick(object sender, EventArgs e)
        {
            Process.GetCurrentProcess().Kill();
        }
    }
}
