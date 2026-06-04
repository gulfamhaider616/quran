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
    public class UserBA
    {
        public bool SaveUser(UserDO user)
        {
            DataSet dataset = new UserDA().SaveUser(user);
            var UserID = "";
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    UserID = r["ID"].ToString();
                }
            }
            if (UserID == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        public UserDO VerifyUser(string email, string password)
        {
            DataSet dataset = new UserDA().VerifyUser(email, password);
            UserDO user = new UserDO();
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    user.Name = r["UName"].ToString();
                    user.Email = r["EMAIL"].ToString();
                    user.BookmarkId = r["LASTID"].ToString();
                    user.ChapterID = r["LASTID"].ToString().Split('-')[0];
                }
            }
            return user;
        }
        public List<UserDO> GetAllUsers()
        {
            List<UserDO> list = new List<UserDO>();
            DataSet dataset = new UserDA().GetAllUsers();
            if (dataset.Tables.Count > 0)
            {
                foreach (DataRow dr in dataset.Tables[0].Rows)
                {
                    UserDO user = new UserDO();
                    user.UserID = dr.Field<int>("ID");
                    user.Name = dr.Field<string>("UName");
                    user.Email = dr.Field<string>("Email");
                    user.Password = dr.Field<string>("Pass");
                    list.Add(user);
                }
            }
            return list;
        }
        public UserDO GetUserProfile(int id)
        {
            UserDO user = new UserDO();
            DataSet dataset = new UserDA().GetUserProfile(id);
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow dr = dataset.Tables[0].Rows[0];
                    user.UserID = dr.Field<int>("ID");
                    user.Name = dr.Field<string>("UName");
                    user.Email = dr.Field<string>("Email");
                    user.Password = dr.Field<string>("Pass");
                }
            }
            return user;
        }
        public bool Addookmark(UserDO user)
        {
            DataSet dataset = new UserDA().Addookmark(user);
            var UserID = "";
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    UserID = r["ID"].ToString();
                }
            }
            if (UserID == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public string GetBookMark(string email)
        {
            DataSet dataset = new UserDA().GetBookMark(email);
            var BookmarkId = "";
            if (dataset.Tables.Count > 0)
            {
                if (dataset.Tables[0].Rows.Count > 0)
                {
                    DataRow r = dataset.Tables[0].Rows[0];
                    BookmarkId = r["LASTID"].ToString();
                }
            }
            return BookmarkId;
        }
    }
}
