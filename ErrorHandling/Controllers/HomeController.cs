using ErrorHandling.Filter;
using ErrorHandling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandling.Controllers
{
    [CustomHandleExceptionFilterAttribute(ErrorPage = "hata1")] // bu controller seviyesinde hata olursa bu sayfayı bas. 
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [CustomHandleExceptionFilterAttribute(ErrorPage = "hata1")] //asagidaki method'da hata meydana gelirse OnException method tetiklenecek.
        public IActionResult Index()
        {
            //throw new Exception("Database baglantisinda bir hata meydana geldi.");
            int value1 = 5;
            int value2 = 0;

            int result = value1 / value2;

            return View();
        }

        [CustomHandleExceptionFilterAttribute(ErrorPage = "hata2")]
        public IActionResult Privacy()
        {
            throw new FileNotFoundException();
            return View();
        }

        [AllowAnonymous] // Authorization yapisindan etkilenmesin, tum kullanicilar gorsun diye. herkes hata sayfasini gorebilsin diye.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // cache 0, lokasyona göre veri yok, aynı zamanda hic bir cache'i kaydetmiyor.
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerFeature>(); //uygulamanin herhangi bir yerinden gelen hata yakalandi.

            ViewBag.path = "hata_yolu";
            ViewBag.message = exception.Error.Message;

            return View();
        }

        public IActionResult Hata1()
        {
            return View();
        }

        public IActionResult Hata2()
        {
            return View();
        }
    }
}
