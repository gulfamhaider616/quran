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
    public class ForumDA
    {
        public DataSet SaveQuestion(AskQuestionDO qustion)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("SaveQuestion");
                command.Parameters.Add("@UserName", SqlDbType.VarChar).Value = qustion.UserName;
                command.Parameters.Add("@Email", SqlDbType.VarChar).Value = qustion.Email;
                command.Parameters.Add("@Country", SqlDbType.VarChar).Value = qustion.Country;
                command.Parameters.Add("@Subject", SqlDbType.VarChar).Value = qustion.Subject;
                command.Parameters.Add("@Explanation", SqlDbType.VarChar).Value = qustion.Explanation;
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                command.Connection.Close();
                adapter.Fill(set);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", qustion,
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
            return set;
        }
        public DataSet GetAllQuestions()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllQuestions");
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
        public DataSet GetAllPublishedQuestions()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllPublishQuestions");
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
        public DataSet GetAllUnPublishedQuestions()
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetAllUnPublishQuestions");
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
        public int DeleteQuestions(int QuestionID)
        {
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("DeleteQuestions");
                command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;
                return new DBConnection().ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", "",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
        }
        public int PublishQuestionByAdmin(int QuestionID)
        {
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("PublishQuestionByAdmin");
                command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;
                return new DBConnection().ExecuteNonQuery(command);
            }
            catch (Exception ex)
            {
                throw new Exception(string.Concat("Error executing the ", "",
                    " in Execute.ExecuteDataSet(SQLCommand) method."), ex);
            }
        }
        public DataSet GetSingleQuestion(int QuestionID)
        {
            DataSet set = new DataSet();
            try
            {
                SqlCommand command = new DBConnection().DbSqlConnection("GetSingleQuestion");
                command.Parameters.Add("@QuestionID", SqlDbType.Int).Value = QuestionID;
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

