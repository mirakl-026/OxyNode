using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}")]
    public class IndustrySolutionsController : Controller
    {
        private IKB_industrySolutionService _db;

        public IndustrySolutionsController(IKB_industrySolutionService context)
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