using Quran.DataAccess;
using Quran.Models;
using System.Collections.Generic;

namespace Quran.Business
{
    public class UserBA
    {
        public bool SaveUser(UserDO user)
        {
            IDictionary<string, object> r = new UserDA().SaveUser(user);
            string UserID = r == null ? "" : r.Str("ID");
            return UserID != "";
        }

        public UserDO VerifyUser(string email, string password)
        {
            UserDO user = new UserDO();
            IDictionary<string, object> r = new UserDA().VerifyUser(email, password);
            if (r != null)
            {
                user.Name = r.Str("UName");
                user.Email = r.Str("EMAIL");
                user.BookmarkId = r.Str("LASTID");
                user.ChapterID = r.Str("LASTID").Split('-')[0];
            }
            return user;
        }

        public List<UserDO> GetAllUsers()
        {
            List<UserDO> list = new List<UserDO>();
            foreach (IDictionary<string, object> dr in new UserDA().GetAllUsers())
            {
                UserDO user = new UserDO();
                user.UserID = dr.Get<int>("ID");
                user.Name = dr.Get<string>("UName");
                user.Email = dr.Get<string>("Email");
                user.Password = dr.Get<string>("Pass");
                list.Add(user);
            }
            return list;
        }

        public UserDO GetUserProfile(int id)
        {
            UserDO user = new UserDO();
            IDictionary<string, object> dr = new UserDA().GetUserProfile(id);
            if (dr != null)
            {
                user.UserID = dr.Get<int>("ID");
                user.Name = dr.Get<string>("UName");
                user.Email = dr.Get<string>("Email");
                user.Password = dr.Get<string>("Pass");
            }
            return user;
        }

        public bool Addookmark(UserDO user)
        {
            IDictionary<string, object> r = new UserDA().Addookmark(user);
            string UserID = r == null ? "" : r.Str("ID");
            return UserID != "";
        }

        public string GetBookMark(string email)
        {
            IDictionary<string, object> r = new UserDA().GetBookMark(email);
            return r == null ? "" : r.Str("LASTID");
        }
    }
}
