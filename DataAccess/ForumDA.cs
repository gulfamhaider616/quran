using Dapper;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.DataAccess
{
    public class ForumDA
    {
        private readonly DBConnection _db = new DBConnection();

        public async Task<DataSet> SaveQuestionAsync(AskQuestionDO qustion)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@UserName", qustion.UserName, DbType.String);
                parameters.Add("@Email", qustion.Email, DbType.String);
                parameters.Add("@Country", qustion.Country, DbType.String);
                parameters.Add("@Subject", qustion.Subject, DbType.String);
                parameters.Add("@Explanation", qustion.Explanation, DbType.String);

                return await _db.ExecuteDataSetAsync("SaveQuestion", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing the {qustion} in SaveQuestionAsync method.", ex);
            }
        }

        public async Task<DataSet> GetAllQuestionsAsync()
        {
            return await _db.ExecuteDataSetAsync("GetAllQuestions");
        }

        public async Task<DataSet> GetAllPublishedQuestionsAsync()
        {
            return await _db.ExecuteDataSetAsync("GetAllPublishQuestions");
        }

        public async Task<DataSet> GetAllUnPublishedQuestionsAsync()
        {
            return await _db.ExecuteDataSetAsync("GetAllUnPublishQuestions");
        }

        public async Task<int> DeleteQuestionsAsync(int questionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionID", questionID, DbType.Int32);

            return await _db.ExecuteNonQueryAsync("DeleteQuestions", parameters);
        }

        public async Task<int> PublishQuestionByAdminAsync(int questionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionID", questionID, DbType.Int32);

            return await _db.ExecuteNonQueryAsync("PublishQuestionByAdmin", parameters);
        }

        public async Task<DataSet> GetSingleQuestionAsync(int questionID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@QuestionID", questionID, DbType.Int32);

            return await _db.ExecuteDataSetAsync("GetSingleQuestion", parameters);
        }
    }
}