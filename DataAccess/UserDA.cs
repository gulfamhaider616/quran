using Dapper;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.DataAccess
{
    public class UserDA
    {
        private readonly DBConnection _db = new DBConnection();

        public async Task<DataSet> SaveUserAsync(UserDO user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@UName", user.Name, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);
            parameters.Add("@Pass", user.Password, DbType.String);

            return await _db.ExecuteDataSetAsync("SaveUser", parameters);
        }

        public async Task<DataSet> VerifyUserAsync(string email, string password)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);
            parameters.Add("@Pass", password, DbType.String);

            return await _db.ExecuteDataSetAsync("VerifyUser", parameters);
        }

        public async Task<DataSet> GetUserProfileAsync(int id)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ID", id, DbType.Int32);

            return await _db.ExecuteDataSetAsync("GetUserProfile", parameters);
        }

        public async Task<DataSet> GetAllUsersAsync()
        {
            return await _db.ExecuteDataSetAsync("GetAllUsers");
        }

        public async Task<DataSet> AddookmarkAsync(UserDO user)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@id", user.BookmarkId, DbType.String);
            parameters.Add("@Email", user.Email, DbType.String);

            return await _db.ExecuteDataSetAsync("AddToBookmark", parameters);
        }

        public async Task<DataSet> GetBookMarkAsync(string email)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email, DbType.String);

            return await _db.ExecuteDataSetAsync("GETBOOKMARK", parameters);
        }
    }
}