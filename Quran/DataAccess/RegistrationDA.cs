using System.Collections.Generic;
using Quran.Models;

namespace Quran.DataAccess
{
    public class RegistrationDA
    {
        public IDictionary<string, object> VarifyEmail(string email)
        {
            return Db.QueryProcSingle("VarifyEmail", new { Email = email });
        }

        public IDictionary<string, object> GetStudentScheduleByID(string studentID)
        {
            return Db.QueryProcSingle("GetStudentScheduleByID", new { StudentID = studentID });
        }

        public IDictionary<string, object> SaveRegistration(RegistrationDO registration)
        {
            return Db.QueryProcSingle("SaveRegistration", new
            {
                StudentName = registration.StudentName,
                FatherName = registration.FatherName,
                PhoneNumber = registration.PhoneNumber,
                Email = registration.Email,
                SkypeID = registration.SkypeID,
                Gender = registration.Gender,
                DateOfBirth = registration.DateOfBirth,
                Country = registration.Country,
                City = registration.City,
                Classes = registration.Classes,
                DaysName = registration.Days,
                FeasibleTime = registration.FeasibleTime,
                FirstLanguage = registration.FirstLanguage
            });
        }

        public IDictionary<string, object> GetForgetStudentIDByEmail(string email)
        {
            return Db.QueryProcSingle("GetForgetStudentIDByEmail", new { Email = email });
        }

        public IDictionary<string, object> SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            return Db.QueryProcSingle("SaveContactUs", new
            {
                ContactTopic = contacttopic,
                ContactEmail = contactemail,
                CotactMessage = contactmessage
            });
        }

        public IDictionary<string, object> SaveFeedback(string name, string country, string message)
        {
            return Db.QueryProcSingle("SaveFeedback", new { Name = name, Country = country, Message = message });
        }

        public List<IDictionary<string, object>> GetFeedback()
        {
            return Db.QueryProc("GetFeedback");
        }

        public IDictionary<string, object> SaveUpdateRecord(RegistrationDO registration)
        {
            return Db.QueryProcSingle("SaveUpdatedRecord", new
            {
                StudentID = registration.StudentID,
                StudentName = registration.StudentName,
                FatherName = registration.FatherName,
                PhoneNumber = registration.PhoneNumber,
                Email = registration.Email,
                SkypeID = registration.SkypeID,
                Gender = registration.Gender,
                DateOfBirth = registration.DateOfBirth,
                Country = registration.Country,
                City = registration.City,
                Classes = registration.Classes,
                DaysName = registration.Days,
                FeasibleTime = registration.FeasibleTime,
                FirstLanguage = registration.FirstLanguage
            });
        }

        public List<IDictionary<string, object>> GetAllVideoLesson()
        {
            return Db.QueryProc("GetAllVideoLessons");
        }

        public IDictionary<string, object> GetVideoLessonByID(int LessonID)
        {
            return Db.QueryProcSingle("GetVideoLessonByID", new { LessonID });
        }

        public int SaveVideoLesson(string lessonName, string link)
        {
            int? result = Db.ExecuteScalar<int?>(
                "INSERT INTO dbo.VideoLesson (LessonName, Link) VALUES (@LessonName, @Link); SELECT CAST(SCOPE_IDENTITY() AS int);",
                new { LessonName = lessonName, Link = link });
            return result ?? 0;
        }

        public int DeleteVideoLesson(int lessonId)
        {
            return Db.Execute("DELETE FROM dbo.VideoLesson WHERE Id = @Id;", new { Id = lessonId });
        }
    }
}
