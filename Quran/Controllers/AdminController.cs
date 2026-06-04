using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public AdminController(IWebHostEnvironment env)
        {
            _env = env;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VerifyAdmin(string adminname, string adminpassword)
        {
            string name = new AdminBA().VerifyAdmin(adminname, adminpassword);
            if (!String.IsNullOrEmpty(name))
            {
                var claims = new List<Claim> { new Claim(ClaimTypes.Name, name) };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
                return RedirectToAction("GetAllStudents");
            }
            else
            {
                return View("Index");
            }
        }

        [Authorize]
        public IActionResult GetAllStudents()
        {
            StudentListDO list = new AdminBA().GetAllStudents();
            return View(list);
        }

        [Authorize]
        public IActionResult GetUnscheduledStudents()
        {
            StudentListDO list = new AdminBA().GetUnscheduledStudents();
            return View(list);
        }

        [Authorize]
        public IActionResult GetScheduledStudents()
        {
            StudentListDO list = new AdminBA().GetScheduledStudents();
            return View(list);
        }

        [Authorize]
        public IActionResult GetTodaySchedule()
        {
            StudentListDO list = new AdminBA().GetTodaySchedule();
            return View(list);
        }

        [Authorize]
        public IActionResult SaveSchedule(string studentid, string totalclasses, string daysname, string classtime, string tutorname, string description)
        {
            ScheduleDO schedule = new ScheduleDO();
            schedule.StudentID = studentid;
            schedule.Classes = Convert.ToInt32(totalclasses);
            schedule.Days = daysname;
            schedule.ClassTime = classtime;
            schedule.TutorName = tutorname;
            schedule.Description = description;
            int result = new AdminBA().SaveSchedule(schedule);
            return RedirectToAction("GetUnscheduledStudents");
        }

        [Authorize]
        public IActionResult ChangeSchedule(string changestudentid, string changetotalclasses, string changedaysname, string changeclasstime, string changetutorname, string changedescription)
        {
            ScheduleDO schedule = new ScheduleDO();
            schedule.StudentID = changestudentid;
            schedule.Classes = Convert.ToInt32(changetotalclasses);
            schedule.Days = changedaysname;
            schedule.ClassTime = changeclasstime;
            schedule.TutorName = changetutorname;
            schedule.Description = changedescription;
            string result = new AdminBA().ChangeSchedule(schedule);
            return RedirectToAction("GetScheduledStudents");
        }

        [Authorize]
        public IActionResult GetContactUs()
        {
            List<ContactUsDO> contact = new AdminBA().GetAllContactUs();
            return View(contact);
        }

        [Authorize]
        public IActionResult GetFeedback()
        {
            List<FeedbackDO> list = new RegistrationBA().GetFeedback();
            return View(list);
        }

        [Authorize]
        public JsonResult DeleteFeedback(int FeedbackID)
        {
            int result = new AdminBA().DeleteFeedback(FeedbackID);
            return Json(result);
        }

        #region Forum

        [Authorize]
        public IActionResult ForumMainPage()
        {
            return View(new ForumBA().GetAllQuestions().ToList());
        }

        [Authorize]
        public IActionResult Publish()
        {
            return View(new ForumBA().GetAllPublishedQuestion().ToList());
        }

        [Authorize]
        public IActionResult UnPublish()
        {
            return View(new ForumBA().GetAllUnPublishedQuestion().ToList());
        }

        [Authorize]
        public JsonResult PublishQuestionByAdmin(int QuestionID)
        {
            return Json(new ForumBA().PublishQuestionByAdmin(QuestionID));
        }

        [Authorize]
        public JsonResult DeleteQuestions(int QuestionID)
        {
            return Json(new ForumBA().DeleteQuestions(QuestionID));
        }

        [Authorize]
        public IActionResult AdminQuestionPreview(int QuestionID)
        {
            return View(new ForumBA().GetSingleQuestion(QuestionID));
        }

        [Authorize]
        public IActionResult StudentPreview(string StudentID)
        {
            return View(new AdminBA().StudentPreview(StudentID));
        }
        #endregion

        [Authorize]
        public IActionResult AddBook()
        {
            return View();
        }

        [Authorize]
        public IActionResult SaveBook(string BookID, string BookTilte, string AuthorName, IFormFile ImagePath, IFormFile FilePath, string BookType, string Detail)
        {
            BookDO book = new BookDO();
            book.BookTilte = BookTilte;
            book.AutherName = AuthorName;

            if (ImagePath != null && ImagePath.Length > 0)
            {
                var filename = Path.GetFileName(ImagePath.FileName);
                var folder = Path.Combine(_env.WebRootPath, "assets", "Books", "Image");
                Directory.CreateDirectory(folder);
                using (var stream = new FileStream(Path.Combine(folder, filename), FileMode.Create))
                {
                    ImagePath.CopyTo(stream);
                }
                book.ImagePath = "~/assets/Books/Image/" + filename;
            }

            if (FilePath != null && FilePath.Length > 0)
            {
                var filename = Path.GetFileName(FilePath.FileName);
                var folder = Path.Combine(_env.WebRootPath, "assets", "Books", "BookFile");
                Directory.CreateDirectory(folder);
                using (var stream = new FileStream(Path.Combine(folder, filename), FileMode.Create))
                {
                    FilePath.CopyTo(stream);
                }
                book.FilePath = "~/assets/Books/BookFile/" + filename;
            }
            book.BookType = BookType;
            book.Detail = Detail;
            string result = new AdminBA().AddBook(book);
            if (!string.IsNullOrEmpty(result))
            {
                return RedirectToAction("GetAllBooks");
            }
            else
            {
                return RedirectToAction("AddBook");
            }
        }

        [Authorize]
        public IActionResult GetBookByID(int BookID)
        {
            var book = new AdminBA().GetAllBooks().FirstOrDefault(b => b.BookID == BookID);
            if (book == null)
            {
                return RedirectToAction("GetAllBooks");
            }
            return View("ChangeBook", book);
        }

        [Authorize]
        [HttpPost]
        public IActionResult ChangeBook(int BookID, string BookTilte, string AuthorName, IFormFile ImagePath, IFormFile FilePath, string BookType, string Detail)
        {
            var existing = new AdminBA().GetAllBooks().FirstOrDefault(b => b.BookID == BookID);

            BookDO book = new BookDO();
            book.BookID = BookID;
            book.BookTilte = BookTilte;
            book.AutherName = AuthorName;
            book.BookType = BookType;
            book.Detail = Detail;
            book.ImagePath = existing?.ImagePath;
            book.FilePath = existing?.FilePath;

            if (ImagePath != null && ImagePath.Length > 0)
            {
                var filename = Path.GetFileName(ImagePath.FileName);
                var folder = Path.Combine(_env.WebRootPath, "assets", "Books", "Image");
                Directory.CreateDirectory(folder);
                using (var stream = new FileStream(Path.Combine(folder, filename), FileMode.Create))
                {
                    ImagePath.CopyTo(stream);
                }
                book.ImagePath = "~/assets/Books/Image/" + filename;
            }

            if (FilePath != null && FilePath.Length > 0)
            {
                var filename = Path.GetFileName(FilePath.FileName);
                var folder = Path.Combine(_env.WebRootPath, "assets", "Books", "BookFile");
                Directory.CreateDirectory(folder);
                using (var stream = new FileStream(Path.Combine(folder, filename), FileMode.Create))
                {
                    FilePath.CopyTo(stream);
                }
                book.FilePath = "~/assets/Books/BookFile/" + filename;
            }

            int result = new AdminBA().ChangeBook(book);
            return RedirectToAction("GetAllBooks");
        }

        [Authorize]
        public IActionResult GetAllBooks()
        {
            List<BookDO> list = new AdminBA().GetAllBooks();
            return View(list);
        }

        [Authorize]
        public IActionResult DeleteBook(int BookID)
        {
            int result = new AdminBA().DeleteBook(BookID);
            if (result > 0)
            {
                return RedirectToAction("GetAllBooks");
            }
            else
            {
                return View("Error");
            }
        }

        #region Video Lessons

        [Authorize]
        public IActionResult VideoLessons()
        {
            return View(new RegistrationBA().GetAllVideoLesson());
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveVideoLesson(string LessonName, string LessonLink)
        {
            if (!string.IsNullOrWhiteSpace(LessonName) && !string.IsNullOrWhiteSpace(LessonLink))
            {
                new RegistrationBA().SaveVideoLesson(LessonName.Trim(), LessonLink.Trim());
            }
            return RedirectToAction("VideoLessons");
        }

        [Authorize]
        public IActionResult DeleteVideoLesson(int LessonID)
        {
            new RegistrationBA().DeleteVideoLesson(LessonID);
            return RedirectToAction("VideoLessons");
        }

        #endregion

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
}
