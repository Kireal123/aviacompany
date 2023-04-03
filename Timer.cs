using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace aviacompany
{
    public partial class Timer : Form
    {
        public Timer()
        {
            InitializeComponent();
        }

        private void Timer_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Start();

        }

        int i = 10;
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (i != 0)
            {
                i -= 1;
                label1.Text = "Осталось: " + i + "";
            }
            else
            {
                this.Close();
            }
        }
    }
}
