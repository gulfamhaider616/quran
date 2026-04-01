using LearnFreeQuran.DataAccess;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.Business
{
    public class ForumBA
    {
        public async Task<bool> SaveQuestionAsync(AskQuestionDO question)
        {
            DataSet dataset = await new ForumDA().SaveQuestionAsync(question);
            string questionID = "";

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                questionID = dataset.Tables[0].Rows[0]["QuestionID"].ToString();
            }

            return !string.IsNullOrEmpty(questionID);
        }

        public async Task<List<AskQuestionDO>> GetAllQuestionsAsync()
        {
            List<AskQuestionDO> list = new List<AskQuestionDO>();
            DataSet dataset = await new ForumDA().GetAllQuestionsAsync();

            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    list.Add(new AskQuestionDO
                    {
                        AskQuestionID = dr.Field<int>("QuestionID"),
                        UserName = dr.Field<string>("UserName"),
                        Email = dr.Field<string>("Email"),
                        Country = dr.Field<string>("Country"),
                        Subject = dr.Field<string>("Subject"),
                        Explanation = dr.Field<string>("Explanation"),
                        Publish = dr.Field<int>("IsPublish")
                    });
                }
            }

            return list;
        }

        public async Task<List<AskQuestionDO>> GetAllPublishedQuestionAsync()
        {
            List<AskQuestionDO> list = new List<AskQuestionDO>();
            DataSet dataset = await new ForumDA().GetAllPublishedQuestionsAsync();

            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    list.Add(new AskQuestionDO
                    {
                        AskQuestionID = dr.Field<int>("QuestionID"),
                        UserName = dr.Field<string>("UserName"),
                        Email = dr.Field<string>("Email"),
                        Country = dr.Field<string>("Country"),
                        Subject = dr.Field<string>("Subject"),
                        Explanation = dr.Field<string>("Explanation"),
                        Publish = dr.Field<int>("IsPublish")
                    });
                }
            }

            return list;
        }

        public async Task<List<AskQuestionDO>> GetAllUnPublishedQuestionAsync()
        {
            List<AskQuestionDO> list = new List<AskQuestionDO>();
            DataSet dataset = await new ForumDA().GetAllUnPublishedQuestionsAsync();

            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    list.Add(new AskQuestionDO
                    {
                        AskQuestionID = dr.Field<int>("QuestionID"),
                        UserName = dr.Field<string>("UserName"),
                        Email = dr.Field<string>("Email"),
                        Country = dr.Field<string>("Country"),
                        Subject = dr.Field<string>("Subject"),
                        Explanation = dr.Field<string>("Explanation"),
                        Publish = dr.Field<int>("IsPublish")
                    });
                }
            }

            return list;
        }

        public async Task<AskQuestionDO> GetSingleQuestionAsync(int QuestionID)
        {
            AskQuestionDO question = new AskQuestionDO();
            DataSet dataset = await new ForumDA().GetSingleQuestionAsync(QuestionID);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataset.Tables[0].Rows[0];

                question.AskQuestionID = dr.Field<int>("QuestionID");
                question.UserName = dr.Field<string>("UserName");
                question.Email = dr.Field<string>("Email");
                question.Country = dr.Field<string>("Country");
                question.Subject = dr.Field<string>("Subject");
                question.Explanation = dr.Field<string>("Explanation");
                question.Publish = dr.Field<int>("IsPublish");
            }

            return question;
        }

        public async Task<bool> PublishQuestionByAdminAsync(int QuestionID)
        {
            int result = await new ForumDA().PublishQuestionByAdminAsync(QuestionID);
            return result > 0;
        }

        public async Task<bool> DeleteQuestionsAsync(int QuestionID)
        {
            int result = await new ForumDA().DeleteQuestionsAsync(QuestionID);
            return result > 0;
        }
    }
}