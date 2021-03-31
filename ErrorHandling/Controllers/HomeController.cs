using ErrorHandling.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ErrorHandling.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            //throw new Exception("Database baglantisinda bir hata meydana geldi.");
            int value1 = 5;
            int value2 = 0;

            int result = value1 / value2;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [AllowAnonymous] // Authorization yapisindan etkilenmesin, tum kullanicilar gorsun diye. herkes hata sayfasini gorebilsin diye.
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)] // cache 0, lokasyona göre veri yok, aynı zamanda hic bir cache'i kaydetmiyor.
        public IActionResult Error()
        {
            // RequestId o anki aktivitenin guncel id'sini verir. eger onu bulamazsa null olursa ?? isaretinden sonraki kisim calisir.
            // ilk ifade null olursa burdaki (HttpContext.TraceIdentifier) data alinir.
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
