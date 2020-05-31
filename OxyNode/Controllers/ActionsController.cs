using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Контроллер роутинга "Услуги"
namespace OxyNode.Controllers
{
    public class ActionsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
