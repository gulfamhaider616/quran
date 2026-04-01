using Dapper;
using System.Data;

namespace LearnFreeQuran.DataAccess
{
    public class QuranDA
    {
        private readonly DBConnection _db = new DBConnection();

        public async Task<DataSet> GetAllSuraNamesAsync()
        {
            return await _db.ExecuteDataSetAsync("GetAllSuraNames");
        }

        public async Task<DataSet> GetSuraByIDAsync(int chapterId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@ChapterID", chapterId, DbType.Int32);

            return await _db.ExecuteDataSetAsync("GetSuraByID", parameters);
        }
    }
}