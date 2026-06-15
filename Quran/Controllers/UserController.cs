using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Registration()
        {
            return View();
        }

        public IActionResult SaveUser(string name, string email, string password)
        {
            UserDO user = new UserDO();
            user.Name = name;
            user.Email = email;
            user.Password = password;
            bool result = new UserBA().SaveUser(user);
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

        public IActionResult VerifyUser(string email, string password)
        {
            var user = new UserBA().VerifyUser(email, password);
            if (user != null)
            {
                HttpContext.Session.SetString("name", user.Name ?? "");
                HttpContext.Session.SetString("Email", user.Email ?? "");
                HttpContext.Session.SetString("Bookmarkid", "#" + user.BookmarkId);
                HttpContext.Session.SetString("Chapterid", user.ChapterID ?? "");
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View("UserLogin");
            }
        }

        public IActionResult GetUserProfile(int userID)
        {
            return View(new UserBA().GetUserProfile(userID));
        }

        public JsonResult Addookmark(string id)
        {
            string baseUrl = $"{Request.Scheme}://{Request.Host}/";
            string url;
            var email = HttpContext.Session.GetString("Email");
            if (email != null)
            {
                UserDO user = new UserDO();
                user.BookmarkId = id;
                user.Email = email;
                bool result = new UserBA().Addookmark(user);
                if (result)
                {
                    HttpContext.Session.SetString("Bookmarkid", "#" + id);
                    HttpContext.Session.SetString("Chapterid", id.Split('-')[0]);
                }
                url = baseUrl + "quran_reading?ChapterID=" + id.Split('-')[0] + "#" + id;
                return Json(url);
            }
            else
            {
                url = baseUrl + "User/UserLogin";
                return Json(url);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
    }
}
