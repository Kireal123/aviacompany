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
    public partial class Manager : Form
    {
        int id_air;
        public Manager(string text)
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet data;
        DateTime blockDate;
        int blockTime;

        private void Manager_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aviacompanyDataSet1.Schedules". При необходимости она может быть перемещена или удалена.
            this.schedulesTableAdapter.Fill(this.aviacompanyDataSet1.Schedules);
            GetList();
            GetStyle();
            Airports();
            Block();
            comboBox3.Items.Add("Date");
            comboBox3.Items.Add("Time");
            comboBox3.Items.Add("EconomyPrice");
            comboBox3.Items.Add("Confirmed");
        }
        void GetList()
        {
            adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                "From Schedules " +
                "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                "JOIN Aircrafts ON AircraftID = Aircrafts.ID", connect);
            data = new DataSet();
            connect.Open();
            adapter.Fill(data, "Schedules");
            database.DataSource = data.Tables["Schedules"];
            connect.Close();
            database.Columns[2].HeaderText = "From";
            database.Columns[3].HeaderText = "To";
            database.Columns.Add("Business", "Business price");
            database.Columns.Add("FirstClass", "First class price");
        }
        void Airports()
        {
            comboBox1.Items.Add("All airports");
            comboBox2.Items.Add("All airports");
            DB db = new DB();
            SqlCommand comm = new SqlCommand("SELECT [Name] FROM Airports", db.getConnection());
            db.openConnection();
            var reader = comm.ExecuteReader();
            string l;
            while (reader.Read())
            {
                l = reader.GetString(0);
                comboBox1.Items.Add(l);
                comboBox2.Items.Add(l);
            }
        }
        void GetAir()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [ID] From Airports WHERE [Name] = '{0}'", comboBox2.Text);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_air = reader.GetInt32(0);
            }
            connect.Close();
        }
        void GetAir2()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [ID] From Airports WHERE [Name] = '{0}'", comboBox1.Text);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                id_air = reader.GetInt32(0);
            }
            connect.Close();
        }
        void GetStyle()
        {
            DB db = new DB();
            SqlCommand comm = new SqlCommand("SELECT [EconomyPrice] FROM Schedules", db.getConnection());
            db.openConnection();
            var reader = comm.ExecuteReader();
            double l;
            double j;
            int i = 0;
            while (reader.Read())
            {
                l = reader.GetInt32(0);
                j = l + ((l / 100) * 30);
                l = l + ((l / 100) * 35);
                database.Rows[i].Cells[7].Value = l;
                database.Rows[i].Cells[8].Value = j;
                i += 1;
            }
        }
        void GetStyl()
        {
            DB db = new DB();
            SqlCommand comm = new SqlCommand("SELECT [EconomyPrice] FROM Schedules ORDER BY " + comboBox3.Text + " " + des + "", db.getConnection());
            db.openConnection();
            var reader = comm.ExecuteReader();
            double l;
            double j;
            int i = 0;
            while (reader.Read())
            {
                l = reader.GetInt32(0);
                j = l + ((l / 100) * 30);
                l = l + ((l / 100) * 35);
                database.Rows[i].Cells[7].Value = l;
                database.Rows[i].Cells[8].Value = j;
                i += 1;
            }
        }
        void GetSty1()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [EconomyPrice] FROM Schedules " +
                "JOIN Routes ON Routes.ID = Schedules.RouteID " +
                "JOIN Airports B ON Routes.ArrivalAirportID = B.ID WHERE Routes.ArrivalAirportID = '{0}'", id_air);
            var reader = command.ExecuteReader();
            double l;
            double j;
            int i = 0;
            while (reader.Read())
            {
                l = reader.GetInt32(0);
                j = l + ((l / 100) * 30);
                l = l + ((l / 100) * 35);
                database.Rows[i].Cells[7].Value = l;
                database.Rows[i].Cells[8].Value = j;
                i += 1;
            }
            connect.Close();
        }
        void GetSty2()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [EconomyPrice] FROM Schedules " +
                "JOIN Routes ON Routes.ID = Schedules.RouteID " +
                "JOIN Airports B ON DepartureAirportID = B.ID WHERE DepartureAirportID = '{0}'", id_air);
            var reader = command.ExecuteReader();
            double l;
            double j;
            int i = 0;
            while (reader.Read())
            {
                l = reader.GetInt32(0);
                j = l + ((l / 100) * 30);
                l = l + ((l / 100) * 35);
                database.Rows[i].Cells[7].Value = l;
                database.Rows[i].Cells[8].Value = j;
                i += 1;
            }
            connect.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All airports")
            {
                GetList();
                Block();
            }
            else
            {
                GetAir2();
                database.Columns.Clear();
                adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                  "From Schedules " +
                  "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                  "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                  "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                  "JOIN Aircrafts ON AircraftID = Aircrafts.ID WHERE Routes.DepartureAirportID = '" + id_air + "'", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Schedules");
                database.DataSource = data.Tables["Schedules"];
                connect.Close();
                database.Columns[2].HeaderText = "From";
                database.Columns[3].HeaderText = "To";
                database.Columns.Add("Business", "Business price");
                database.Columns.Add("FirstClass", "First class price");
                GetSty2();
                Vlock1();
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "All airports")
            {
                GetList();
                Block();
            }
            else
            {
                GetAir();
                database.Columns.Clear();
                adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                  "From Schedules " +
                  "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                  "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                  "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                  "JOIN Aircrafts ON AircraftID = Aircrafts.ID WHERE Routes.ArrivalAirportID = '" + id_air + "'", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Schedules");
                database.DataSource = data.Tables["Schedules"];
                connect.Close();
                database.Columns[2].HeaderText = "From";
                database.Columns[3].HeaderText = "To";
                database.Columns.Add("Business", "Business price");
                database.Columns.Add("FirstClass", "First class price");
                GetSty1();
                Vlock2();
            }
        }
        void Block()
        {
            int i = 0;
            bool flor;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT Confirmed From Schedules");
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flor = reader.GetBoolean(0);
                if (flor == true)
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                else
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                i += 1;
            }
            connect.Close();
        }
        void Vlock1()
        {
            int i = 0;
            bool flor;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT Confirmed From Schedules JOIN Routes ON Schedules.RouteID = Routes.ID " +
                "WHERE Routes.DepartureAirportID = '" + id_air + "'");
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flor = reader.GetBoolean(0);
                if (flor == true)
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                else
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                i += 1;
            }
            connect.Close();
        }
        void Vlock2()
        {
            int i = 0;
            bool flor;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT Confirmed From Schedules JOIN Routes ON Schedules.RouteID = Routes.ID " +
            "WHERE Routes.ArrivalAirportID = '" + id_air + "'");
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flor = reader.GetBoolean(0);
                if (flor == true)
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                else
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                i += 1;
            }
            connect.Close();
        }
        void Filter1()
        {
            int i = 0;
            bool flor;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT Confirmed From Schedules WHERE Date = '" + dateTimePicker1.Value.ToShortDateString() + "'");
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flor = reader.GetBoolean(0);
                if (flor == true)
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                else
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                i += 1;
            }
            connect.Close();
        }
        void Filter2()
        {
            int i = 0;
            bool flor;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT Confirmed From Schedules " +
                "WHERE FlightNumber = '" + textBox1.Text + "' AND Date = '" + dateTimePicker1.Value.ToShortDateString() + "'");
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flor = reader.GetBoolean(0);
                if (flor == true)
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Green;
                else
                    database.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                i += 1;
            }
            connect.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int select = database.CurrentRow.Index;
            blockDate = Convert.ToDateTime(database.Rows[select].Cells[0].Value);
            blockTime = (int)database.Rows[select].Cells[6].Value;
            bool flori = true;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Confirmed] From Schedules WHERE [Date] = '{0}' AND [EconomyPrice] = '{1}'", blockDate.ToShortDateString(), blockTime);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flori = reader.GetBoolean(0);
            }
            connect.Close();
            if (flori == true)
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = String.Format("UPDATE Schedules SET Confirmed = '0' WHERE Date = '{0}' AND EconomyPrice = '{1}'", blockDate.ToShortDateString(), blockTime);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Рейс отменен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetList();
                Block();
            }
            else
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = String.Format("UPDATE Schedules SET Confirmed = '1' WHERE Date = '{0}' AND EconomyPrice = '{1}'", blockDate.ToShortDateString(), blockTime);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Рейс восстановлен", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetList();
                Block();
            }
        }
        string des = "ASC";

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (des == "DESC")
            {
                des = "ASC";
            }
            else
            {
                des = "DESC";
            }
            if (comboBox1.Text == "" && comboBox2.Text == "")
            {
                database.Columns.Clear();
                adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                  "From Schedules " +
                  "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                  "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                  "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                  "JOIN Aircrafts ON AircraftID = Aircrafts.ID ORDER BY " + comboBox3.Text + " " + des + "", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Schedules");
                database.DataSource = data.Tables["Schedules"];
                connect.Close();
                database.Columns[2].HeaderText = "From";
                database.Columns[3].HeaderText = "To";
                database.Columns.Add("Business", "Business price");
                database.Columns.Add("FirstClass", "First class price");
                GetSty2();
                Block();
            }
            else
            {
                if (comboBox1.Text != null)
                {
                    database.Columns.Clear();
                    adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                      "From Schedules " +
                      "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                      "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                      "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                      "JOIN Aircrafts ON AircraftID = Aircrafts.ID WHERE Routes.DepartureAirportID = '" + id_air + "' ORDER BY " + comboBox3.Text + " " + des + "", connect);
                    data = new DataSet();
                    connect.Open();
                    adapter.Fill(data, "Schedules");
                    database.DataSource = data.Tables["Schedules"];
                    connect.Close();
                    database.Columns[2].HeaderText = "From";
                    database.Columns[3].HeaderText = "To";
                    database.Columns.Add("Business", "Business price");
                    database.Columns.Add("FirstClass", "First class price");
                    GetSty2();
                    Vlock2();
                }
                else
                {
                    database.Columns.Clear();
                    adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                      "From Schedules " +
                      "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                      "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                      "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                      "JOIN Aircrafts ON AircraftID = Aircrafts.ID WHERE Routes.ArrivalAirportID = '" + id_air + "' ORDER BY " + comboBox3.Text + " " + des + "", connect);
                    data = new DataSet();
                    connect.Open();
                    adapter.Fill(data, "Schedules");
                    database.DataSource = data.Tables["Schedules"];
                    connect.Close();
                    database.Columns[2].HeaderText = "From";
                    database.Columns[3].HeaderText = "To";
                    database.Columns.Add("Business", "Business price");
                    database.Columns.Add("FirstClass", "First class price");
                    GetSty1();
                    Vlock1();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                database.Columns.Clear();
                adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                  "From Schedules " +
                  "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                  "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                  "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                  "JOIN Aircrafts ON AircraftID = Aircrafts.ID WHERE Date = '" + dateTimePicker1.Value.ToShortDateString() + "'", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Schedules");
                database.DataSource = data.Tables["Schedules"];
                connect.Close();
                database.Columns[2].HeaderText = "From";
                database.Columns[3].HeaderText = "To";
                database.Columns.Add("Business", "Business price");
                database.Columns.Add("FirstClass", "First class price");
                Filter1();
            }
            else
            {
                database.Columns.Clear();
                adapter = new SqlDataAdapter("SELECT [Date], [Time], A.IATACode, B.IATACode, Aircrafts.Name, FlightNumber, EconomyPrice " +
                  "From Schedules " +
                  "JOIN Routes ON Schedules.RouteID = Routes.ID " +
                  "JOIN Airports A ON Routes.DepartureAirportID = A.ID " +
                  "JOIN Airports B ON Routes.ArrivalAirportID = B.ID " +
                  "JOIN Aircrafts ON AircraftID = Aircrafts.ID WHERE FlightNumber = '" + textBox1.Text + "' AND Date = '" + dateTimePicker1.Value.ToShortDateString() + "'", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Schedules");
                database.DataSource = data.Tables["Schedules"];
                connect.Close();
                database.Columns[2].HeaderText = "From";
                database.Columns[3].HeaderText = "To";
                database.Columns.Add("Business", "Business price");
                database.Columns.Add("FirstClass", "First class price");
                Filter2();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Import import = new Import();
            import.Show();
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int select = database.CurrentRow.Index;
            DateTime dater = Convert.ToDateTime(database.Rows[select].Cells[0].Value);
            string fast = Convert.ToString(database.Rows[select].Cells[5].Value);
            Schedule_edit kol = new Schedule_edit(dater, fast);
            kol.Show();
            this.Hide();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
