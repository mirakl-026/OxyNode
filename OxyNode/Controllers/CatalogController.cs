using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.Infrastructure.Aditional;
using OxyNode.Services;
using OxyNode.Models;
using OxyNode.ViewModels;


namespace OxyNode.Controllers
{
    public class CatalogController : Controller
    {
        private IGasAnalyzerService _db;
        private int pageSize = 6;

        public CatalogController(IGasAnalyzerService context)
        {
            _db = context;
        }


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var gasAnalyzers_vm = new GasAnalyzersViewModel();
            gasAnalyzers_vm.gasAnalyzers = await _db.GetAllGasAnalyzers();
            return View(gasAnalyzers_vm);
        }
    }
}
