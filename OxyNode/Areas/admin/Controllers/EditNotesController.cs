using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;
using OxyNode.Models;


namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditNotesController : Controller
    {
        private IKB_noteService _db;

        public EditNotesController(IKB_noteService context)
        {
            _db = context;
        }

        public async Task<IActionResult> ReadAllNotes()
        {
            // Вьюмодель для статей
            NotesViewModel nvm = new NotesViewModel();

            // передача во вьюмодель - общего ко-ва статей
            long notesCount = await _db.GetNotesCount();
            nvm.notesCount = notesCount;

            // номер текущей страницы
            nvm.currentPageNumber = 1;

            // сами статьи
            nvm.notes = await _db.GetAllNotes();

            return View(nvm);
        }

        #region CRUD
        // Создать статью
        [HttpGet]
        public IActionResult CreateNote()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNote(KB_note newNote)
        {
            if (ModelState.IsValid)
            {
                await _db.CreateNote(newNote);
                return RedirectToAction("Index", "Panel");
            }
            return View(newNote);
        }


        // считать статью 
        [HttpGet]
        public async Task<IActionResult> ReadNote(string id)
        {
            var note = await _db.ReadNote(id);
            return View(note);
        }


        // обновить статью
        [HttpGet]
        public async Task<IActionResult> UpdateNote(string id)
        {
            var noteToUpdate = await _db.ReadNote(id);
            return View(noteToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNote(KB_note updatedNote)
        {
            if(ModelState.IsValid)
            {
                await _db.UpdateNote(updatedNote);
                return RedirectToAction("Index", "Panel");
            }
            return View(updatedNote);
        }


        // удалить статью
        [HttpGet]
        public async Task<IActionResult> DeleteNote(string id)
        {
            await _db.DeleteNote(id);
            return RedirectToAction("Index", "Panel");
        }
        #endregion

    }
}