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
    public class RegistrationDA
    {
        public DataSet VarifyEmail(string email)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.VarifyEmail");
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetStudentScheduleByID");
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
        public DataSet SaveRegistration(RegistrationDO registration)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.SaveRegistration");
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
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetForgetStudentIDByEmail");
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = email;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", email,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        /// <summary>
        /// Save Contact Us Message
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public DataSet SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.SaveContactUs");
                command.Parameters.Add("@ContactTopic", SqlDbType.VarChar).Value = contacttopic;
                command.Parameters.Add("@ContactEmail", SqlDbType.VarChar).Value = contactemail;
                command.Parameters.Add("@CotactMessage", SqlDbType.VarChar).Value = contactmessage;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.SaveFeedback");
                command.Parameters.Add("@Name", SqlDbType.VarChar).Value = name;
                command.Parameters.Add("@Country", SqlDbType.VarChar).Value = country;
                command.Parameters.Add("@Message", SqlDbType.VarChar).Value = message;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetFeedback");
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

        public DataSet SaveUpdateRecord(RegistrationDO registration)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.SaveUpdatedRecord");
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
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", registration,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }

        /// <summary>
        /// Get All Video Lessons
        /// </summary>
        /// <returns></returns>
        public DataSet GetAllVideoLesson()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetAllVideoLessons");
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
                SqlCommand command = new DBConnection().DbSqlConnection("ahmedshair.GetVideoLessonByID");
                command.Parameters.Add("@LessonID", SqlDbType.Int).Value = LessonID;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", LessonID,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }
    }
}
