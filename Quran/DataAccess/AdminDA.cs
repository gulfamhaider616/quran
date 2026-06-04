using Quran.Models;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quran.DataAccess
{
    public class AdminDA
    {
        public DataSet VerifyAdmin(string adminemail, string adminpassword)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("VerifyAdmin");
                command.Parameters.Add("@AdminEmail", SqlDbType.VarChar).Value = adminemail;
                command.Parameters.Add("@AdminPassword", SqlDbType.VarChar).Value = adminpassword;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", adminemail,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetAllStudents()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllStudents");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetUnscheduledStudents()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetUnscheduledStudents");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

public DataSet GetScheduledStudents()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetScheduledStudents");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

public DataSet GetTodaySchedule()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetTodaySchedule");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet SaveSchedule(ScheduleDO schedule)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveSchedule");
                command.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = schedule.StudentID;
                command.Parameters.Add("@Classes", SqlDbType.Int).Value = schedule.Classes;
                command.Parameters.Add("@DaysName", SqlDbType.VarChar).Value = schedule.Days;
                command.Parameters.Add("@ClassTime", SqlDbType.VarChar).Value = schedule.ClassTime;
                command.Parameters.Add("@TutorName", SqlDbType.VarChar).Value = schedule.TutorName;
                command.Parameters.Add("@Discription", SqlDbType.VarChar).Value = schedule.Description;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet ChangeSchedule(ScheduleDO schedule)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ChangeSchedule");
                command.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = schedule.StudentID;
                command.Parameters.Add("@Classes", SqlDbType.Int).Value = schedule.Classes;
                command.Parameters.Add("@DaysName", SqlDbType.VarChar).Value = schedule.Days;
                command.Parameters.Add("@ClassTime", SqlDbType.VarChar).Value = schedule.ClassTime;
                command.Parameters.Add("@TutorName", SqlDbType.VarChar).Value = schedule.TutorName;
                command.Parameters.Add("@Discription", SqlDbType.VarChar).Value = schedule.Description;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

public DataSet GetAllContactUs()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllContactUs");
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
        public int DeleteFeedback(int feedbackID)
        {
            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.CommandText = "DeleteFeedback";
            objSqlCommand.CommandType = CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@FeedbackID", feedbackID);
            return new DBConnection().ExecuteNonQuery(objSqlCommand);
        }

        public DataSet StudentPreview(string studentID)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("StudentPreviewByID");
                command.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = studentID;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", studentID,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet AddBook(BookDO book)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("AddBook");
                command.Parameters.Add("@BookTilte", SqlDbType.VarChar).Value = (object)book.BookTilte ?? DBNull.Value;
                command.Parameters.Add("@AuthorName", SqlDbType.VarChar).Value = (object)book.AutherName ?? DBNull.Value;
                command.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = (object)book.ImagePath ?? DBNull.Value;
                command.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = (object)book.FilePath ?? DBNull.Value;
                command.Parameters.Add("@BookType", SqlDbType.VarChar).Value = (object)book.BookType ?? DBNull.Value;
                command.Parameters.Add("@Detail", SqlDbType.VarChar).Value = (object)book.Detail ?? DBNull.Value;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }
        public int GetBookByID(int BookID)
        {
            int result = 0;
            try
            {
                BookDO book = new BookDO();
                SqlCommand command = new DBConnection().DbSqlConnection("GetBookByID");
                command.Parameters.Add("@BookID", SqlDbType.Int).Value = book.BookID;
                command.Parameters.Add("@BookTilte", SqlDbType.VarChar).Value = (object)book.BookTilte ?? DBNull.Value;
                command.Parameters.Add("@AuthorName", SqlDbType.VarChar).Value = (object)book.AutherName ?? DBNull.Value;
                command.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = (object)book.ImagePath ?? DBNull.Value;
                command.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = (object)book.FilePath ?? DBNull.Value;
                command.Parameters.Add("@BookType", SqlDbType.VarChar).Value = (object)book.BookType ?? DBNull.Value;
                command.Parameters.Add("@Detail", SqlDbType.VarChar).Value = (object)book.Detail ?? DBNull.Value;
                result = new DBConnection().ExecuteNonQuery(command);
            }
            catch(Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                  " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return result;
        }
        public int ChangeBook(BookDO book)
        {
            int result = 0;
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ChangeBook");
                command.Parameters.Add("@BookID", SqlDbType.Int).Value = book.BookID;
                command.Parameters.Add("@BookTilte", SqlDbType.VarChar).Value = (object)book.BookTilte ?? DBNull.Value;
                command.Parameters.Add("@AuthorName", SqlDbType.VarChar).Value = (object)book.AutherName ?? DBNull.Value;
                command.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = (object)book.ImagePath ?? DBNull.Value;
                command.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = (object)book.FilePath ?? DBNull.Value;
                command.Parameters.Add("@BookType", SqlDbType.VarChar).Value = (object)book.BookType ?? DBNull.Value;
                command.Parameters.Add("@Detail", SqlDbType.VarChar).Value = (object)book.Detail ?? DBNull.Value;
                result =  new DBConnection().ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                  throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return result;
        }

public DataSet GetAllBooks()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllBooks");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public int DeleteBook(int BookID)
        {
            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.CommandText = "DeleteBook";
            objSqlCommand.CommandType = CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@BookID", BookID);
            return new DBConnection().ExecuteNonQuery(objSqlCommand);
        }

}
}

