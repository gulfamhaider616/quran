using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class QuranController : Controller
    {
        // GET: Quran
        public IActionResult GetAllSuraNames()
        {
            return View(new QuranBA().GetAllSuraNames());
        }

        public IActionResult SuraDetail(int ChapterID, string trans)
        {
            SuraDetailContract contract = new QuranBA().GetSuraByID(ChapterID);
            contract.SuraList = new QuranBA().GetAllSuraNames();
            contract.trans = trans;
            return View(contract);
        }
    }
}
