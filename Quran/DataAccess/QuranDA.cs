using System.Collections.Generic;

namespace Quran.DataAccess
{
    public class QuranDA
    {
        public List<IDictionary<string, object>> GetAllSuraNames()
        {
            return Db.QueryProc("GetAllSuraNames");
        }

        public (List<IDictionary<string, object>> Header, List<IDictionary<string, object>> Ayat) GetSuraByID(int chapterId)
        {
            return Db.QueryProcTwo("GetSuraByID", new { ChapterID = chapterId });
        }

        public IDictionary<string, object> GetFeaturedVerse(int position)
        {
            const string sql = @"
SELECT q.ChapterID, q.VerseID, q.AyahText, q.EnglishTranslation, q.UrduTranslation,
       s.SuraName, s.EnglishName, t.Total
FROM (
        SELECT ChapterID, VerseID, AyahText, EnglishTranslation, UrduTranslation,
               ROW_NUMBER() OVER (ORDER BY ChapterID, VerseID) AS rn
        FROM dbo.Quran
     ) q
CROSS JOIN (SELECT COUNT(*) AS Total FROM dbo.Quran) t
LEFT JOIN dbo.SuraNames s ON q.ChapterID = s.ChapterID
WHERE q.rn = @position";

            return Db.QuerySingle(sql, new { position });
        }
    }
}
