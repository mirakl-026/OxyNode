using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.ViewModels;

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}")]
    public class IndustrySolutionsController : Controller
    {
        private KB_industrySolutionService _db;

        public IndustrySolutionsController(KB_industrySolutionService context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            IndustrySolutionsViewModel isvm = new IndustrySolutionsViewModel();
            isvm.industrySolutions = await _db.GetAllIndustrySolutions();
            return View(isvm);
        }
    }
}