using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace LearnFreeQuran.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VerifyAdmin(string adminname,string adminpassword)
        {
            string name = new AdminBA().VerifyAdmin(adminname, adminpassword);
            if(! String.IsNullOrEmpty(name))
            {
                FormsAuthentication.SetAuthCookie(name, false);
                return RedirectToAction("GetAllStudents");// View("GetAllStudents");
            }
            else
            {
                return View("Index");
            } 
        }

        [Authorize]
        public ActionResult GetAllStudents()
        {
            StudentListDO list = new AdminBA().GetAllStudents();
            return View(list);
        }

        [Authorize]
        public ActionResult GetUnscheduledStudents()
        {
            StudentListDO list = new AdminBA().GetUnscheduledStudents();
            return View(list);
        }

        [Authorize]
        public ActionResult GetScheduledStudents()
        {
            StudentListDO list = new AdminBA().GetScheduledStudents();
            return View(list);
        }

        [Authorize]
        public ActionResult GetTodaySchedule()
        {
            StudentListDO list = new AdminBA().GetTodaySchedule();
            return View(list);
        }

        [Authorize]
        public ActionResult SaveSchedule(string studentid,string totalclasses,string daysname,string classtime, string tutorname,string description)
        {
            ScheduleDO schedule = new ScheduleDO();
            schedule.StudentID = studentid;
            schedule.Classes = Convert.ToInt32(totalclasses);
            schedule.Days = daysname;
            schedule.ClassTime = classtime;
            schedule.TutorName = tutorname;
            schedule.Description = description;
            int result= new AdminBA().SaveSchedule(schedule);
            return RedirectToAction("GetUnscheduledStudents");
        }

        [Authorize]
        public ActionResult ChangeSchedule(string changestudentid,string changetotalclasses,string changedaysname,string changeclasstime, string changetutorname,string changedescription)
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
        public ActionResult GetContactUs()
        {
            List<ContactUsDO> contact = new AdminBA().GetAllContactUs();
            return View(contact);
        }

        [Authorize]
        public ActionResult GetFeedback()
        {
            List<FeedbackDO> list = new Business.RegistrationBA().GetFeedback();
            return View(list);
        }

        [Authorize]
        public JsonResult DeleteFeedback(int FeedbackID)
        {
            //Code to delete feedback
            int result = new AdminBA().DeleteFeedback(FeedbackID);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        #region Forum

        [Authorize]
        public ActionResult ForumMainPage()
        {
            return View(new ForumBA().GetAllQuestions().ToList());
        }

        [Authorize]
        public ActionResult Publish()
        {
            return View(new ForumBA().GetAllPublishedQuestion().ToList());
        }

        [Authorize]
        public ActionResult UnPublish()
        {
            return View(new ForumBA().GetAllUnPublishedQuestion().ToList());
        }

        [Authorize]
        public JsonResult PublishQuestionByAdmin(int QuestionID)
        {
            return Json(new ForumBA().PublishQuestionByAdmin(QuestionID), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public JsonResult DeleteQuestions(int QuestionID)
        {
            return Json(new ForumBA().DeleteQuestions(QuestionID), JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        public ActionResult AdminQuestionPreview(int QuestionID)
        {
            return View(new ForumBA().GetSingleQuestion(QuestionID));
        }

        [Authorize]
        public ActionResult StudentPreview(string StudentID)
        {
            return View(new AdminBA().StudentPreview(StudentID));
        }
        #endregion

        [Authorize]
        public ActionResult AddBook()
        {
            return View();
        }
       

        [Authorize]
        public ActionResult SaveBook(string BookID, string BookTilte, string AuthorName, HttpPostedFileBase ImagePath, HttpPostedFileBase FilePath, string BookType, string Detail)
        {
            BookDO book = new BookDO();
            book.BookTilte = BookTilte;
            book.AutherName = AuthorName;
            //book.ImagePath = ImagePath;
            var imgpath = string.Empty;
            if (ImagePath != null && ImagePath.ContentLength > 0)
            {
                var filename = Path.GetFileName(ImagePath.FileName);
                imgpath = Path.Combine(Server.MapPath("~/assets/Books/Image/"), filename);
                ImagePath.SaveAs(imgpath);
                imgpath = "~/assets/Books/Image/" + imgpath.Split(new[] { "Image\\" }, StringSplitOptions.None)[1];
                book.ImagePath = imgpath;

            }

            //book.FilePath = FilePath;
            var filepath = string.Empty;
            if (FilePath != null && FilePath.ContentLength > 0)
            {
                var filename = Path.GetFileName(FilePath.FileName);
                filepath = Path.Combine(Server.MapPath("~/assets/Books/BookFile/"), filename);
                FilePath.SaveAs(filepath);
                filepath = "~/assets/Books/BookFile/" + filepath.Split(new[] { "BookFile\\" }, StringSplitOptions.None)[1];
                book.FilePath = filepath;

            }
            book.BookType = BookType;
            book.Detail = Detail;
            string result = new AdminBA().AddBook(book);
            if(!string.IsNullOrEmpty(result))
            {
                return RedirectToAction("GetAllBooks");
            }
            else
            {
                return RedirectToAction("AddBook");
            }
        }
        public ActionResult GetBookByID(int BookID)
        {
            BookDO book = new BookDO();
            book.BookID = BookID;

            int result = 0;// new AdminBA().GetBookByID(book);
            return RedirectToAction("ChangeBook");
        }

        [Authorize]
        public ActionResult ChangeBook(int BookID, string BookTilte, string AuthorName, HttpPostedFileBase ImagePath, HttpPostedFileBase FilePath, string BookType, string Detail)
        {
            BookDO book = new BookDO();
            book.BookID = BookID;
            book.BookTilte = BookTilte;
            book.AutherName = AuthorName;
            //book.ImagePath = ImagePath;
            var imgpath = string.Empty;
            if (ImagePath != null && ImagePath.ContentLength > 0)
            {
                var filename = Path.GetFileName(ImagePath.FileName);
                imgpath = Path.Combine(Server.MapPath("~/assets/Books/Image/"), filename);
                ImagePath.SaveAs(imgpath);
                imgpath = "~/assets/Books/Image/" + imgpath.Split(new[] { "Image\\" }, StringSplitOptions.None)[1];
                book.ImagePath = imgpath;

            }
            //book.FilePath = FilePath;
            var filepath = string.Empty;
            if (FilePath != null && FilePath.ContentLength > 0)
            {
                var filename = Path.GetFileName(FilePath.FileName);
                filepath = Path.Combine(Server.MapPath("~/assets/Books/BookFile/"), filename);
                FilePath.SaveAs(filepath);
                filepath = "~/assets/Books/BookFile/" + filepath.Split(new[] { "BookFile\\" }, StringSplitOptions.None)[1];
                book.FilePath = filepath;

            }
            book.BookType = BookType;
            book.Detail = Detail;
            int result = new AdminBA().ChangeBook(book);
            return RedirectToAction("ChangeBook");
        }
        [Authorize]
        public ActionResult GetAllBooks()
        {
            List<BookDO> list = new AdminBA().GetAllBooks();
            return View(list);
        }

        [Authorize]
        public ActionResult DeleteBook(int BookID)
        {
            //Code to delete feedback
            int result = new AdminBA().DeleteBook(BookID);
            if (result > 0)
            {
                return RedirectToAction("GetAllBooks");
            }
            else
            {
                return View("Error");
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return View("Index");
        }

        
    }
}