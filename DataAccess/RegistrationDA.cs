using Dapper;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.DataAccess
{
    public class RegistrationDA
    {
        private readonly DBConnection _db = new DBConnection();

        public async Task<DataSet> VarifyEmailAsync(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            return await _db.ExecuteDataSetAsync("VarifyEmail", parameters);
        }

        public async Task<DataSet> GetStudentScheduleByIDAsync(string studentID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StudentID", studentID, DbType.String);

            return await _db.ExecuteDataSetAsync("GetStudentScheduleByID", parameters);
        }

        public async Task<DataSet> SaveRegistrationAsync(RegistrationDO registration)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StudentName", registration.StudentName, DbType.String);
            parameters.Add("@FatherName", registration.FatherName, DbType.String);
            parameters.Add("@PhoneNumber", registration.PhoneNumber, DbType.String);
            parameters.Add("@Email", registration.Email, DbType.String);
            parameters.Add("@SkypeID", registration.SkypeID, DbType.String);
            parameters.Add("@Gender", registration.Gender, DbType.String);
            parameters.Add("@DateOfBirth", registration.DateOfBirth, DbType.String);
            parameters.Add("@Country", registration.Country, DbType.String);
            parameters.Add("@City", registration.City, DbType.String);
            parameters.Add("@Classes", registration.Classes, DbType.String);
            parameters.Add("@DaysName", registration.Days, DbType.String);
            parameters.Add("@FeasibleTime", registration.FeasibleTime, DbType.String);
            parameters.Add("@FirstLanguage", registration.FirstLanguage, DbType.String);

            return await _db.ExecuteDataSetAsync("SaveRegistration", parameters);
        }

        public async Task<DataSet> GetForgetStudentIDByEmailAsync(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            return await _db.ExecuteDataSetAsync("GetForgetStudentIDByEmail", parameters);
        }

        public async Task<DataSet> SaveContactUsAsync(string contacttopic, string contactemail, string contactmessage)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ContactTopic", contacttopic, DbType.String);
            parameters.Add("@ContactEmail", contactemail, DbType.String);
            parameters.Add("@CotactMessage", contactmessage, DbType.String);

            return await _db.ExecuteDataSetAsync("SaveContactUs", parameters);
        }

        public async Task<DataSet> SaveFeedbackAsync(string name, string country, string message)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Name", name, DbType.String);
            parameters.Add("@Country", country, DbType.String);
            parameters.Add("@Message", message, DbType.String);

            return await _db.ExecuteDataSetAsync("SaveFeedback", parameters);
        }

        public async Task<DataSet> GetFeedbackAsync()
        {
            return await _db.ExecuteDataSetAsync("GetFeedback");
        }

        public async Task<DataSet> SaveUpdateRecordAsync(RegistrationDO registration)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@StudentID", registration.StudentID, DbType.String);
            parameters.Add("@StudentName", registration.StudentName, DbType.String);
            parameters.Add("@FatherName", registration.FatherName, DbType.String);
            parameters.Add("@PhoneNumber", registration.PhoneNumber, DbType.String);
            parameters.Add("@Email", registration.Email, DbType.String);
            parameters.Add("@SkypeID", registration.SkypeID, DbType.String);
            parameters.Add("@Gender", registration.Gender, DbType.String);
            parameters.Add("@DateOfBirth", registration.DateOfBirth, DbType.String);
            parameters.Add("@Country", registration.Country, DbType.String);
            parameters.Add("@City", registration.City, DbType.String);
            parameters.Add("@Classes", registration.Classes, DbType.String);
            parameters.Add("@DaysName", registration.Days, DbType.String);
            parameters.Add("@FeasibleTime", registration.FeasibleTime, DbType.String);
            parameters.Add("@FirstLanguage", registration.FirstLanguage, DbType.String);

            return await _db.ExecuteDataSetAsync("SaveUpdatedRecord", parameters);
        }

        public async Task<DataSet> GetAllVideoLessonAsync()
        {
            return await _db.ExecuteDataSetAsync("GetAllVideoLessons");
        }

        public async Task<DataSet> GetVideoLessonByIDAsync(int lessonID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@LessonID", lessonID, DbType.Int32);

            return await _db.ExecuteDataSetAsync("GetVideoLessonByID", parameters);
        }
    }
}