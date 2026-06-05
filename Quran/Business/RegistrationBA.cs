using Quran.DataAccess;
using Quran.Models;
using System.Collections.Generic;

namespace Quran.Business
{
    public class RegistrationBA
    {
        public bool VarifyEmail(string email)
        {
            IDictionary<string, object> r = new RegistrationDA().VarifyEmail(email);
            string studentID = r == null ? "" : r.Str("StudentID");
            return studentID == "";
        }

        public ScheduleDO GetStudentScheduleByID(string studentID)
        {
            ScheduleDO schedule = new ScheduleDO();
            IDictionary<string, object> r = new RegistrationDA().GetStudentScheduleByID(studentID);
            if (r != null)
            {
                schedule.StudentID = r.Str("StudentID");
                schedule.StudentName = r.Str("Name");
                schedule.Classes = r.Get<int>("Classes");
                schedule.Days = r.Str("DaysName");
                schedule.ClassTime = r.Str("ClassTime");
                schedule.TutorName = r.Str("TutorName");
                schedule.Description = r.Str("Description");
            }
            return schedule;
        }

        public RegistrationDO SaveRegistration(RegistrationDO registration)
        {
            IDictionary<string, object> r = new RegistrationDA().SaveRegistration(registration);
            if (r != null)
            {
                registration.StudentID = r.Str("StudentID");
                registration.StudentName = r.Str("Name");
                registration.Email = r.Str("Email");
                registration.SkypeID = r.Str("SkypeID");
            }
            return registration;
        }

        public ScheduleDO GetForgetStudentIDByEmail(string email)
        {
            ScheduleDO schedule = new ScheduleDO();
            IDictionary<string, object> r = new RegistrationDA().GetForgetStudentIDByEmail(email);
            if (r != null)
            {
                schedule.StudentID = r.Str("StudentID");
                schedule.StudentName = r.Str("Name");
            }
            return schedule;
        }

        public int SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {
            IDictionary<string, object> r = new RegistrationDA().SaveContactUs(contacttopic, contactemail, contactmessage);
            return r == null ? 0 : r.Get<int>("Id");
        }

        public int SaveFeedback(string name, string country, string message)
        {
            IDictionary<string, object> r = new RegistrationDA().SaveFeedback(name, country, message);
            return r == null ? 0 : r.Get<int>("Id");
        }

        public List<FeedbackDO> GetFeedback()
        {
            List<FeedbackDO> list = new List<FeedbackDO>();
            foreach (IDictionary<string, object> dr in new RegistrationDA().GetFeedback())
            {
                FeedbackDO FeedbackList = new FeedbackDO();
                FeedbackList.Name = dr.Get<string>("Name");
                FeedbackList.Country = dr.Get<string>("Country");
                FeedbackList.Message = dr.Get<string>("FeedbackMessage");
                FeedbackList.ID = dr.Get<int>("Id");
                list.Add(FeedbackList);
            }
            return list;
        }

        public RegistrationDO SaveUpdatedRecord(RegistrationDO registration)
        {
            IDictionary<string, object> r = new RegistrationDA().SaveUpdateRecord(registration);
            if (r != null)
            {
                registration.StudentID = r.Str("StudentID");
                registration.StudentName = r.Str("Name");
                registration.Email = r.Str("Email");
                registration.SkypeID = r.Str("SkypeID");
            }
            return registration;
        }

        public List<VideoLessonDO> GetAllVideoLesson()
        {
            List<VideoLessonDO> list = new List<VideoLessonDO>();
            foreach (IDictionary<string, object> dr in new RegistrationDA().GetAllVideoLesson())
            {
                VideoLessonDO lesson = new VideoLessonDO();
                lesson.LessonID = dr.Get<int>("Id");
                lesson.LessonName = dr.Get<string>("LessonName");
                lesson.LessonLink = dr.Get<string>("Link");
                list.Add(lesson);
            }
            return list;
        }

        public VideoLessonDO GetVideoLessonByID(int LessonID)
        {
            VideoLessonDO lesson = new VideoLessonDO();
            IDictionary<string, object> r = new RegistrationDA().GetVideoLessonByID(LessonID);
            if (r != null)
            {
                lesson.LessonID = r.Get<int>("Id");
                lesson.LessonName = r.Str("LessonName");
                lesson.LessonLink = r.Str("Link").Replace("watch?v=", "embed/");
            }
            return lesson;
        }

        public int SaveVideoLesson(string lessonName, string link)
        {
            return new RegistrationDA().SaveVideoLesson(lessonName, link);
        }

        public bool DeleteVideoLesson(int lessonId)
        {
            return new RegistrationDA().DeleteVideoLesson(lessonId) > 0;
        }
    }
}
