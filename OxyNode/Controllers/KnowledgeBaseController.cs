﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OxyNode.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}