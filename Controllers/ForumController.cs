using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace LearnFreeQuran.Web.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public async Task<IActionResult> ForumHomePage()
        {
            return View((await new ForumBA().GetAllPublishedQuestionAsync()).ToList());
        }

        public IActionResult AskQuestion()
        {
            return View();
        }

        public async Task<JsonResult> SaveQuestion(string uname, string qemail, string qcountry, string qsubject, string qexplanation)
        {
            AskQuestionDO question = new AskQuestionDO
            {
                UserName = uname,
                Email = qemail,
                Country = qcountry,
                Subject = qsubject,
                Explanation = qexplanation
            };

            return Json(await new ForumBA().SaveQuestionAsync(question));
        }

        public async Task<IActionResult> SingleQuestion(int QuestionID)
        {
            return View(await new ForumBA().GetSingleQuestionAsync(QuestionID));
        }
    }
}