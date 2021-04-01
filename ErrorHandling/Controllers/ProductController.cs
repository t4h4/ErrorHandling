using ErrorHandling.Filter;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandling.Controllers
{
    [CustomHandleExceptionFilterAttribute(ErrorPage = "hata2")]
    public class ProductController : Controller
    {
        public IActionResult Index()
        {
            throw new Exception("hata meydana geldi bro");
            return View();
        }
    }
}
