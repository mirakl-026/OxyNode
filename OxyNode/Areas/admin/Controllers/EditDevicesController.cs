using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// все действия над газоанализаторами

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditDevicesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
