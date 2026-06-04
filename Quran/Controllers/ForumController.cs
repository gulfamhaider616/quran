using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public IActionResult ForumHomePage()
        {
            return View(new ForumBA().GetAllPublishedQuestion().ToList());
        }

        public IActionResult AskQuestion()
        {
            return View();
        }

        public JsonResult SaveQuestion(string uname, string qemail, string qcountry, string qsubject, string qexplanation)
        {
            AskQuestionDO question = new AskQuestionDO();
            question.UserName = uname;
            question.Email = qemail;
            question.Country = qcountry;
            question.Subject = qsubject;
            question.Explanation = qexplanation;
            return Json(new ForumBA().SaveQuestion(question));
        }

        public IActionResult SingleQuestion(int QuestionID)
        {
            return View(new ForumBA().GetSingleQuestion(QuestionID));
        }
    }
}
