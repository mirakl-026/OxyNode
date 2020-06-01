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
    public class RegularDocumentsController : Controller
    {
        private IKB_regularDocumentService _db;

        public RegularDocumentsController(IKB_regularDocumentService context)
        {
            _db = context;
        }

        public async Task<IActionResult> Index()
        {
            RegularDocumentsViewModel rdvm = new RegularDocumentsViewModel();
            rdvm.regularDocuments = await _db.GetAllRegularDocuments();
            return View(rdvm);
        }
    }
}