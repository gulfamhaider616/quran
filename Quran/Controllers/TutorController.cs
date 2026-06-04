using Microsoft.AspNetCore.Mvc;

namespace Quran.Controllers
{
    public class TutorController : Controller
    {
        public IActionResult NewTutorInformatinView()
        {
            return View();
        }
    }
}
