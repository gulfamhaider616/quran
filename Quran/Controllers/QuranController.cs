using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class QuranController : Controller
    {
        public IActionResult GetAllSuraNames()
        {
            return View(new QuranBA().GetAllSuraNames());
        }

        public IActionResult SuraDetail(string slug, string trans)
        {
            int chapterId = new QuranBA().GetChapterIdBySlug(slug);
            if (chapterId == 0)
            {
                return RedirectToAction("GetAllSuraNames");
            }
            SuraDetailContract contract = new QuranBA().GetSuraByID(chapterId);
            contract.SuraList = new QuranBA().GetAllSuraNames();
            contract.trans = trans;
            return View(contract);
        }

        // Legacy /quran_reading?ChapterID=N — permanently redirect to the slug URL (keeps old
        // links, sitemap entries and search rankings working). The browser re-appends any #verse.
        public IActionResult SuraDetailLegacy(int ChapterID, string trans)
        {
            string slug = new QuranBA().GetSlugByChapterId(ChapterID);
            if (string.IsNullOrEmpty(slug))
            {
                return RedirectToAction("GetAllSuraNames");
            }
            string url = "/quran/" + slug;
            if (!string.IsNullOrEmpty(trans)) { url += "/" + trans; }
            return RedirectPermanent(url);
        }
    }
}
