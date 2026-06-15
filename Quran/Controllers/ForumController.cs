using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;
using Quran.Helpers;

namespace Quran.Controllers
{
    public class ForumController : Controller
    {
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

        public IActionResult SingleQuestion(string slug, int QuestionID)
        {
            ForumBA ba = new ForumBA();

            // Legacy /Forum/SingleQuestion?QuestionID=N — 301 to the slug URL.
            if (QuestionID > 0)
            {
                AskQuestionDO legacy = ba.GetSingleQuestion(QuestionID);
                if (legacy != null && !string.IsNullOrEmpty(legacy.Subject))
                {
                    return RedirectPermanent("/forum/" + SlugHelper.Make(legacy.Subject));
                }
                return RedirectToAction("ForumHomePage");
            }

            int id = ba.GetQuestionIdBySlug(slug, false);
            if (id == 0)
            {
                return RedirectToAction("ForumHomePage");
            }
            return View(ba.GetSingleQuestion(id));
        }
    }
}
