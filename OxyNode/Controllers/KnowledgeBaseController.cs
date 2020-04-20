using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;

namespace OxyNode.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private KnowledgeBaseService _db;

        public KnowledgeBaseController(KnowledgeBaseService context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            var kb = await _db.ReadKnowledgeBase();
            return View(kb);
        }
    }
}