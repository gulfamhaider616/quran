using System;
using System.Data;
using Microsoft.Data.SqlClient;

namespace Quran.DataAccess
{
    public class DBConnection
    {
        public SqlCommand DbSqlConnection(string query)
        {
            string conString = DbConfig.ConnectionString;
            SqlConnection connection = new SqlConnection(conString);
            SqlCommand command = new SqlCommand(query, connection);
            command.CommandType = CommandType.StoredProcedure;
            connection.Open();
            return command;
        }

        public int ExecuteNonQuery(SqlCommand objValue)
        {
            int res = 0;
            try
            {
                string conString = DbConfig.ConnectionString;
                SqlConnection connection = new SqlConnection(conString);
                objValue.Connection = connection;
                objValue.Connection.Open();
                res = objValue.ExecuteNonQuery();
                objValue.Connection.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", objValue.CommandText,
                    " in Execute.ExecuteNonQuery(SqlCommand) method."), ex);
            }
            return res;
        }
    }
}
