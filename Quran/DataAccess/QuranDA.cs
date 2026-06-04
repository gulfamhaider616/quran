using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.DataAccess
{
    public class QuranDA
    {
        public DataSet GetAllSuraNames()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllSuraNames");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", "",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetSuraByID(int chapterId)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetSuraByID");
                command.Parameters.Add("@ChapterID", SqlDbType.Int).Value = chapterId;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", "",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }
    }
}

