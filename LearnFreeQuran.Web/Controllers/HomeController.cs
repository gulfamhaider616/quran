using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
namespace LearnFreeQuran.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            if (Session["Email"] != null)
            {
                string email = Session["Email"].ToString();
                string result = new UserBA().GetBookMark(email);
                if (!string.IsNullOrEmpty(result))
                {
                    Session["Bookmarkid"] = "#" + result;
                    Session["Chapterid"] = result.Split('-')[0];
                }
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Registration()
        {
            return View();
        }

        public ActionResult UserTrust()
        {
            List<FeedbackDO> list= new Business.RegistrationBA().GetFeedback();
            return View(list);
        }

        public ActionResult GetStudentScheduleByID()
        {
            return View();
        }

        public JsonResult VarifyEmail(string email)
        {
            bool result= new Business.RegistrationBA().VarifyEmail(email);
            if (result)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveRegistration(string studentname,string fathername,string phonenumber,string email, string skypeid,string gender,string date,string month,string year,string country, string city, string classes,string days, string feasibletime,string language)
        {
            Library.RegistrationDO registration = new Library.RegistrationDO();
            string DateOfBirth = date + "/" + month + "/" + year;
            registration.StudentName = studentname;
            registration.FatherName = fathername;
            registration.PhoneNumber = phonenumber;
            registration.Email = email;
            registration.SkypeID = skypeid;
            registration.Gender = gender;
            registration.DateOfBirth = DateOfBirth;
            registration.Country = country;
            registration.City = city;
            registration.Classes = int.Parse(classes);
            if(int.Parse(classes)==7)
            {
                registration.Days = "All";
            }
            else
            {
                registration.Days = days;
            }
            registration.FeasibleTime = feasibletime;
            registration.FirstLanguage = language;
            var result =new Business.RegistrationBA().SaveRegistration(registration);
            if(result.StudentID != null)
            {
                SendStudentID_Email(email, studentname, result.StudentID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult GetStudentScheduleDataByID(string studentid)
        {
            var schedule = new Business.RegistrationBA().GetStudentScheduleByID(studentid);
            return Json(schedule, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ForgetYourIDView()
        {
            return View();
        }

        public JsonResult GetForgetStudentIDBy(string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                var result = new Business.RegistrationBA().GetForgetStudentIDByEmail(email);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult TermsForStudentsAndParents()
        {
            return View();
        }

        public ActionResult TermsForInstructor()
        {
            return View();
        }

        public JsonResult SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            int result = new RegistrationBA().SaveContactUs(contacttopic, contactemail, contactmessage);
            return Json(result,JsonRequestBehavior.AllowGet);
        }

        public ActionResult ReadKalmas()
        {
            return View();
        }
        public ActionResult ReadNamaz()
        {
            return View();
        }

        public ActionResult ReadDuain()
        {
            return View();
        }
        public ActionResult ReadDarood()
        {
            return View();
        }
        public ActionResult FeeStructure()
        {
            return View();
        }

        public JsonResult SaveFeedback(string name, string country,string message)
        {
            int result = new Business.RegistrationBA().SaveFeedback(name,country,message);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        //Send student id in email to User.
        public void SendStudentID_Email(string email,string name, string studentID)
        {
            string your_id = "learnfreequran@gmail.com";
            string your_password = "learnfreequran123";
            try
            {
                SmtpClient client = new SmtpClient
                {
                    Host = "smtp.gmail.com",
                    Port = 587,
                    EnableSsl = true,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new System.Net.NetworkCredential(your_id, your_password),
                    Timeout = 1000000,
                };
                MailMessage mm = new MailMessage(your_id, email, "Thanks for Registration to LFQ", "<h3> Dear " + name + ",</h3><h3> Thank you very much to join us. Your Student ID is " + studentID + ".</h3><p> Learn Free Quran is 100% free and our objective is just to teach you Islam by using this technological way.  Our tutor will schedule you very soon and you will get started. By using above mentioned Student ID, you can check your schedule with tutor from <a href='http://learnfreequran.com/student_schedule' target='_blank'>website</a>.</p>" + "<p>Please remember your Student ID, we have your all record on the basis of this ID.</p>");
                client.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}",
                            ex.ToString());
            }
        }
        public ActionResult Lesson_1_Noorani()
        {
            return View();
        }

        public ActionResult ReadNamazJanaza()
        {
            return View();
        }
        public ActionResult Due_e_Qunoot()
        {
            return View();
        }

        public ActionResult StudentDetails(string StudentID)
        {
            return View(new AdminBA().StudentPreview(StudentID));
        }

        #region Canidate Edit 
        public ActionResult EditView()
        {
            return View();
        }

        public JsonResult GetEditInformation(string StudentID)
        {
            return Json(new AdminBA().StudentPreview(StudentID), JsonRequestBehavior.AllowGet);
        }
        public JsonResult SaveUpdatedRecord(string studentid, string studentname, string fathername, string phonenumber, string email, string skypeid, string gender, string dateofbirth, string country, string city,string language, string classes, string days, string feasibletime)
        {
            RegistrationDO registration = new RegistrationDO();
            registration.StudentID = studentid;
            registration.StudentName = studentname;
            registration.FatherName = fathername;
            registration.PhoneNumber = phonenumber;
            registration.Email = email;
            registration.SkypeID = skypeid;
            registration.Gender = gender;
            registration.DateOfBirth = dateofbirth;
            registration.Country = country;
            registration.City = city;
            registration.Classes = int.Parse(classes);
            registration.Days = days;
            registration.FeasibleTime = feasibletime;
            registration.FirstLanguage = language;
            var result = new RegistrationBA().SaveUpdatedRecord(registration);
            if (result.StudentID != null)
            {
                SendStudentID_Email(email, studentname, result.StudentID);
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        public ActionResult QuraniLesson()
        {
            return View(new RegistrationBA().GetAllVideoLesson());
        }
        public ActionResult VideoLessonPartial(int LessonID)
        {
            LessonsContract contract = new LessonsContract();
            contract.list = new RegistrationBA().GetAllVideoLesson();
            contract.Lesson = new RegistrationBA().GetVideoLessonByID(LessonID);
            return View(contract);
        }

        public JsonResult SectionLesson(int LessonID)
        {
            //LessonsContract contract = new LessonsContract();
            //contract.list = new RegistrationBA().GetAllVideoLesson();
            //contract.Lesson = new RegistrationBA().GetVideoLessonByID(LessonID);
            return Json(new RegistrationBA().GetVideoLessonByID(LessonID), JsonRequestBehavior.AllowGet);
        }
    }
}