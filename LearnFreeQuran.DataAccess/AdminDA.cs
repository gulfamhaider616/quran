using LearnFreeQuran.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFreeQuran.DataAccess
{
    public class AdminDA
    {
        public DataSet VerifyAdmin(string adminemail, string adminpassword)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.VerifyAdmin");
                command.Parameters.Add("@AdminEmail", SqlDbType.VarChar).Value = adminemail;
                command.Parameters.Add("@AdminPassword", SqlDbType.VarChar).Value = adminpassword;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", adminemail,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        /// <summary>
        /// Get All Students saved in database
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllStudents()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetAllStudents");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        /// <summary>
        /// Get All UnScheduled Students
        /// </summary>
        /// <returns></returns>
        public DataSet GetUnscheduledStudents()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetUnscheduledStudents");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetScheduledStudents");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetTodaySchedule");
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        /// <summary>
        /// Set Student Schedule
        /// </summary>
        /// <param name="schedule"></param>
        /// <returns></returns>
        public DataSet SaveSchedule(ScheduleDO schedule)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.SaveSchedule");
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
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.ChangeSchedule");
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
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetAllContactUs");
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
        public int DeleteFeedback(int feedbackID)
        {
            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.CommandText = "learnfreequran.DeleteFeedback";
            objSqlCommand.CommandType = CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@FeedbackID", feedbackID);
            return new DBConnection().ExecuteNonQuery(objSqlCommand);
        }

        /// <summary>
        /// Student preview
        /// </summary>
        /// <param name="adminemail"></param>
        /// <param name="adminpassword"></param>
        /// <returns></returns>
        public DataSet StudentPreview(string studentID)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.StudentPreviewByID");
                command.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = studentID;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                command.Parameters.Add("@BookTilte", SqlDbType.VarChar).Value = book.BookTilte;
                command.Parameters.Add("@AuthorName", SqlDbType.VarChar).Value = book.AutherName;
                command.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = book.ImagePath;
                command.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = book.FilePath;
                command.Parameters.Add("@BookType", SqlDbType.VarChar).Value = book.BookType;
                command.Parameters.Add("@Detail", SqlDbType.VarChar).Value = book.Detail;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                command.Parameters.Add("@BookTilte", SqlDbType.VarChar).Value = book.BookTilte;
                command.Parameters.Add("@AuthorName", SqlDbType.VarChar).Value = book.AutherName;
                command.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = book.ImagePath;
                command.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = book.FilePath;
                command.Parameters.Add("@BookType", SqlDbType.VarChar).Value = book.BookType;
                command.Parameters.Add("@Detail", SqlDbType.VarChar).Value = book.Detail;
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
                command.Parameters.Add("@BookTilte", SqlDbType.VarChar).Value = book.BookTilte;
                command.Parameters.Add("@AuthorName", SqlDbType.VarChar).Value = book.AutherName;
                command.Parameters.Add("@ImagePath", SqlDbType.VarChar).Value = book.ImagePath;
                command.Parameters.Add("@FilePath", SqlDbType.VarChar).Value = book.FilePath;
                command.Parameters.Add("@BookType", SqlDbType.VarChar).Value = book.BookType;
                command.Parameters.Add("@Detail", SqlDbType.VarChar).Value = book.Detail;
                result =  new DBConnection().ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                //]\
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
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public int DeleteBook(int BookID)
        {
            SqlCommand objSqlCommand = new SqlCommand();
            objSqlCommand.CommandText = "ph16200308291.DeleteBook";
            objSqlCommand.CommandType = CommandType.StoredProcedure;
            objSqlCommand.Parameters.AddWithValue("@BookID", BookID);
            return new DBConnection().ExecuteNonQuery(objSqlCommand);
        }

     
    }
}
