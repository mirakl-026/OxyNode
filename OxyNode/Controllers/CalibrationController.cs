using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// контроллер -> услуги | калибровка
namespace OxyNode.Controllers
{
    public class CalibrationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
