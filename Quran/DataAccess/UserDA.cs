using System.Collections.Generic;
using Quran.Models;

namespace Quran.DataAccess
{
    public class UserDA
    {
        public IDictionary<string, object> SaveUser(UserDO user)
        {
            return Db.QueryProcSingle("SaveUser", new { UName = user.Name, Email = user.Email, Pass = user.Password });
        }

        public IDictionary<string, object> VerifyUser(string email, string password)
        {
            return Db.QueryProcSingle("VerifyUser", new { Email = email, Pass = password });
        }

        public IDictionary<string, object> GetUserProfile(int Id)
        {
            return Db.QueryProcSingle("GetUserProfile", new { ID = Id });
        }

        public List<IDictionary<string, object>> GetAllUsers()
        {
            return Db.QueryProc("GetAllUsers");
        }

        public IDictionary<string, object> Addookmark(UserDO user)
        {
            return Db.QueryProcSingle("AddToBookmark", new { id = user.BookmarkId, Email = user.Email });
        }

        public IDictionary<string, object> GetBookMark(string email)
        {
            return Db.QueryProcSingle("GETBOOKMARK", new { Email = email });
        }
    }
}
