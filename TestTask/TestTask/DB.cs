using MySqlConnector;
using System.Windows;
using System.Data.SqlClient;

namespace TestTask
{
    public class DB
    {
        SqlConnection sql = null;
        public DB(string name) 
        {
            sql = new SqlConnection(@$"Data Source=PC-BOLOFFICIAL\SQLEXPRESS;Initial Catalog={name};Integrated Security=True");
        }
        //public void OpenConnection()
        //{
        //    if (sql.State == System.Data.ConnectionState.Closed)
        //    {
        //        sql.Open();
        //    }
        //}

        //public void CloseConnection()
        //{
        //    if (sql.State == System.Data.ConnectionState.Open)
        //    {
        //        sql.Close();
        //    }
        //}

        public SqlConnection GetConnection() 
        {
            return sql;
        }
    }
}
