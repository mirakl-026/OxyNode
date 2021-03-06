﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}/{action}")]
    public class NotesController : Controller
    {
        private IKB_noteService _db;
        private int pageSize = 6;

        public NotesController(IKB_noteService context)
        {
            _db = context;
        }
        

        // Статьи, Index - равносильно запросу 1-страницы статей - возможно неправильно, и надо сделать на клиенте!
        public async Task<IActionResult> Index()
        {
            // Вьюмодель для статей
            NotesViewModel nvm = new NotesViewModel();

            // передача во вьюмодель - общего ко-ва статей
            nvm.notesCount = await _db.GetNotesCount();

            // номер текущей страницы
            nvm.currentPageNumber = 1;

            // сами статьи
            nvm.notes = await _db.GetPageOfNotes(1, pageSize);

            return View(nvm);
        }

        // конкретная страница статей
        public async Task<IActionResult> Page(int pageNumber)
        {
            // Вьюмодель для статей
            NotesViewModel nvm = new NotesViewModel();

            // передача во вьюмодель - общего ко-ва статей
            nvm.notesCount = await _db.GetNotesCount();

            // номер текущей страницы
            nvm.currentPageNumber = pageNumber;

            // сами статьи
            nvm.notes = await _db.GetPageOfNotes(pageNumber, pageSize);

            return View(nvm);
        }

        // конкретная статья
        public async Task<IActionResult> ReadNote(string id)
        {
            var note = await _db.ReadNote(id);
            return View(note);
        }
    }
}