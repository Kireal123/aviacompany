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
    public partial class Schedule_edit : Form
    {
        DateTime da;
        string flig;
        public Schedule_edit(DateTime date, string flight)
        {
            InitializeComponent();
            da = date;
            flig = flight;
        }
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet data;
        private void button2_Click(object sender, EventArgs e)
        {
            string lot = "";
            Manager manager = new Manager(lot);
            manager.Show();
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GetList();
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("UPDATE Schedules SET [Date] = '{1}', [Time] = '{2}', [EconomyPrice] = '{3}'  WHERE ID = '{0}'", flori, dateTimePicker1.Value.ToShortDateString(), textBox1.Text, textBox2.Text);
            command.ExecuteNonQuery();
            connect.Close();
            MessageBox.Show("Рейс изменен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public int flori;
        void GetList()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [ID] From Schedules WHERE [Date] = '{0}' AND [FlightNumber] = '{1}'", da.ToShortDateString(), flig);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flori = reader.GetInt32(0);
            }
            connect.Close();
        }
        private void Schedule_edit_Load(object sender, EventArgs e)
        {

        }
    }
}

