using LearnFreeQuran.DataAccess;
using LearnFreeQuran.Library;
using System.Data;

namespace LearnFreeQuran.Business
{
    public class UserBA
    {
        public async Task<bool> SaveUserAsync(UserDO user)
        {
            DataSet dataset = await new UserDA().SaveUserAsync(user);
            string userId = "";

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow r = dataset.Tables[0].Rows[0];
                userId = r[0].ToString();
            }

            return !string.IsNullOrEmpty(userId);
        }

        public async Task<UserDO?> VerifyUserAsync(string email, string password)
        {
            DataSet dataset = await new UserDA().VerifyUserAsync(email, password);
            UserDO? user = null;

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataset.Tables[0].Rows[0];

                user = new UserDO
                {
                    Name = dr.Table.Columns.Contains("Name") ? dr["Name"].ToString() : "",
                    Email = dr.Table.Columns.Contains("Email") ? dr["Email"].ToString() : "",
                    Password = dr.Table.Columns.Contains("Pass") ? dr["Pass"].ToString() : "",
                    BookmarkId = dr.Table.Columns.Contains("BookmarkId") ? dr["BookmarkId"].ToString() : "",
                    ChapterID = dr.Table.Columns.Contains("ChapterID") ? dr["ChapterID"].ToString() : ""
                };

                if (dr.Table.Columns.Contains("UserID") && dr["UserID"] != DBNull.Value)
                {
                    user.UserID = Convert.ToInt32(dr["UserID"]);
                }
            }

            return user;
        }

        public async Task<UserDO> GetUserProfileAsync(int userID)
        {
            DataSet dataset = await new UserDA().GetUserProfileAsync(userID);
            UserDO user = new UserDO();

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataset.Tables[0].Rows[0];

                if (dr.Table.Columns.Contains("UserID") && dr["UserID"] != DBNull.Value)
                    user.UserID = Convert.ToInt32(dr["UserID"]);

                user.Name = dr.Table.Columns.Contains("Name") ? dr["Name"].ToString() : "";
                user.Email = dr.Table.Columns.Contains("Email") ? dr["Email"].ToString() : "";
                user.Password = dr.Table.Columns.Contains("Pass") ? dr["Pass"].ToString() : "";
                user.BookmarkId = dr.Table.Columns.Contains("BookmarkId") ? dr["BookmarkId"].ToString() : "";
                user.ChapterID = dr.Table.Columns.Contains("ChapterID") ? dr["ChapterID"].ToString() : "";
            }

            return user;
        }

        public async Task<bool> AddookmarkAsync(UserDO user)
        {
            DataSet dataset = await new UserDA().AddookmarkAsync(user);
            string result = "";

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                result = dataset.Tables[0].Rows[0][0].ToString();
            }

            return !string.IsNullOrEmpty(result);
        }

        public async Task<string> GetBookMarkAsync(string email)
        {
            DataSet dataset = await new UserDA().GetBookMarkAsync(email);
            string bookmarkId = "";

            if (dataset.Tables.Count > 0 && dataset.Tables[0].Rows.Count > 0)
            {
                DataRow dr = dataset.Tables[0].Rows[0];

                if (dr.Table.Columns.Contains("BookmarkId"))
                    bookmarkId = dr["BookmarkId"].ToString();
                else
                    bookmarkId = dr[0].ToString();
            }

            return bookmarkId;
        }
    }
}