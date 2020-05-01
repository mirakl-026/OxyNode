using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}")]
    public class NotesController : Controller
    {
        // Статьи
        public IActionResult Index()
        {
            return View();
        }
    }
}