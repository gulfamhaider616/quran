using Quran.DataAccess;
using Quran.Models;
using System.Collections.Generic;

namespace Quran.Business
{
    public class ForumBA
    {
        public bool SaveQuestion(AskQuestionDO question)
        {
            IDictionary<string, object> r = new ForumDA().SaveQuestion(question);
            string questionID = r == null ? "" : r.Str("QuestionID");
            return questionID != "";
        }

        public List<AskQuestionDO> GetAllQuestions()
        {
            return Map(new ForumDA().GetAllQuestions());
        }

        public List<AskQuestionDO> GetAllPublishedQuestion()
        {
            return Map(new ForumDA().GetAllPublishedQuestions());
        }

        public List<AskQuestionDO> GetAllUnPublishedQuestion()
        {
            return Map(new ForumDA().GetAllUnPublishedQuestions());
        }

        public AskQuestionDO GetSingleQuestion(int QuestionID)
        {
            AskQuestionDO question = new AskQuestionDO();
            IDictionary<string, object> dr = new ForumDA().GetSingleQuestion(QuestionID);
            if (dr != null)
            {
                MapRow(dr, question);
            }
            return question;
        }

        public bool PublishQuestionByAdmin(int QuestionID)
        {
            return new ForumDA().PublishQuestionByAdmin(QuestionID) > 0;
        }

        public bool UnPublishQuestionByAdmin(int QuestionID)
        {
            return new ForumDA().UnPublishQuestionByAdmin(QuestionID) > 0;
        }

        public bool DeleteQuestions(int QuestionID)
        {
            return new ForumDA().DeleteQuestions(QuestionID) > 0;
        }

        private List<AskQuestionDO> Map(List<IDictionary<string, object>> rows)
        {
            List<AskQuestionDO> list = new List<AskQuestionDO>();
            foreach (IDictionary<string, object> dr in rows)
            {
                AskQuestionDO question = new AskQuestionDO();
                MapRow(dr, question);
                list.Add(question);
            }
            return list;
        }

        private void MapRow(IDictionary<string, object> dr, AskQuestionDO question)
        {
            question.AskQuestionID = dr.Get<int>("QuestionID");
            question.UserName = dr.Get<string>("UserName");
            question.Email = dr.Get<string>("Email");
            question.Country = dr.Get<string>("Country");
            question.Subject = dr.Get<string>("Subject");
            question.Explanation = dr.Get<string>("Explanation");
            question.Publish = dr.Get<int>("IsPublish");
        }
    }
}
