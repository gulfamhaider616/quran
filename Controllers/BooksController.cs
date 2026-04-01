using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace LearnFreeQuran.Web.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public async Task<IActionResult> AllBooks()
        {
            List<BookDO> list = await new AdminBA().GetAllBooksAsync();
            return View(list);
        }
    }
}