using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace aviacompany
{
    public partial class Import : Form
    {
        public Import()
        {
            InitializeComponent();
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = File.ReadAllText(open.FileName, Encoding.GetEncoding(1251));
                label4.Text = "31";
                label5.Text = "2";
                label6.Text = "0";
            }
        }
    }
}

