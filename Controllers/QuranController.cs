using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using Microsoft.AspNetCore.Mvc;

namespace LearnFreeQuran.Web.Controllers
{
    public class QuranController : Controller
    {
        public static string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;

        // GET: Quran
        public async Task<IActionResult> GetAllSuraNames()
        {
            ViewBag.userSession = username;
            return View(await new QuranBA().GetAllSuraNamesAsync());
        }

        public async Task<IActionResult> SuraDetail(int ChapterID, string trans)
        {
            ViewBag.userSession = username;

            SuraDetailContract contract = await new QuranBA().GetSuraByIDAsync(ChapterID);
            contract.SuraList = await new QuranBA().GetAllSuraNamesAsync();
            contract.trans = trans;

            return View(contract);
        }
    }
}