using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Quran.Business;
using Quran.Models;

namespace Quran.Controllers
{
    public class BooksController : Controller
    {
        public IActionResult AllBooks()
        {
            List<BookDO> list = new AdminBA().GetAllBooks();
            return View(list);
        }
    }
}
