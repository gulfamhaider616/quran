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
    public class RegistrationDA
    {
        public DataSet VarifyEmail(string email)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("VarifyEmail");
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", email,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetStudentScheduleByID(string studentID)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetStudentScheduleByID");
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
        public DataSet SaveRegistration(RegistrationDO registration)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveRegistration");
                command.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = registration.StudentName;
                command.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = registration.FatherName;
                command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = registration.PhoneNumber;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = registration.Email;
                command.Parameters.Add("@SkypeID", SqlDbType.VarChar).Value = registration.SkypeID;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = registration.Gender;
                command.Parameters.Add("@DateOfBirth", SqlDbType.VarChar).Value = registration.DateOfBirth;
                command.Parameters.Add("@Country", SqlDbType.VarChar).Value = registration.Country;
                command.Parameters.Add("@City", SqlDbType.VarChar).Value = registration.City;
                command.Parameters.Add("@Classes", SqlDbType.VarChar).Value = registration.Classes;
                command.Parameters.Add("@DaysName", SqlDbType.VarChar).Value = registration.Days;
                command.Parameters.Add("@FeasibleTime", SqlDbType.VarChar).Value = registration.FeasibleTime;
                command.Parameters.Add("@FirstLanguage", SqlDbType.VarChar).Value = registration.FirstLanguage;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", registration,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetForgetStudentIDByEmail(string email)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetForgetStudentIDByEmail");
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", email,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveContactUs");
                command.Parameters.Add("@ContactTopic", SqlDbType.VarChar).Value = contacttopic;
                command.Parameters.Add("@ContactEmail", SqlDbType.VarChar).Value = contactemail;
                command.Parameters.Add("@CotactMessage", SqlDbType.VarChar).Value = contactmessage;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", contacttopic,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet SaveFeedback(string name, string country, string message)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveFeedback");
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                command.Parameters.Add("@Message", SqlDbType.VarChar).Value = message;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", name,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetFeedback()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetFeedback");
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

        public DataSet SaveUpdateRecord(RegistrationDO registration)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveUpdatedRecord");
                command.Parameters.Add("@StudentID", SqlDbType.VarChar).Value = registration.StudentID;
                command.Parameters.Add("@StudentName", SqlDbType.VarChar).Value = registration.StudentName;
                command.Parameters.Add("@FatherName", SqlDbType.VarChar).Value = registration.FatherName;
                command.Parameters.Add("@PhoneNumber", SqlDbType.VarChar).Value = registration.PhoneNumber;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = registration.Email;
                command.Parameters.Add("@SkypeID", SqlDbType.VarChar).Value = registration.SkypeID;
                command.Parameters.Add("@Gender", SqlDbType.VarChar).Value = registration.Gender;
                command.Parameters.Add("@DateOfBirth", SqlDbType.VarChar).Value = registration.DateOfBirth;
                command.Parameters.Add("@Country", SqlDbType.VarChar).Value = registration.Country;
                command.Parameters.Add("@City", SqlDbType.VarChar).Value = registration.City;
                command.Parameters.Add("@Classes", SqlDbType.VarChar).Value = registration.Classes;
                command.Parameters.Add("@DaysName", SqlDbType.VarChar).Value = registration.Days;
                command.Parameters.Add("@FeasibleTime", SqlDbType.VarChar).Value = registration.FeasibleTime;
                command.Parameters.Add("@FirstLanguage", SqlDbType.VarChar).Value = registration.FirstLanguage;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", registration,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public DataSet GetAllVideoLesson()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllVideoLessons");
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

        public DataSet GetVideoLessonByID(int LessonID)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetVideoLessonByID");
                command.Parameters.Add("@LessonID", SqlDbType.Int).Value = LessonID;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", LessonID,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        public int SaveVideoLesson(string lessonName, string link)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand(
                    "INSERT INTO dbo.VideoLesson (LessonName, Link) VALUES (@LessonName, @Link); SELECT CAST(SCOPE_IDENTITY() AS int);",
                    connection);
                command.Parameters.Add("@LessonName", SqlDbType.NVarChar).Value = (object)lessonName ?? DBNull.Value;
                command.Parameters.Add("@Link", SqlDbType.NVarChar).Value = (object)link ?? DBNull.Value;
                connection.Open();
                object result = command.ExecuteScalar();
                connection.Close();
                return result == null || result == DBNull.Value ? 0 : Convert.ToInt32(result);
            }
            catch (Exception ex)
            {
                throw new Exception("Error saving video lesson.", ex);
            }
        }

        public int DeleteVideoLesson(int lessonId)
        {
            try
            {
                SqlConnection connection = new SqlConnection(DbConfig.ConnectionString);
                SqlCommand command = new SqlCommand("DELETE FROM dbo.VideoLesson WHERE Id = @Id;", connection);
                command.Parameters.Add("@Id", SqlDbType.Int).Value = lessonId;
                connection.Open();
                int rows = command.ExecuteNonQuery();
                connection.Close();
                return rows;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting video lesson.", ex);
            }
        }
    }
}

