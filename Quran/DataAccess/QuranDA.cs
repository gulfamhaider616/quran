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

        public DataSet GetFeaturedVerse(int position)
        {
            DataSet set = new DataSet();
            try
            {
                const string sql = @"
SELECT q.ChapterID, q.VerseID, q.AyahText, q.EnglishTranslation, q.UrduTranslation,
       s.SuraName, s.EnglishName, t.Total
FROM (
        SELECT ChapterID, VerseID, AyahText, EnglishTranslation, UrduTranslation,
               ROW_NUMBER() OVER (ORDER BY ChapterID, VerseID) AS rn
        FROM dbo.Quran
     ) q
CROSS JOIN (SELECT COUNT(*) AS Total FROM dbo.Quran) t
LEFT JOIN dbo.SuraNames s ON q.ChapterID = s.ChapterID
WHERE q.rn = @position";

                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand(sql, connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@position", SqlDbType.Int).Value = position;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetFeaturedVerse.", ex);
            }
            return set;
        }
    }
}

