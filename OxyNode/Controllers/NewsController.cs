using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


namespace OxyNode.Controllers
{
    // Контроллер новостей
    public class NewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
