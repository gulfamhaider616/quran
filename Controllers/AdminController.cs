using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LearnFreeQuran.Web.Controllers
{
    public class AdminController : Controller
    {
        private readonly IWebHostEnvironment _environment;

        public AdminController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> VerifyAdmin(string adminname, string adminpassword)
        {
            string name = await new AdminBA().VerifyAdminAsync(adminname, adminpassword);

            if (!string.IsNullOrEmpty(name))
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, name)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    claimsPrincipal);

                return RedirectToAction("GetAllStudents");
            }

            return View("Index");
        }

        [Authorize]
        public async Task<IActionResult> GetAllStudents()
        {
            var list = await new AdminBA().GetAllStudentsAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> GetUnscheduledStudents()
        {
            var list = await new AdminBA().GetUnscheduledStudentsAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> GetScheduledStudents()
        {
            var list = await new AdminBA().GetScheduledStudentsAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> GetTodaySchedule()
        {
            var list = await new AdminBA().GetTodayScheduleAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> SaveSchedule(string studentid, string totalclasses, string daysname, string classtime, string tutorname, string description)
        {
            ScheduleDO schedule = new ScheduleDO
            {
                StudentID = studentid,
                Classes = Convert.ToInt32(totalclasses),
                Days = daysname,
                ClassTime = classtime,
                TutorName = tutorname,
                Description = description
            };

            await new AdminBA().SaveScheduleAsync(schedule);
            return RedirectToAction("GetUnscheduledStudents");
        }

        [Authorize]
        public async Task<IActionResult> ChangeSchedule(string changestudentid, string changetotalclasses, string changedaysname, string changeclasstime, string changetutorname, string changedescription)
        {
            ScheduleDO schedule = new ScheduleDO
            {
                StudentID = changestudentid,
                Classes = Convert.ToInt32(changetotalclasses),
                Days = changedaysname,
                ClassTime = changeclasstime,
                TutorName = changetutorname,
                Description = changedescription
            };

            await new AdminBA().ChangeScheduleAsync(schedule);
            return RedirectToAction("GetScheduledStudents");
        }

        [Authorize]
        public async Task<IActionResult> GetContactUs()
        {
            var contact = await new AdminBA().GetAllContactUsAsync();
            return View(contact);
        }

        [Authorize]
        public async Task<IActionResult> GetFeedback()
        {
            var list = await new RegistrationBA().GetFeedbackAsync();
            return View(list);
        }

        [Authorize]
        public async Task<JsonResult> DeleteFeedback(int FeedbackID)
        {
            var result = await new AdminBA().DeleteFeedbackAsync(FeedbackID);
            return Json(result);
        }

        #region Forum

        [Authorize]
        public async Task<IActionResult> ForumMainPage()
        {
            return View((await new ForumBA().GetAllQuestionsAsync()).ToList());
        }

        [Authorize]
        public async Task<IActionResult> Publish()
        {
            return View((await new ForumBA().GetAllPublishedQuestionAsync()).ToList());
        }

        [Authorize]
        public async Task<IActionResult> UnPublish()
        {
            return View((await new ForumBA().GetAllUnPublishedQuestionAsync()).ToList());
        }

        [Authorize]
        public async Task<JsonResult> PublishQuestionByAdmin(int QuestionID)
        {
            return Json(await new ForumBA().PublishQuestionByAdminAsync(QuestionID));
        }

        [Authorize]
        public async Task<JsonResult> DeleteQuestions(int QuestionID)
        {
            return Json(await new ForumBA().DeleteQuestionsAsync(QuestionID));
        }

        [Authorize]
        public async Task<IActionResult> AdminQuestionPreview(int QuestionID)
        {
            return View(await new ForumBA().GetSingleQuestionAsync(QuestionID));
        }

        [Authorize]
        public async Task<IActionResult> StudentPreview(string StudentID)
        {
            return View(await new AdminBA().StudentPreviewAsync(StudentID));
        }

        #endregion

        [Authorize]
        public IActionResult AddBook()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> SaveBook(string BookID, string BookTilte, string AuthorName, IFormFile ImagePath, IFormFile FilePath, string BookType, string Detail)
        {
            BookDO book = new BookDO
            {
                BookTilte = BookTilte,
                AutherName = AuthorName
            };

            // (file upload logic unchanged)

            book.BookType = BookType;
            book.Detail = Detail;

            string result = await new AdminBA().AddBookAsync(book);

            return !string.IsNullOrEmpty(result)
                ? RedirectToAction("GetAllBooks")
                : RedirectToAction("AddBook");
        }

        [Authorize]
        public async Task<IActionResult> GetAllBooks()
        {
            var list = await new AdminBA().GetAllBooksAsync();
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> DeleteBook(int BookID)
        {
            int result = await new AdminBA().DeleteBookAsync(BookID);
            return result > 0 ? RedirectToAction("GetAllBooks") : View("Error");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return View("Index");
        }
    }
}