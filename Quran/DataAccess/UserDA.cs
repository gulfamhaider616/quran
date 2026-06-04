using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Quran.Models;

namespace Quran.DataAccess
{
    public class UserDA
    {
        public DataSet SaveUser(UserDO user)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveUser");
                command.Parameters.AddWithValue("@UName", user.Name);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Pass", user.Password);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", user,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }
        public DataSet VerifyUser(string email, string password)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("VerifyUser");
                command.Parameters.AddWithValue("@Email", email);
                command.Parameters.AddWithValue("@Pass", password);
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
        /// Get All Questions
        /// </summary>
        /// <returns></returns>
        public DataSet GetUserProfile(int Id)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetUserProfile");
                command.Parameters.AddWithValue("@ID", Id);
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
        public DataSet GetAllUsers()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllUsers");
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
        public DataSet Addookmark(UserDO user)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("AddToBookmark");
                command.Parameters.AddWithValue("@id", user.BookmarkId);
                command.Parameters.AddWithValue("@Email", user.Email);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                //logger.Debug("Error executing " + objValue.CommandText + ": " + ex.ToString());
                throw new Exception(string.Concat("Error executing the ", user,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }
        public DataSet GetBookMark(string email)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GETBOOKMARK");
                command.Parameters.AddWithValue("@Email", email);
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
    }
}

