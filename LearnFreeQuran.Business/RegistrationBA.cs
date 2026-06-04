using LearnFreeQuran.DataAccess;
using LearnFreeQuran.Library;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFreeQuran.Business
{
   public class RegistrationBA
    {
        public bool VarifyEmail(string email)
        {
            DataSet dataset = new RegistrationDA().VarifyEmail(email);
            var studentID = "";
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    studentID = r["StudentID"].ToString();
                }
            }
            if(studentID == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public ScheduleDO GetStudentScheduleByID(string studentID)
        {
            DataSet dataset = new RegistrationDA().GetStudentScheduleByID(studentID);
            ScheduleDO schedule = new ScheduleDO();
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    schedule.StudentID = r["StudentID"].ToString();
                    schedule.StudentName = r["Name"].ToString();
                    schedule.Classes = Convert.ToInt32(r["Classes"]);
                    schedule.Days = r["DaysName"].ToString();
                    schedule.ClassTime = r["ClassTime"].ToString();
                    schedule.TutorName = r["TutorName"].ToString();
                    schedule.Description = r["Description"].ToString();
                }
            }
            return schedule;
        }
        public RegistrationDO SaveRegistration(RegistrationDO registration)
        {
            DataSet dataset = new RegistrationDA().SaveRegistration(registration);
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    registration.StudentID = r["StudentID"].ToString();
                    registration.StudentName = r["Name"].ToString();
                    registration.Email = r["Email"].ToString();
                    registration.SkypeID = r["SkypeID"].ToString();
                }
            }
            return registration;
        }

        public ScheduleDO GetForgetStudentIDByEmail(string email)
        {

            DataSet dataset = new RegistrationDA().GetForgetStudentIDByEmail(email);
            ScheduleDO schedule = new ScheduleDO();
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    schedule.StudentID = r["StudentID"].ToString();
                    schedule.StudentName = r["Name"].ToString();
                }
            }
            return schedule;
        }


        public int SaveContactUs(string contacttopic, string contactemail, string contactmessage)
        {

            DataSet dataset = new RegistrationDA().SaveContactUs(contacttopic, contactemail, contactmessage);
            int result = 0;
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    result = Convert.ToInt32(r["Id"]);
                }
            }
            return result;
        }

        public int SaveFeedback(string name, string country, string message)
        {

            DataSet dataset = new RegistrationDA().SaveFeedback(name,country,message);
            int result = 0;
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    result = Convert.ToInt32(r["Id"]);
                }
            }
            return result;
        }

        public List<FeedbackDO> GetFeedback()
        {
            List<FeedbackDO> list = new List<FeedbackDO>();
            DataSet dataset = new RegistrationDA().GetFeedback();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    FeedbackDO FeedbackList = new FeedbackDO();
                    FeedbackList.Name = dr.Field<string>("Name");
                    FeedbackList.Country = dr.Field<string>("Country");
                    FeedbackList.Message = dr.Field<string>("FeedbackMessage");
                    FeedbackList.ID = dr.Field<int>("Id");
                    list.Add(FeedbackList);
                }
            }
            return list;
        }

        public RegistrationDO SaveUpdatedRecord(RegistrationDO registration)
        {
            DataSet dataset = new RegistrationDA().SaveUpdateRecord(registration);
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    registration.StudentID = r["StudentID"].ToString();
                    registration.StudentName = r["Name"].ToString();
                    registration.Email = r["Email"].ToString();
                    registration.SkypeID = r["SkypeID"].ToString();
                }
            }
            return registration;
        }

        public List<VideoLessonDO> GetAllVideoLesson()
        {
            List<VideoLessonDO> list = new List<VideoLessonDO>();
            DataSet dataset = new RegistrationDA().GetAllVideoLesson();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    VideoLessonDO lesson = new VideoLessonDO();
                    lesson.LessonID = dr.Field<int>("Id");
                    lesson.LessonName = dr.Field<string>("LessonName");
                    lesson.LessonLink = dr.Field<string>("Link");
                    list.Add(lesson);
                }
            }
            return list;
        }

        public VideoLessonDO GetVideoLessonByID(int LessonID)
        {
            VideoLessonDO lesson = new VideoLessonDO();
            DataSet dataset = new RegistrationDA().GetVideoLessonByID(LessonID);
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    lesson.LessonID =Convert.ToInt32(r["Id"]);
                    lesson.LessonName = r["LessonName"].ToString();
                    lesson.LessonLink = r["Link"].ToString().Replace("watch?v=", "embed/");
                }
            }
            return lesson;
        }
    }
}
