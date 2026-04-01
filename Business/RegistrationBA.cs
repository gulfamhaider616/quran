using LearnFreeQuran.DataAccess;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.Business
{
    public class RegistrationBA
    {
        public async Task<bool> VarifyEmailAsync(string email)
        {
            DataSet dataset = await new RegistrationDA().VarifyEmailAsync(email);
            string studentID = "";

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                studentID = dataset.Tables[0].Rows[0]["StudentID"].ToString();
            }

            return string.IsNullOrEmpty(studentID);
        }

        public async Task<ScheduleDO> GetStudentScheduleByIDAsync(string studentID)
        {
            DataSet dataset = await new RegistrationDA().GetStudentScheduleByIDAsync(studentID);
            ScheduleDO schedule = new ScheduleDO();

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
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

            return schedule;
        }

        public async Task<RegistrationDO> SaveRegistrationAsync(RegistrationDO registration)
        {
            DataSet dataset = await new RegistrationDA().SaveRegistrationAsync(registration);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow r = dataset.Tables[0].Rows[0];

                registration.StudentID = r["StudentID"].ToString();
                registration.StudentName = r["Name"].ToString();
                registration.Email = r["Email"].ToString();
                registration.SkypeID = r["SkypeID"].ToString();
            }

            return registration;
        }

        public async Task<ScheduleDO> GetForgetStudentIDByEmailAsync(string email)
        {
            DataSet dataset = await new RegistrationDA().GetForgetStudentIDByEmailAsync(email);
            ScheduleDO schedule = new ScheduleDO();

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow r = dataset.Tables[0].Rows[0];

                schedule.StudentID = r["StudentID"].ToString();
                schedule.StudentName = r["Name"].ToString();
            }

            return schedule;
        }

        public async Task<int> SaveContactUsAsync(string contacttopic, string contactemail, string contactmessage)
        {
            DataSet dataset = await new RegistrationDA().SaveContactUsAsync(contacttopic, contactemail, contactmessage);
            int result = 0;

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(dataset.Tables[0].Rows[0]["Id"]);
            }

            return result;
        }

        public async Task<int> SaveFeedbackAsync(string name, string country, string message)
        {
            DataSet dataset = await new RegistrationDA().SaveFeedbackAsync(name, country, message);
            int result = 0;

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                result = Convert.ToInt32(dataset.Tables[0].Rows[0]["Id"]);
            }

            return result;
        }

        public async Task<List<FeedbackDO>> GetFeedbackAsync()
        {
            List<FeedbackDO> list = new List<FeedbackDO>();
            DataSet dataset = await new RegistrationDA().GetFeedbackAsync();

            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    FeedbackDO feedback = new FeedbackDO
                    {
                        Name = dr.Field<string>("Name"),
                        Country = dr.Field<string>("Country"),
                        Message = dr.Field<string>("FeedbackMessage"),
                        ID = dr.Field<int>("Id")
                    };

                    list.Add(feedback);
                }
            }

            return list;
        }

        public async Task<RegistrationDO> SaveUpdatedRecordAsync(RegistrationDO registration)
        {
            DataSet dataset = await new RegistrationDA().SaveUpdateRecordAsync(registration);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow r = dataset.Tables[0].Rows[0];

                registration.StudentID = r["StudentID"].ToString();
                registration.StudentName = r["Name"].ToString();
                registration.Email = r["Email"].ToString();
                registration.SkypeID = r["SkypeID"].ToString();
            }

            return registration;
        }

        public async Task<List<VideoLessonDO>> GetAllVideoLessonAsync()
        {
            List<VideoLessonDO> list = new List<VideoLessonDO>();
            DataSet dataset = await new RegistrationDA().GetAllVideoLessonAsync();

            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    VideoLessonDO lesson = new VideoLessonDO
                    {
                        LessonID = dr.Field<int>("Id"),
                        LessonName = dr.Field<string>("LessonName"),
                        LessonLink = dr.Field<string>("Link")
                    };

                    list.Add(lesson);
                }
            }

            return list;
        }

        public async Task<VideoLessonDO> GetVideoLessonByIDAsync(int LessonID)
        {
            VideoLessonDO lesson = new VideoLessonDO();
            DataSet dataset = await new RegistrationDA().GetVideoLessonByIDAsync(LessonID);

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow r = dataset.Tables[0].Rows[0];

                lesson.LessonID = Convert.ToInt32(r["Id"]);
                lesson.LessonName = r["LessonName"].ToString();
                lesson.LessonLink = r["Link"].ToString().Replace("watch?v=", "embed/");
            }

            return lesson;
        }
    }
}