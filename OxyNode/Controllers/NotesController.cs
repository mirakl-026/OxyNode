using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.ViewModels;

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}/{action}")]
    public class NotesController : Controller
    {
        private KB_noteService _db;
        private int pageSize = 6;

        public NotesController(KB_noteService context)
        {
            _db = context;
        }
        

        // Статьи, Index - равносильно запросу 1-страницы статей - возможно неправильно, и надо сделать на клиенте!
        public async Task<IActionResult> Index()
        {
            // Вьюмодель для статей
            NotesViewModel nvm = new NotesViewModel();

            // передача во вьюмодель - общего ко-ва статей
            long notesCount = await _db.GetNotesCount();
            nvm.notesCount = notesCount;

            // номер текущей страницы
            nvm.currentPageNumber = 1;

            // сами статьи
            nvm.notes = await _db.GetPageOfNotes(1, pageSize);

            return View(nvm);
        }

        // конкретная страница новостей
        public async Task<IActionResult> Page(int pageNumber)
        {
            // Вьюмодель для статей
            NotesViewModel nvm = new NotesViewModel();

            // передача во вьюмодель - общего ко-ва статей
            long notesCount = await _db.GetNotesCount();
            nvm.notesCount = notesCount;

            // номер текущей страницы
            nvm.currentPageNumber = pageNumber;

            // сами статьи
            nvm.notes = await _db.GetPageOfNotes(pageNumber, pageSize);

            return View(nvm);
        }

        // конкретная новость
        public async Task<IActionResult> ReadNote(string id)
        {
            var note = await _db.ReadNote(id);
            return View(note);
        }
    }
}