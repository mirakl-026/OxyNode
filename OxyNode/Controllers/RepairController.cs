using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Контроллер -> услуги | ремонт
namespace OxyNode.Controllers
{
    [Route("Actions/{controller}/{action}")]
    public class RepairController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
