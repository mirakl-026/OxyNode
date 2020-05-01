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
            
            // текущая страница - 1
            nvm.currentPageNumber = 1;

            // разные страницы
            long notesCount = await _db.GetNotesCount();

            long pageStep = 1;
            int pageCounter = 0;
            do
            {
                var stepResult = notesCount - (pageStep * pageSize);
                if (stepResult > -6)
                {
                    pageStep++;
                    pageCounter++;
                    nvm.pagesNumbers.Add(pageCounter);
                    if (nvm.pagesNumbers.Count > 5)
                    {
                        break;
                    }
                }
                else
                {
                    break;
                }
            }
            while (true);

            // сами статьи
            nvm.notes = await _db.GetPageOfNotes(1, pageSize);

            return View(nvm);
        }
    }
}