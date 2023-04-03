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
    public partial class AddUsers : Form
    {
        int l;
        public AddUsers()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet data;
        private void AddUsers_Load(object sender, EventArgs e)
        {
            DB db = new DB();
            SqlCommand comm = new SqlCommand("SELECT [Title] FROM Offices", db.getConnection());
            db.openConnection();
            var reader = comm.ExecuteReader();
            string l;
            while (reader.Read())
            {
                l = reader.GetString(0);
                comboBox1.Items.Add(l);
            }
        }


        void GetList()
        {
            adapter = new SqlDataAdapter("SELECT *From Users", connect);
            data = new DataSet();
            connect.Open();
            adapter.Fill(data, "Users");
            connect.Close();
        }
        void Office()
        {
            if (comboBox1.Text != null)
            {
                connect.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = String.Format("SELECT [ID] From Offices WHERE [Title] = '{0}'", comboBox1.Text);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    l = reader.GetInt32(0);
                }
                connect.Close();
            }
            else
            {
                l = 2;
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && textBox3.Text != null && textBox4.Text != null && comboBox1.Text != null && dateTimePicker1.Value.ToShortDateString() != null)
            {
                SqlCommand command = new SqlCommand();
                Office();
                connect.Open();
                command.Connection = connect;
                command.CommandText = String.Format("INSERT Users " +
                    "(RoleID, Email, Password, FirstName, LastName, OfficeID, Birthdate, Active) " +
                    "VALUES('2', '{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '1')",
                    textBox1.Text, textBox4.Text, textBox3.Text, textBox2.Text, l, dateTimePicker1.Value.ToShortDateString());
                command.ExecuteNonQuery();
                connect.Close();
                GetList();
                MessageBox.Show("Пользователь зарагестрирован", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Заполните все поля, чтобы зарегистрироваться", "Ошибка регестрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            AdminInterface adminInterface = new AdminInterface();
            adminInterface.Show();
            this.Close();
        }
    }
    
}

