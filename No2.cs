using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Utau_for_0505_installer
{
    public partial class No2 : UserControl
    {
        public No2()
        {
            InitializeComponent();
        }
        private bool isHandlingCheckEvent = false;
        private string[] 说明 = { "裝載 Utau 軟體至您的計算機上。", "食用先進的五號 (Untitled_0505) 音源庫。", "裝載完成後跳轉至瀏覽器並自動訂閲 \"Untitled_0505\" 嗶哩嗶哩頻道。" };

        private void No2_Load(object sender, EventArgs e)
        {
            //默认选中
            checkedListBox1.SetItemChecked(0, true);
            checkedListBox1.SetItemChecked(1, true);
            checkedListBox1.SetItemChecked(2, true);
        }

        private void checkedListBox1_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            // 防止无限递归
            if (isHandlingCheckEvent)
            {
                return;
            }

            isHandlingCheckEvent = true;

            try
            {
                // 如果用户尝试更改第一项或第二项的状态，则保持其为选中状态
                if (e.Index == 0 || e.Index == 1)
                {
                    e.NewValue = CheckState.Checked;
                }
                if (e.Index == 2)
                {
                    if(checkedListBox1.GetItemChecked(2) == false)
                    {
                        checkBox1.Checked = true;
                    }
                    else
                    {
                        checkBox1.Checked = false;
                    }
                }
            }
            finally
            {
                isHandlingCheckEvent = false;
            }
        }

        private void checkedListBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int index = checkedListBox1.IndexFromPoint(e.Location);
            if (index != -1)
            {
                label4.Text = 说明[index];
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkedListBox1.SetItemChecked(2, true);
            }
            else
            {
                checkedListBox1.SetItemChecked(2, false);
            }
        }

        private void checkBox1_MouseEnter(object sender, EventArgs e)
        {
            label4.Text = 说明[2];
        }
    }
}
