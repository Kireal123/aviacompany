using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;


namespace aviacompany
{
    partial class DB
    {
        SqlConnection connection = new SqlConnection(@"Data Source=LAPTOP-0M1E3SMS\KCHEB;Initial Catalog=aviacompany;Integrated Security=True");

        public void openConnection()
        {
            if (connection.State == System.Data.ConnectionState.Closed)
                connection.Open();
        }

        public void closeConnection()
        {
            if (connection.State == System.Data.ConnectionState.Open)
                connection.Close();
        }

        public SqlConnection getConnection()
        {
            return connection;
        }
    }
}
