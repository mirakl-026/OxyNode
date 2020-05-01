using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.ViewModels;
using OxyNode.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Bson;


namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditNotesController : Controller
    {
        private KB_noteService _db;

        public EditNotesController(KB_noteService context)
        {
            _db = context;
        }

        public IActionResult ReadAllNotes()
        {
            return View();
        }

        #region CRUD
        // 
        public IActionResult CreateNote()
        {
            return View();
        }

        public IActionResult ReadNote()
        {
            return View();
        }

        public IActionResult UpdateNote()
        {
            return View();
        }

        public IActionResult DeleteNote()
        {
            return RedirectToAction("Index", "Panel");
        }
        #endregion

    }
}