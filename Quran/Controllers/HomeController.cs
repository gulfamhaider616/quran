using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var email = HttpContext.Session.GetString("Email");
            if (email != null)
            {
                string result = new UserBA().GetBookMark(email);
                if (!string.IsNullOrEmpty(result))
                {
                    HttpContext.Session.SetString("Bookmarkid", "#" + result);
                    HttpContext.Session.SetString("Chapterid", result.Split('-')[0]);
                }
            }
            return View();
        }

        public IActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult UserTrust()
        {
            List<FeedbackDO> list = new RegistrationBA().GetFeedback();
            return View(list);
        }

        public IActionResult GetStudentScheduleByID()
        {
            return View();
        }

        public JsonResult VarifyEmail(string email)
        {
            bool result = new RegistrationBA().VarifyEmail(email);
            return Json(result);
        }

        public JsonResult SaveRegistration(string studentname, string fathername, string phonenumber, string email, string skypeid, string gender, string date, string month, string year, string country, string city, string classes, string days, string feasibletime, string language)
        {
            RegistrationDO registration = new RegistrationDO();
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
            if (int.Parse(classes) == 7)
            {
                registration.Days = "All";
            }
            else
            {
                registration.Days = days;
            }
            registration.FeasibleTime = feasibletime;
            registration.FirstLanguage = language;
            var result = new RegistrationBA().SaveRegistration(registration);
            if (result.StudentID != null)
            {
                SendStudentID_Email(email, studentname, result.StudentID);
                return Json(result);
            }
            else
            {
                return Json(false);
            }
        }

        public JsonResult GetStudentScheduleDataByID(string studentid)
        {
            var schedule = new RegistrationBA().GetStudentScheduleByID(studentid);
            return Json(schedule);
        }

        public IActionResult ForgetYourIDView()
        {
            return View();
        }

        public JsonResult GetForgetStudentIDBy(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var result = new RegistrationBA().GetForgetStudentIDByEmail(email);
                return Json(result);
            }
            else
            {
                return Json(false);
            }
        }

        public IActionResult TermsForStudentsAndParents()
        {
            return View();
        }

        public IActionResult TermsForInstructor()
        {
            return View();
        }

        public JsonResult SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            int result = new RegistrationBA().SaveContactUs(contacttopic, contactemail, contactmessage);
            return Json(result);
        }

        public IActionResult ReadKalmas()
        {
            return View();
        }

        public IActionResult ReadNamaz()
        {
            return View();
        }

        public IActionResult ReadDuain()
        {
            return View();
        }

        public IActionResult ReadDarood()
        {
            return View();
        }

        public IActionResult FeeStructure()
        {
            return View();
        }

        public JsonResult SaveFeedback(string name, string country, string message)
        {
            int result = new RegistrationBA().SaveFeedback(name, country, message);
            return Json(result);
        }

        // Send student id in email to User.
        [NonAction]
        public void SendStudentID_Email(string email, string name, string studentID)
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
                    Credentials = new NetworkCredential(your_id, your_password),
                    Timeout = 1000000,
                };
                MailMessage mm = new MailMessage(your_id, email, "Thanks for Registration to LFQ", "<h3> Dear " + name + ",</h3><h3> Thank you very much to join us. Your Student ID is " + studentID + ".</h3><p> Learn Free Quran is 100% free and our objective is just to teach you Islam by using this technological way.  Our tutor will schedule you very soon and you will get started. By using above mentioned Student ID, you can check your schedule with tutor from <a href='http://learnfreequran.com/student_schedule' target='_blank'>website</a>.</p>" + "<p>Please remember your Student ID, we have your all record on the basis of this ID.</p>");
                client.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception caught in CreateTestMessage2(): {0}", ex.ToString());
            }
        }

        public IActionResult Lesson_1_Noorani()
        {
            return View();
        }

        public IActionResult ReadNamazJanaza()
        {
            return View();
        }

        public IActionResult Due_e_Qunoot()
        {
            return View();
        }

        public IActionResult StudentDetails(string StudentID)
        {
            return View(new AdminBA().StudentPreview(StudentID));
        }

        #region Canidate Edit
        public IActionResult EditView()
        {
            return View();
        }

        public JsonResult GetEditInformation(string StudentID)
        {
            return Json(new AdminBA().StudentPreview(StudentID));
        }

        public JsonResult SaveUpdatedRecord(string studentid, string studentname, string fathername, string phonenumber, string email, string skypeid, string gender, string dateofbirth, string country, string city, string language, string classes, string days, string feasibletime)
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
                return Json(result);
            }
            else
            {
                return Json(false);
            }
        }
        #endregion

        public IActionResult QuraniLesson()
        {
            return View(new RegistrationBA().GetAllVideoLesson());
        }

        public IActionResult VideoLessonPartial(int LessonID)
        {
            LessonsContract contract = new LessonsContract();
            contract.list = new RegistrationBA().GetAllVideoLesson();
            contract.Lesson = new RegistrationBA().GetVideoLessonByID(LessonID);
            return View(contract);
        }

        public JsonResult SectionLesson(int LessonID)
        {
            return Json(new RegistrationBA().GetVideoLessonByID(LessonID));
        }
    }
}
