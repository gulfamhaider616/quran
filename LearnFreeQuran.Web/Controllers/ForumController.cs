using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnFreeQuran.Web.Controllers
{
    public class ForumController : Controller
    {
        // GET: Forum
        public ActionResult ForumHomePage()
        {
            return View(new ForumBA().GetAllPublishedQuestion().ToList());
        }
        public ActionResult AskQuestion()
        {
            return View();
        }
        public JsonResult SaveQuestion(string uname,string qemail,string qcountry,string qsubject,string qexplanation)
        {
            AskQuestionDO question = new AskQuestionDO();
            question.UserName = uname;
            question.Email = qemail;
            question.Country = qcountry;
            question.Subject = qsubject;
            question.Explanation = qexplanation;
            return Json(new ForumBA().SaveQuestion(question), JsonRequestBehavior.AllowGet);
        }
        public ActionResult SingleQuestion(int QuestionID)
        {
            return View(new ForumBA().GetSingleQuestion(QuestionID));
        }
    }
}