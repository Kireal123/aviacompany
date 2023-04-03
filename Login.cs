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
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        int i = 3;

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void LoginButton_Click(object sender, EventArgs e)
        {
            string loginClient = textBox1.Text;
            string passwordClient = textBox2.Text;
            int id = 0;
            DB db = new DB();
            DataTable table = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter();
            SqlCommand command = new SqlCommand("SELECT [Email],[Password] From Users WHERE [Email] = @lg AND [Password] = @pw", db.getConnection());
            command.Parameters.Add("@lg", SqlDbType.VarChar).Value = loginClient;
            command.Parameters.Add("@pw", SqlDbType.VarChar).Value = passwordClient;
            adapter.SelectCommand = command;
            adapter.Fill(table);
            db.closeConnection();
            if (table.Rows.Count > 0)
            {
                SqlCommand comm = new SqlCommand("SELECT [RoleID] FROM Users WHERE [Email] = @e AND [Password] = @p", db.getConnection());
                comm.Parameters.Add("@e", SqlDbType.VarChar).Value = loginClient;
                comm.Parameters.Add("@p", SqlDbType.VarChar).Value = passwordClient;
                db.openConnection();
                var reader = comm.ExecuteReader();
                while (reader.Read())
                {
                    id = reader.GetInt32(0);
                }
                if (id == 1)
                {
                    MessageBox.Show("Вход в систему успешен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AdminInterface AdminInterface = new AdminInterface();
                    AdminInterface.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Вход в систему успешен", "Готово", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    UserInterface UserInterface = new UserInterface(textBox1.Text);
                    UserInterface.Show();
                    this.Hide();
                }
            }
            else
            {
                if (i != 0)
                {
                    i -= 1;
                    MessageBox.Show("При входе возникла ошибка. У вас осталось: " + i + "", "Ошибка входа", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    Timer timer = new Timer();
                    timer.Show();
                    i = 3;
                }
            }
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
