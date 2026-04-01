using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnFreeQuran.Library
{
    public class UserDO
    {
        public string Name { get; set; }
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string BookmarkId { get; set; }
        public string ChapterID { get; set; }
        public string VerseID { get; set; }
    }
}
