using LearnFreeQuran.Business;
using LearnFreeQuran.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearnFreeQuran.Web.Controllers
{
    public class BooksController : Controller
    {
        // GET: Books
        public ActionResult AllBooks()
        {

            List<BookDO> list = new AdminBA().GetAllBooks();
            return View(list);
        }
    }
}