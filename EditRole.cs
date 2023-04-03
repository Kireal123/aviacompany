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
    public partial class EditRole : Form
    {
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet data;
        public string email;
        public EditRole(string text)
        {
            InitializeComponent();
            email = text;
        }
        int l;
        string pass;
        DateTime date;
        bool active;
        int role;
        string name;
        string familia;
        int office;
        string off;
        void GetList()
        {
            adapter = new SqlDataAdapter("SELECT *From Users", connect);
            data = new DataSet();
            connect.Open();
            adapter.Fill(data, "Users");
            connect.Close();
        }

        void GetUser()
        {
            connect.Open();
            SqlCommand comm = new SqlCommand();
            comm.Connection = connect;
            comm.CommandText = String.Format("SELECT [FirstName] From Users WHERE [Email] = '{0}'", email);
            var first = comm.ExecuteReader();
            while (first.Read())
            {
                name = first.GetString(0);
            }
            connect.Close();
            connect.Open();
            SqlCommand com = new SqlCommand();
            com.Connection = connect;
            com.CommandText = String.Format("SELECT [LastName] From Users WHERE [Email] = '{0}'", email);
            var last = com.ExecuteReader();
            while (last.Read())
            {
                familia = last.GetString(0);
            }
            connect.Close();
            connect.Open();
            SqlCommand co = new SqlCommand();
            co.Connection = connect;
            co.CommandText = String.Format("SELECT [OfficeID] From Users WHERE [Email] = '{0}'", email);
            var officer = co.ExecuteReader();
            while (officer.Read())
            {
                office = officer.GetInt32(0);
            }
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
        void Officer()
        {
            if (comboBox1.Text != null)
            {
                connect.Open();
                SqlCommand command = new SqlCommand();
                command.Connection = connect;
                command.CommandText = String.Format("SELECT [Title] From Offices " +
                    "JOIN Users ON Users.OfficeID = Offices.ID WHERE Users.Email = '{0}'", email);
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    off = reader.GetString(0);
                }
                connect.Close();
            }
            else
            {
                l = 2;
            }
        }
        void GetPass()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Password] From Users WHERE [Email] = '{0}'", textBox1.Text);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                pass = reader.GetString(0);
            }
            connect.Close();
        }

        void GetDate()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Birthdate] From Users WHERE [Email] = '{0}'", textBox1.Text);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                date = reader.GetDateTime(0);
            }
            connect.Close();
        }

        void GetActive()
        {
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Active] From Users WHERE [Email] = '{0}'", textBox1.Text);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                active = reader.GetBoolean(0);
            }
            connect.Close();
        }

        void GetRole()
        {
            if (radioButton1.Checked == true)
            {
                role = 2;
            }
            else
            {
                role = 1;
            }
        }

        private void EditRole_Load(object sender, EventArgs e)
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
            GetUser();
            Officer();
            textBox1.Text = email;
            textBox2.Text = name;
            textBox3.Text = familia;
            comboBox1.Text = off;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != null && textBox2.Text != null && textBox3.Text != null && comboBox1.Text != null)
            {
                SqlCommand command = new SqlCommand();
                Office();
                GetPass();
                GetDate();
                GetRole();
                GetActive();
                connect.Open();
                command.Connection = connect;
                command.CommandText = String.Format("UPDATE Users SET " +
                    "RoleID = '{7}', Email = '{0}', Password = '{1}', FirstName = '{2}', LastName = '{3}', " +
                    "OfficeID = '{4}', Birthdate = '{5}', Active = '{6}' WHERE Email = '{0}'",
                    textBox1.Text, pass, textBox3.Text, textBox2.Text, l, date, active, role);
                command.ExecuteNonQuery();
                connect.Close();
                GetList();
                MessageBox.Show("Пользователь зарегистрирован", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Заполните все поля, чтобы зарегистрироваться", "Ошибка регистрации", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AdminInterface adminInterface = new AdminInterface();
            adminInterface.Show();
            this.Close();
        }
    }
}