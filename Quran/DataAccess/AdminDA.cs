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

        public DataSet GetAllAdmins()
        {
            DataSet set = new DataSet();
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand("SELECT Id, AdminName, AdminEmail, AdminPassword FROM dbo.AdminUser ORDER BY Id;", connection);
                command.CommandType = CommandType.Text;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllAdmins.", ex);
            }
            return set;
        }

        public DataSet GetAdminById(int id)
        {
            DataSet set = new DataSet();
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand("SELECT Id, AdminName, AdminEmail, AdminPassword FROM dbo.AdminUser WHERE Id = @Id;", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAdminById.", ex);
            }
            return set;
        }

        public int AdminEmailExists(string email, int excludeId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand("SELECT COUNT(*) FROM dbo.AdminUser WHERE AdminEmail = @AdminEmail AND Id <> @Id;", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@AdminEmail", SqlDbType.NVarChar).Value = (object)email ?? DBNull.Value;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = excludeId;
                connection.Open();
                int count = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
                return count;
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing AdminEmailExists.", ex);
            }
        }

        public int SaveAdmin(AdminUserDO admin)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand(
                    "INSERT INTO dbo.AdminUser (AdminName, AdminEmail, AdminPassword) VALUES (@AdminName, @AdminEmail, @AdminPassword); SELECT CAST(SCOPE_IDENTITY() AS int);",
                    connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@AdminName", SqlDbType.NVarChar).Value = (object)admin.AdminName ?? DBNull.Value;
                command.Parameters.Add("@AdminEmail", SqlDbType.NVarChar).Value = (object)admin.AdminEmail ?? DBNull.Value;
                command.Parameters.Add("@AdminPassword", SqlDbType.NVarChar).Value = (object)admin.AdminPassword ?? DBNull.Value;
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving admin.", ex);
            }
        }

        public int UpdateAdmin(AdminUserDO admin)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand(
                    "UPDATE dbo.AdminUser SET AdminName = @AdminName, AdminEmail = @AdminEmail, AdminPassword = @AdminPassword WHERE Id = @Id;",
                    connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = admin.Id;
                command.Parameters.Add("@AdminName", SqlDbType.NVarChar).Value = (object)admin.AdminName ?? DBNull.Value;
                command.Parameters.Add("@AdminEmail", SqlDbType.NVarChar).Value = (object)admin.AdminEmail ?? DBNull.Value;
                command.Parameters.Add("@AdminPassword", SqlDbType.NVarChar).Value = (object)admin.AdminPassword ?? DBNull.Value;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating admin.", ex);
            }
        }

        public int DeleteAdmin(int id)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand("DELETE FROM dbo.AdminUser WHERE Id = @Id;", connection);
                command.CommandType = CommandType.Text;
                command.Parameters.Add("@Id", SqlDbType.Int).Value = id;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting admin.", ex);
            }
        }

}
}

