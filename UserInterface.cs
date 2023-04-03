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
    public partial class UserInterface : Form
    {
        public string email;
        public string name;
        public int id;
        public string crash;
        public UserInterface(string text)
        {
            InitializeComponent();
            email = text;
        }

        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet data;
        DateTime dateBegin;
        DateTime dateEnd;
        string current;
        DateTime date;
        void UserID()
        {
            try
            {
                connect.Open();
                SqlCommand comman = new SqlCommand();
                comman.Connection = connect;
                comman.CommandText = String.Format("SELECT [ID] From Users WHERE [Email] = '{0}'", email);
                var RoleId = comman.ExecuteReader();
                while (RoleId.Read())
                {
                    id = RoleId.GetInt32(0);
                }
                connect.Close();
            }
            catch (Exception e)
            {
                crash = e.Message.ToString();
            }
        }
        void GetList()
        {
            try
            {
                adapter = new SqlDataAdapter("SELECT *From Time WHERE UsersID = '" + id + "'", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Time");
                database.DataSource = data.Tables["Time"];
                connect.Close();
            }
            catch (Exception e)
            {
                crash = e.Message.ToString();
            }
        }
        void Error()
        {
            try
            {
                int[] mai = new int[4];
                for (int i = 0; i < 6; i++)
                {
                    mai[i] = 1;
                }
            }
            catch (Exception n)
            {
                crash = n.Message.ToString();
            }
        }
        void UserName()
        {
            try
            {
                connect.Open();
                SqlCommand comman = new SqlCommand();
                comman.Connection = connect;
                comman.CommandText = String.Format("SELECT [FirstName] From Users WHERE [Email] = '{0}'", email);
                var RoleId = comman.ExecuteReader();
                while (RoleId.Read())
                {
                    name = RoleId.GetString(0);
                }
                connect.Close();
            }
            catch (Exception e)
            {
                crash = e.Message.ToString();
            }
        }
        private void UserInterface_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aviacompanyDataSet2.Routes". При необходимости она может быть перемещена или удалена.
            this.routesTableAdapter.Fill(this.aviacompanyDataSet2.Routes);
            try
            {
                UserID();
                GetList();
                UserName();
                label1.Text = "Hi, " + name + ", Welcome to FlyToMonn Airlines.";
                label2.Text = "Time spent on system:";
                dateBegin = DateTime.Now;
                timer1.Interval = 1000;
                timer1.Start();
            }
            catch (Exception b)
            {
                crash = b.Message.ToString();
            }
        }

        private void UserInterface_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                dateEnd = DateTime.Now;
                date = DateTime.Now;
                connect.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = String.Format("INSERT INTO Time([Date],[Login time],[Logout time]," +
                "[Time spent on system],Crash,UsersID) VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}')",
                date.ToShortDateString(), dateBegin.ToLongTimeString(), dateEnd.ToLongTimeString(), label2.Text, crash, id);
                command.ExecuteNonQuery();
                connect.Close();
            }
            catch (Exception s)
            {
                crash = s.Message.ToString();
            }
        }

        public int h;
        public int m;
        public int s;
        private void timer1_Tick_1(object sender, EventArgs e)
        {
            if (m == 59)
            {
                m = 0;
                h += 1;
            }
            else
            {
                if (s == 59)
                {
                    m += 1;
                    s = 0;
                }
                else
                {
                    s += 1;
                }
            }
            label2.Text = "" + h + ":" + m + ":" + s + "";
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Login login = new Login();
            login.Show();
            this.Close();
        }
    }
}

