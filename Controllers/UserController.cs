using LearnFreeQuran.Library;
using LearnFreeQuran.Business;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace LearnFreeQuran.Web.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public IActionResult Registration()
        {
            return View();
        }

        public async Task<IActionResult> SaveUser(string name, string email, string password)
        {
            UserDO user = new UserDO();
            user.Name = name;
            user.Email = email;
            user.Password = password;

            bool result = await new UserBA().SaveUserAsync(user);

            if (result)
            {
                return View("UserLogin");
            }
            else
            {
                return View("Registration");
            }
        }

        public IActionResult UserLogin()
        {
            return View();
        }

        public async Task<IActionResult> VerifyUser(string email, string password)
        {
            var user = await new UserBA().VerifyUserAsync(email, password);

            if (user != null)
            {
                HttpContext.Session.SetString("name", user.Name ?? "");
                HttpContext.Session.SetString("Email", user.Email ?? "");
                HttpContext.Session.SetString("Bookmarkid", "#" + user.BookmarkId);
                HttpContext.Session.SetString("Chapterid", user.ChapterID);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("UserLogin");
            }
        }

        public async Task<IActionResult> GetUserProfile(int userID)
        {
            return View(await new UserBA().GetUserProfileAsync(userID));
        }

        public async Task<JsonResult> Addookmark(string id)
        {
            string url = $"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}";

            if (HttpContext.Session.GetString("Email") != null)
            {
                UserDO user = new UserDO();
                user.BookmarkId = id;
                user.Email = HttpContext.Session.GetString("Email");

                bool result = await new UserBA().AddookmarkAsync(user);

                if (result)
                {
                    HttpContext.Session.SetString("Bookmarkid", "#" + id);
                    HttpContext.Session.SetString("Chapterid", id.Split('-')[0]);
                }

                url = url.Split('U')[0] + "quran_reading?ChapterID=" + id.Split('-')[0] + "#" + id;
                return Json(url);
            }
            else
            {
                url = url.Split('U')[0] + "User/UserLogin";
                return Json(url);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.SetString("name", "");
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}