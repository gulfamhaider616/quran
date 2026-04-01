using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Net.Mail;

namespace LearnFreeQuran.Web.Controllers
{
    public class HomeController : Controller
    {
        public async Task<IActionResult> Index()
        {
            if (HttpContext.Session.GetString("Email") != null)
            {
                string email = HttpContext.Session.GetString("Email");
                string result = await new UserBA().GetBookMarkAsync(email);

                if (!string.IsNullOrEmpty(result))
                {
                    HttpContext.Session.SetString("Bookmarkid", "#" + result);
                    HttpContext.Session.SetString("Chapterid", result.Split('-')[0]);
                }
            }
            return View();
        }

        public IActionResult About() => View();

        public IActionResult Contact() => View();

        public IActionResult Registration() => View();

        public async Task<IActionResult> UserTrust()
        {
            List<FeedbackDO> list = await new RegistrationBA().GetFeedbackAsync();
            return View(list);
        }

        public IActionResult GetStudentScheduleByID() => View();

        public async Task<JsonResult> VarifyEmail(string email)
        {
            bool result = await new RegistrationBA().VarifyEmailAsync(email);
            return Json(result);
        }

        public async Task<JsonResult> SaveRegistration(string studentname, string fathername, string phonenumber, string email, string skypeid, string gender, string date, string month, string year, string country, string city, string classes, string days, string feasibletime, string language)
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

            registration.Days = int.Parse(classes) == 7 ? "All" : days;
            registration.FeasibleTime = feasibletime;
            registration.FirstLanguage = language;

            var result = await new RegistrationBA().SaveRegistrationAsync(registration);

            if (result.StudentID != null)
            {
                SendStudentID_Email(email, studentname, result.StudentID);
                return Json(result);
            }

            return Json(false);
        }

        public async Task<JsonResult> GetStudentScheduleDataByID(string studentid)
        {
            var schedule = await new RegistrationBA().GetStudentScheduleByIDAsync(studentid);
            return Json(schedule);
        }

        public IActionResult ForgetYourIDView() => View();

        public async Task<JsonResult> GetForgetStudentIDBy(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                var result = await new RegistrationBA().GetForgetStudentIDByEmailAsync(email);
                return Json(result);
            }

            return Json(false);
        }

        public IActionResult TermsForStudentsAndParents() => View();

        public IActionResult TermsForInstructor() => View();

        public async Task<JsonResult> SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            int result = await new RegistrationBA().SaveContactUsAsync(contacttopic, contactemail, contactmessage);
            return Json(result);
        }

        public IActionResult ReadKalmas() => View();
        public IActionResult ReadNamaz() => View();
        public IActionResult ReadDuain() => View();
        public IActionResult ReadDarood() => View();
        public IActionResult FeeStructure() => View();

        public async Task<JsonResult> SaveFeedback(string name, string country, string message)
        {
            int result = await new RegistrationBA().SaveFeedbackAsync(name, country, message);
            return Json(result);
        }

        // Email logic unchanged
        public void SendStudentID_Email(string email, string name, string studentID)
        {
            string your_id = "learnfreequran@gmail.com";
            string your_password = "learnfreequran123";

            try
            {
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587)
                {
                    EnableSsl = true,
                    Credentials = new NetworkCredential(your_id, your_password)
                };

                MailMessage mm = new MailMessage(
                    your_id,
                    email,
                    "Thanks for Registration to LFQ",
                    $"<h3> Dear {name},</h3><h3> Thank you very much to join us. Your Student ID is {studentID}.</h3>"
                );

                mm.IsBodyHtml = true;
                client.Send(mm);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public IActionResult Lesson_1_Noorani() => View();
        public IActionResult ReadNamazJanaza() => View();
        public IActionResult Due_e_Qunoot() => View();

        public async Task<IActionResult> StudentDetails(string StudentID)
        {
            return View(await new AdminBA().StudentPreviewAsync(StudentID));
        }

        #region Candidate Edit

        public IActionResult EditView() => View();

        public async Task<JsonResult> GetEditInformation(string StudentID)
        {
            return Json(await new AdminBA().StudentPreviewAsync(StudentID));
        }

        public async Task<JsonResult> SaveUpdatedRecord(string studentid, string studentname, string fathername, string phonenumber, string email, string skypeid, string gender, string dateofbirth, string country, string city, string language, string classes, string days, string feasibletime)
        {
            RegistrationDO registration = new RegistrationDO
            {
                StudentID = studentid,
                StudentName = studentname,
                FatherName = fathername,
                PhoneNumber = phonenumber,
                Email = email,
                SkypeID = skypeid,
                Gender = gender,
                DateOfBirth = dateofbirth,
                Country = country,
                City = city,
                Classes = int.Parse(classes),
                Days = days,
                FeasibleTime = feasibletime,
                FirstLanguage = language
            };

            var result = await new RegistrationBA().SaveUpdatedRecordAsync(registration);

            if (result.StudentID != null)
            {
                SendStudentID_Email(email, studentname, result.StudentID);
                return Json(result);
            }

            return Json(false);
        }

        #endregion

        public async Task<IActionResult> QuraniLesson()
        {
            return View(await new RegistrationBA().GetAllVideoLessonAsync());
        }

        public async Task<IActionResult> VideoLessonPartial(int LessonID)
        {
            LessonsContract contract = new LessonsContract
            {
                list = await new RegistrationBA().GetAllVideoLessonAsync(),
                Lesson = await new RegistrationBA().GetVideoLessonByIDAsync(LessonID)
            };

            return View(contract);
        }

        public async Task<JsonResult> SectionLesson(int LessonID)
        {
            return Json(await new RegistrationBA().GetVideoLessonByIDAsync(LessonID));
        }
    }
}