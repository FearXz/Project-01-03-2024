using System.Configuration;
using System.Data.SqlClient;

namespace Project_01_03_2024.Models
{
    public class Connection
    {
        public static SqlConnection GetConn()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["MyDb"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);
            return conn;
        }
    }
}