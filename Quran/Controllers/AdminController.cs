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
using Quran.Helpers;

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
            if (User?.Identity?.IsAuthenticated ?? false)
            {
                return RedirectToAction("GetAllStudents");
            }
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
        public JsonResult UnPublishQuestionByAdmin(int QuestionID)
        {
            return Json(new ForumBA().UnPublishQuestionByAdmin(QuestionID));
        }

        [Authorize]
        public JsonResult DeleteQuestions(int QuestionID)
        {
            return Json(new ForumBA().DeleteQuestions(QuestionID));
        }

        [Authorize]
        public IActionResult AdminQuestionPreview(string slug)
        {
            ForumBA ba = new ForumBA();
            int id = ba.GetQuestionIdBySlug(slug, true);
            if (id == 0) { return RedirectToAction("ForumMainPage"); }
            return View(ba.GetSingleQuestion(id));
        }

        [Authorize]
        public IActionResult StudentPreview(string id)
        {
            return View(new AdminBA().StudentPreview(id));
        }
        #endregion

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
            TempData[!string.IsNullOrEmpty(result) ? "BookSuccess" : "BookError"] =
                !string.IsNullOrEmpty(result) ? "Book added successfully." : "Could not add the book. Please try again.";
            return RedirectToAction("GetAllBooks");
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
            TempData["BookSuccess"] = "Book updated successfully.";
            return RedirectToAction("GetAllBooks");
        }

        [Authorize]
        public IActionResult GetAllBooks()
        {
            List<BookDO> list = new AdminBA().GetAllBooks();
            return View(list);
        }

        [Authorize]
        public IActionResult DeleteBook(string id)
        {
            int bookId = new AdminBA().GetBookIdBySlug(id);
            if (bookId == 0)
            {
                TempData["BookError"] = "Book not found.";
                return RedirectToAction("GetAllBooks");
            }
            int result = new AdminBA().DeleteBook(bookId);
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
        [RequestSizeLimit(524288000)]
        public IActionResult SaveVideoLesson(string LessonName, string LessonLink, IFormFile LessonFile)
        {
            if (string.IsNullOrWhiteSpace(LessonName))
            {
                TempData["VideoError"] = "Lesson title is required.";
                return RedirectToAction("VideoLessons");
            }

            string link = (LessonLink ?? "").Trim();

            if (LessonFile != null && LessonFile.Length > 0)
            {
                string[] allowed = { ".mp4", ".webm", ".ogg", ".mov", ".m4v" };
                var ext = Path.GetExtension(LessonFile.FileName).ToLowerInvariant();
                if (Array.IndexOf(allowed, ext) < 0)
                {
                    TempData["VideoError"] = "Unsupported video format. Please upload MP4, WebM, OGG, MOV or M4V.";
                    return RedirectToAction("VideoLessons");
                }
                var folder = Path.Combine(_env.WebRootPath, "assets", "Videos");
                Directory.CreateDirectory(folder);
                var fileName = Guid.NewGuid().ToString("N") + ext;
                using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
                {
                    LessonFile.CopyTo(stream);
                }
                link = "/assets/Videos/" + fileName;
            }

            if (string.IsNullOrWhiteSpace(link))
            {
                TempData["VideoError"] = "Please provide a YouTube link or upload a video file.";
                return RedirectToAction("VideoLessons");
            }

            new RegistrationBA().SaveVideoLesson(LessonName.Trim(), link);
            TempData["VideoSuccess"] = "Video lesson added successfully.";
            return RedirectToAction("VideoLessons");
        }

        [Authorize]
        public IActionResult DeleteVideoLesson(string id)
        {
            int lessonId = new RegistrationBA().GetLessonIdBySlug(id);
            if (lessonId == 0)
            {
                TempData["VideoError"] = "Video lesson not found.";
                return RedirectToAction("VideoLessons");
            }
            var lesson = new RegistrationBA().GetVideoLessonByID(lessonId);
            new RegistrationBA().DeleteVideoLesson(lessonId);

            if (lesson != null && !string.IsNullOrWhiteSpace(lesson.LessonLink) &&
                lesson.LessonLink.StartsWith("/assets/Videos/", StringComparison.OrdinalIgnoreCase))
            {
                try
                {
                    var path = Path.Combine(_env.WebRootPath, lesson.LessonLink.TrimStart('/').Replace('/', Path.DirectorySeparatorChar));
                    if (System.IO.File.Exists(path)) { System.IO.File.Delete(path); }
                }
                catch { }
            }
            return RedirectToAction("VideoLessons");
        }

        #endregion

        #region Admins

        [Authorize]
        public IActionResult ManageAdmins()
        {
            return View(new AdminBA().GetAllAdmins());
        }

        [Authorize]
        [HttpPost]
        public IActionResult SaveAdminUser(string AdminName, string AdminEmail, string AdminPassword)
        {
            if (string.IsNullOrWhiteSpace(AdminName) || string.IsNullOrWhiteSpace(AdminEmail) || string.IsNullOrWhiteSpace(AdminPassword))
            {
                TempData["AdminError"] = "Name, email and password are all required.";
                return RedirectToAction("ManageAdmins");
            }
            if (new AdminBA().AdminEmailExists(AdminEmail.Trim(), 0))
            {
                TempData["AdminError"] = "An admin with that email already exists.";
                return RedirectToAction("ManageAdmins");
            }
            AdminUserDO admin = new AdminUserDO
            {
                AdminName = AdminName.Trim(),
                AdminEmail = AdminEmail.Trim(),
                AdminPassword = AdminPassword.Trim()
            };
            new AdminBA().SaveAdmin(admin);
            TempData["AdminSuccess"] = "Admin added successfully.";
            return RedirectToAction("ManageAdmins");
        }

        [Authorize]
        [HttpPost]
        public IActionResult UpdateAdminUser(int Id, string AdminName, string AdminEmail, string AdminPassword)
        {
            if (string.IsNullOrWhiteSpace(AdminName) || string.IsNullOrWhiteSpace(AdminEmail) || string.IsNullOrWhiteSpace(AdminPassword))
            {
                TempData["AdminError"] = "Name, email and password are all required.";
                return RedirectToAction("ManageAdmins");
            }
            if (new AdminBA().AdminEmailExists(AdminEmail.Trim(), Id))
            {
                TempData["AdminError"] = "Another admin with that email already exists.";
                return RedirectToAction("ManageAdmins");
            }
            AdminUserDO admin = new AdminUserDO
            {
                Id = Id,
                AdminName = AdminName.Trim(),
                AdminEmail = AdminEmail.Trim(),
                AdminPassword = AdminPassword.Trim()
            };
            new AdminBA().UpdateAdmin(admin);
            TempData["AdminSuccess"] = "Admin updated successfully.";
            return RedirectToAction("ManageAdmins");
        }

        [Authorize]
        public IActionResult DeleteAdminUser(string id)
        {
            int Id = new AdminBA().GetAdminIdBySlug(id);
            var admins = new AdminBA().GetAllAdmins();
            if (Id == 0)
            {
                TempData["AdminError"] = "Admin not found.";
                return RedirectToAction("ManageAdmins");
            }
            if (admins.Count <= 1)
            {
                TempData["AdminError"] = "You cannot delete the last remaining admin.";
                return RedirectToAction("ManageAdmins");
            }
            var target = admins.FirstOrDefault(a => a.Id == Id);
            if (target != null && string.Equals(target.AdminName, User?.Identity?.Name, StringComparison.OrdinalIgnoreCase))
            {
                TempData["AdminError"] = "You cannot delete the account you are currently logged in with.";
                return RedirectToAction("ManageAdmins");
            }
            new AdminBA().DeleteAdmin(Id);
            TempData["AdminSuccess"] = "Admin deleted successfully.";
            return RedirectToAction("ManageAdmins");
        }

        #endregion

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index");
        }
    }
}
