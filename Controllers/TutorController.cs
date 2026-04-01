using Microsoft.AspNetCore.Mvc;

namespace LearnFreeQuran.Web.Controllers
{
    public class TutorController : Controller
    {
        // GET: Tutor
        public IActionResult NewTutorInformatinView()
        {
            return View();
        }
    }
}