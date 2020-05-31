using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;

namespace OxyNode.Controllers
{
    public class KnowledgeBaseController : Controller
    {
        private IKnowledgeBaseService _db;

        public KnowledgeBaseController(IKnowledgeBaseService context)
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