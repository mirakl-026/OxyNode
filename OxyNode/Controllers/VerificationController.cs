using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// контроллер -> услуги | поверка
namespace OxyNode.Controllers
{
    [Route("Actions/{controller}/{action}")]
    public class VerificationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
