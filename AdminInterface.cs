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
    public partial class AdminInterface : Form
    {
        public AdminInterface()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");
        SqlDataAdapter adapter;
        DataSet data;
        int l = 0;

        string email;
        void GetList()
        {
            adapter = new SqlDataAdapter("SELECT [FirstName], [LastName], DATEDIFF(year, Birthdate, GetDate()) as Data, Roles.Title, Email, Offices.Title " +
                "From Users JOIN Roles ON Users.RoleID = Roles.ID " +
                "JOIN Offices ON Users.OfficeID = Offices.ID", connect);
            data = new DataSet();
            connect.Open();
            adapter.Fill(data, "Users");
            database.DataSource = data.Tables["Users"];
            connect.Close();
            database.Columns[0].HeaderText = "Name";
            database.Columns[2].HeaderText = "Age";
            database.Columns[2].Width = 50;
            database.Columns[4].Width = 150;
            database.Columns[3].HeaderText = "User Role";
            database.Columns[5].HeaderText = "Office";
        }

        private void AdminInterface_Load(object sender, EventArgs e)
        {
            // TODO: данная строка кода позволяет загрузить данные в таблицу "aviacompanyDataSet.Users". При необходимости она может быть перемещена или удалена.
            this.usersTableAdapter.Fill(this.aviacompanyDataSet.Users);
            comboBox1.Items.Add("All offices");
            GetList();
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
            Block();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int select = database.CurrentRow.Index;
            email = (string)database.Rows[select].Cells[4].Value;
            EditRole edit = new EditRole(this.email);
            edit.Show();
            this.Close();
        }

        void Block()
        {
            int i = 0;
            bool flor;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Active] From Users");
            var reader = command.ExecuteReader();
            
            connect.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int select = database.CurrentRow.Index;
            email = (string)database.Rows[select].Cells[4].Value;
            bool flori = false;
            int vito = 0;
            int id = 0;
            string login = "";
            string password = "";
            string name = "";
            string familia = "";
            int office = 0;
            DateTime date = DateTime.Today;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Active] From Users WHERE [Email] = '{0}'", email);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                flori = reader.GetBoolean(0);
            }
            connect.Close();
            connect.Open();
            SqlCommand comman = new SqlCommand();
            comman.Connection = connect;
            comman.CommandText = String.Format("SELECT [RoleID] From Users WHERE [Email] = '{0}'", email);
            var RoleId = comman.ExecuteReader();
            while (RoleId.Read())
            {
                id = RoleId.GetInt32(0);
            }
            connect.Close();
            connect.Open();
            SqlCommand comma = new SqlCommand();
            comma.Connection = connect;
            comma.CommandText = String.Format("SELECT [Password] From Users WHERE [Email] = '{0}'", email);
            var pass = comma.ExecuteReader();
            while (pass.Read())
            {
                password = pass.GetString(0);
            }
            connect.Close();
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
            connect.Open();
            SqlCommand c = new SqlCommand();
            c.Connection = connect;
            c.CommandText = String.Format("SELECT [Birthdate] From Users WHERE [Email] = '{0}'", email);
            var dater = c.ExecuteReader();
            while (dater.Read())
            {
                date = dater.GetDateTime(0);
            }
            connect.Close();
            if (flori == true)
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = String.Format("UPDATE Users SET " +
                    "RoleID = '{0}', Email = '{1}', Password = '{2}', FirstName = '{3}', LastName = '{4}', " +
                    "OfficeID = '{5}', Birthdate = '{6}', Active = '0' WHERE Email = '{1}'",
                    id, email, password, name, familia, office, date);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Пользователь заблокирован", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetList();
                Block();
            }
            else
            {
                connect.Open();
                command.Connection = connect;
                command.CommandText = String.Format("UPDATE Users SET " +
                     "RoleID = '{0}', Email = '{1}', Password = '{2}', FirstName = '{3}', LastName = '{4}', " +
                     "OfficeID = '{5}', Birthdate = '{6}', Active = '1' WHERE Email = '{1}'",
                     id, email, password, name, familia, office, date);
                command.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Пользователь разблокирован", "Успешно", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetList();
                Block();
            }
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

        void Vlock()
        {
            int i = 0;
            bool flor = false;
            connect.Open();
            SqlCommand command = new SqlCommand();
            command.Connection = connect;
            command.CommandText = String.Format("SELECT [Active] From Users WHERE OfficeID = '" + l + "'");
            var reader = command.ExecuteReader();
           
            connect.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "All offices")
            {
                GetList();
                Block();
            }
            else
            {
                Office();
                adapter = new SqlDataAdapter("SELECT [FirstName], [LastName], DATEDIFF(year, Birthdate, GetDate()) as Data, Roles.Title, Email, Offices.Title " +
                   "From Users JOIN Roles ON Users.RoleID = Roles.ID " +
                   "JOIN Offices ON Users.OfficeID = Offices.ID WHERE Offices.ID = '" + l + "'", connect);
                data = new DataSet();
                connect.Open();
                adapter.Fill(data, "Users");
                database.DataSource = data.Tables["Users"];
                connect.Close();
                database.Columns[0].HeaderText = "Name";
                database.Columns[2].HeaderText = "Age";
                database.Columns[2].Width = 50;
                database.Columns[4].Width = 150;
                database.Columns[3].HeaderText = "User Role";
                database.Columns[5].HeaderText = "Office";
                Vlock();
            }
        }

        private void addUser_Click(object sender, EventArgs e)
        {
            AddUsers AddUsers = new AddUsers();
            AddUsers.Show();
            this.Close();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Login Login = new Login();
            Login.Show();
            this.Close();
        }
    }
}
