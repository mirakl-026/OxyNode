﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class PanelController : Controller
    {
        // основная страница панели
        public IActionResult Index()
        {
            return View();
        }
    }
}