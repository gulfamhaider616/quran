using Microsoft.AspNetCore.Mvc;

namespace Quran.Controllers
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
