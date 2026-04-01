using Dapper;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.DataAccess
{
    public class AdminDA
    {
        private readonly DBConnection _db = new DBConnection();

        public async Task<DataSet> VerifyAdminAsync(string adminemail, string adminpassword)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@AdminEmail", adminemail, DbType.String);
                parameters.Add("@AdminPassword", adminpassword, DbType.String);

                return await _db.ExecuteDataSetAsync("VerifyAdmin", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing the {adminemail} in VerifyAdminAsync method.", ex);
            }
        }

        public async Task<DataSet> GetAllStudentsAsync()
        {
            try
            {
                return await _db.ExecuteDataSetAsync("GetAllStudents");
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllStudentsAsync method.", ex);
            }
        }

        public async Task<DataSet> GetUnscheduledStudentsAsync()
        {
            try
            {
                return await _db.ExecuteDataSetAsync("GetUnscheduledStudents");
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetUnscheduledStudentsAsync method.", ex);
            }
        }

        public async Task<DataSet> GetScheduledStudentsAsync()
        {
            try
            {
                return await _db.ExecuteDataSetAsync("GetScheduledStudents");
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetScheduledStudentsAsync method.", ex);
            }
        }

        public async Task<DataSet> GetTodayScheduleAsync()
        {
            try
            {
                return await _db.ExecuteDataSetAsync("GetTodaySchedule");
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetTodayScheduleAsync method.", ex);
            }
        }

        public async Task<DataSet> SaveScheduleAsync(ScheduleDO schedule)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@StudentID", schedule.StudentID, DbType.String);
                parameters.Add("@Classes", schedule.Classes, DbType.Int32);
                parameters.Add("@DaysName", schedule.Days, DbType.String);
                parameters.Add("@ClassTime", schedule.ClassTime, DbType.String);
                parameters.Add("@TutorName", schedule.TutorName, DbType.String);
                parameters.Add("@Discription", schedule.Description, DbType.String);

                return await _db.ExecuteDataSetAsync("SaveSchedule", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing SaveScheduleAsync method.", ex);
            }
        }

        public async Task<DataSet> ChangeScheduleAsync(ScheduleDO schedule)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@StudentID", schedule.StudentID, DbType.String);
                parameters.Add("@Classes", schedule.Classes, DbType.Int32);
                parameters.Add("@DaysName", schedule.Days, DbType.String);
                parameters.Add("@ClassTime", schedule.ClassTime, DbType.String);
                parameters.Add("@TutorName", schedule.TutorName, DbType.String);
                parameters.Add("@Discription", schedule.Description, DbType.String);

                return await _db.ExecuteDataSetAsync("ChangeSchedule", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing ChangeScheduleAsync method.", ex);
            }
        }

        public async Task<DataSet> GetAllContactUsAsync()
        {
            try
            {
                return await _db.ExecuteDataSetAsync("GetAllContactUs");
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllContactUsAsync method.", ex);
            }
        }

        public async Task<int> DeleteFeedbackAsync(int feedbackID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@FeedbackID", feedbackID, DbType.Int32);

            return await _db.ExecuteNonQueryAsync("learnfreequran.DeleteFeedback", parameters);
        }

        public async Task<DataSet> StudentPreviewAsync(string studentID)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@StudentID", studentID, DbType.String);

                return await _db.ExecuteDataSetAsync("StudentPreviewByID", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error executing the {studentID} in StudentPreviewAsync method.", ex);
            }
        }

        public async Task<DataSet> AddBookAsync(BookDO book)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BookTilte", book.BookTilte, DbType.String);
                parameters.Add("@AuthorName", book.AutherName, DbType.String);
                parameters.Add("@ImagePath", book.ImagePath, DbType.String);
                parameters.Add("@FilePath", book.FilePath, DbType.String);
                parameters.Add("@BookType", book.BookType, DbType.String);
                parameters.Add("@Detail", book.Detail, DbType.String);

                return await _db.ExecuteDataSetAsync("AddBook", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing AddBookAsync method.", ex);
            }
        }

        public async Task<int> ChangeBookAsync(BookDO book)
        {
            try
            {
                var parameters = new DynamicParameters();
                parameters.Add("@BookID", book.BookID, DbType.Int32);
                parameters.Add("@BookTilte", book.BookTilte, DbType.String);
                parameters.Add("@AuthorName", book.AutherName, DbType.String);
                parameters.Add("@ImagePath", book.ImagePath, DbType.String);
                parameters.Add("@FilePath", book.FilePath, DbType.String);
                parameters.Add("@BookType", book.BookType, DbType.String);
                parameters.Add("@Detail", book.Detail, DbType.String);

                return await _db.ExecuteNonQueryAsync("ChangeBook", parameters);
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing ChangeBookAsync method.", ex);
            }
        }

        public async Task<DataSet> GetAllBooksAsync()
        {
            try
            {
                return await _db.ExecuteDataSetAsync("GetAllBooks");
            }
            catch (Exception ex)
            {
                throw new Exception("Error executing GetAllBooksAsync method.", ex);
            }
        }

        public async Task<int> DeleteBookAsync(int bookID)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@BookID", bookID, DbType.Int32);

            return await _db.ExecuteNonQueryAsync("ph16200308291.DeleteBook", parameters);
        }
    }
}