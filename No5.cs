using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Utau_for_0505_installer
{
    public partial class No5 : UserControl
    {
        public No5()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            this.textBox1.SelectionStart = this.textBox1.TextLength;
            this.textBox1.SelectionLength = 0;
            this.textBox1.ScrollToCaret();
            //Console.WriteLine(this.textBox1.SelectionStart.ToString());
        }
    }
}
