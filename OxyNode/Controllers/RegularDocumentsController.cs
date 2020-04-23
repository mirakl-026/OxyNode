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
    public class RegularDocumentsController : Controller
    {
        private KB_regularDocumentService _db;

        public RegularDocumentsController(KB_regularDocumentService context)
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