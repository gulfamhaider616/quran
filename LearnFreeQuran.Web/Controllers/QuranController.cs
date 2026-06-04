using LearnFreeQuran.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnFreeQuran.Web.Controllers
{
    public class QuranController : Controller
    {
        public static string username = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        // GET: Quran
        public ActionResult GetAllSuraNames()
        {
            ViewBag.userSession= username;
            return View(new QuranBA().GetAllSuraNames());
        }

        public ActionResult SuraDetail(int ChapterID,string trans)
        {
            ViewBag.userSession = username;
            SuraDetailContract contract = new QuranBA().GetSuraByID(ChapterID);
            contract.SuraList = new QuranBA().GetAllSuraNames();
            contract.trans = trans;
            return View(contract);
        }
    }
}