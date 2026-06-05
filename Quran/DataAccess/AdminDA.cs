using System.Collections.Generic;
using Quran.Models;

namespace Quran.DataAccess
{
    public class AdminDA
    {
        public IDictionary<string, object> VerifyAdmin(string adminemail, string adminpassword)
        {
            return Db.QueryProcSingle("VerifyAdmin", new { AdminEmail = adminemail, AdminPassword = adminpassword });
        }

        public (List<IDictionary<string, object>> Rows, List<IDictionary<string, object>> Counts) GetAllStudents()
        {
            return Db.QueryProcTwo("GetAllStudents");
        }

        public (List<IDictionary<string, object>> Rows, List<IDictionary<string, object>> Counts) GetUnscheduledStudents()
        {
            return Db.QueryProcTwo("GetUnscheduledStudents");
        }

        public (List<IDictionary<string, object>> Rows, List<IDictionary<string, object>> Counts) GetScheduledStudents()
        {
            return Db.QueryProcTwo("GetScheduledStudents");
        }

        public (List<IDictionary<string, object>> Rows, List<IDictionary<string, object>> Counts) GetTodaySchedule()
        {
            return Db.QueryProcTwo("GetTodaySchedule");
        }

        public IDictionary<string, object> SaveSchedule(ScheduleDO schedule)
        {
            return Db.QueryProcSingle("SaveSchedule", new
            {
                StudentID = schedule.StudentID,
                Classes = schedule.Classes,
                DaysName = schedule.Days,
                ClassTime = schedule.ClassTime,
                TutorName = schedule.TutorName,
                Discription = schedule.Description
            });
        }

        public IDictionary<string, object> ChangeSchedule(ScheduleDO schedule)
        {
            return Db.QueryProcSingle("ChangeSchedule", new
            {
                StudentID = schedule.StudentID,
                Classes = schedule.Classes,
                DaysName = schedule.Days,
                ClassTime = schedule.ClassTime,
                TutorName = schedule.TutorName,
                Discription = schedule.Description
            });
        }

        public List<IDictionary<string, object>> GetAllContactUs()
        {
            return Db.QueryProc("GetAllContactUs");
        }

        public int DeleteFeedback(int feedbackID)
        {
            return Db.ExecuteProc("DeleteFeedback", new { FeedbackID = feedbackID });
        }

        public IDictionary<string, object> StudentPreview(string studentID)
        {
            return Db.QueryProcSingle("StudentPreviewByID", new { StudentID = studentID });
        }

        public IDictionary<string, object> AddBook(BookDO book)
        {
            return Db.QueryProcSingle("AddBook", new
            {
                BookTilte = book.BookTilte,
                AuthorName = book.AutherName,
                ImagePath = book.ImagePath,
                FilePath = book.FilePath,
                BookType = book.BookType,
                Detail = book.Detail
            });
        }

        public int GetBookByID(int BookID)
        {
            BookDO book = new BookDO();
            return Db.ExecuteProc("GetBookByID", new
            {
                BookID = book.BookID,
                BookTilte = book.BookTilte,
                AuthorName = book.AutherName,
                ImagePath = book.ImagePath,
                FilePath = book.FilePath,
                BookType = book.BookType,
                Detail = book.Detail
            });
        }

        public int ChangeBook(BookDO book)
        {
            return Db.ExecuteProc("ChangeBook", new
            {
                BookID = book.BookID,
                BookTilte = book.BookTilte,
                AuthorName = book.AutherName,
                ImagePath = book.ImagePath,
                FilePath = book.FilePath,
                BookType = book.BookType,
                Detail = book.Detail
            });
        }

        public List<IDictionary<string, object>> GetAllBooks()
        {
            return Db.QueryProc("GetAllBooks");
        }

        public int DeleteBook(int BookID)
        {
            return Db.ExecuteProc("DeleteBook", new { BookID });
        }

        public List<IDictionary<string, object>> GetAllAdmins()
        {
            return Db.Query("SELECT Id, AdminName, AdminEmail, AdminPassword FROM dbo.AdminUser ORDER BY Id;");
        }

        public IDictionary<string, object> GetAdminById(int id)
        {
            return Db.QuerySingle("SELECT Id, AdminName, AdminEmail, AdminPassword FROM dbo.AdminUser WHERE Id = @Id;",
                new { Id = id });
        }

        public int AdminEmailExists(string email, int excludeId)
        {
            return Db.ExecuteScalar<int>(
                "SELECT COUNT(*) FROM dbo.AdminUser WHERE AdminEmail = @AdminEmail AND Id <> @Id;",
                new { AdminEmail = email, Id = excludeId });
        }

        public int SaveAdmin(AdminUserDO admin)
        {
            int? result = Db.ExecuteScalar<int?>(
                "INSERT INTO dbo.AdminUser (AdminName, AdminEmail, AdminPassword) VALUES (@AdminName, @AdminEmail, @AdminPassword); SELECT CAST(SCOPE_IDENTITY() AS int);",
                new { admin.AdminName, admin.AdminEmail, admin.AdminPassword });
            return result ?? 0;
        }

        public int UpdateAdmin(AdminUserDO admin)
        {
            return Db.Execute(
                "UPDATE dbo.AdminUser SET AdminName = @AdminName, AdminEmail = @AdminEmail, AdminPassword = @AdminPassword WHERE Id = @Id;",
                new { admin.Id, admin.AdminName, admin.AdminEmail, admin.AdminPassword });
        }

        public int DeleteAdmin(int id)
        {
            return Db.Execute("DELETE FROM dbo.AdminUser WHERE Id = @Id;", new { Id = id });
        }
    }
}
