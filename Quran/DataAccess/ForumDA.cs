using System.Collections.Generic;
using Quran.Models;

namespace Quran.DataAccess
{
    public class ForumDA
    {
        public IDictionary<string, object> SaveQuestion(AskQuestionDO qustion)
        {
            return Db.QueryProcSingle("SaveQuestion", new
            {
                UserName = qustion.UserName,
                Email = qustion.Email,
                Country = qustion.Country,
                Subject = qustion.Subject,
                Explanation = qustion.Explanation
            });
        }

        public List<IDictionary<string, object>> GetAllQuestions()
        {
            return Db.QueryProc("GetAllQuestions");
        }

        public List<IDictionary<string, object>> GetAllPublishedQuestions()
        {
            return Db.QueryProc("GetAllPublishQuestions");
        }

        public List<IDictionary<string, object>> GetAllUnPublishedQuestions()
        {
            return Db.QueryProc("GetAllUnPublishQuestions");
        }

        public int DeleteQuestions(int QuestionID)
        {
            return Db.ExecuteProc("DeleteQuestions", new { QuestionID });
        }

        public int PublishQuestionByAdmin(int QuestionID)
        {
            return Db.Execute("UPDATE dbo.Forum SET IsPublish = 1 WHERE QuestionID = @QuestionID;", new { QuestionID });
        }

        public IDictionary<string, object> GetSingleQuestion(int QuestionID)
        {
            return Db.QueryProcSingle("GetSingleQuestion", new { QuestionID });
        }
    }
}
