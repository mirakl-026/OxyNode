﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.Models;
using OxyNode.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditKnowledgeBaseController : Controller
    {
        // контроллер для редактирования основной страницы - "База знаний"

        // private string KbPath = "/resources/knowledgeBase/";
        // private string Kb_rd_Path = "/resources/knowledgeBase/regularDocuments/";
        // private string Kb_is_Path = "/resources/knowledgeBase/industrySolutions/";
        // private string Kb_qa_Path = "/resources/knowledgeBase/quesAns/";
        // private string Kb_nt_Path = "/resources/knowledgeBase/notes/";
        //
        // private IWebHostEnvironment _appEnvironment;
        private KnowledgeBaseService _db_kb;
        
        public EditKnowledgeBaseController(KnowledgeBaseService kbContext)
        {
            
            _db_kb = kbContext;        
        }


        #region Edit KnowledgeBase - main
        // CREATE/ создать страницу "База знаний"
        [HttpGet]
        public IActionResult CreateKnowledgeBase()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateKnowledgeBase(KnowledgeBase kb)
        {
            if (ModelState.IsValid)
            {
                await _db_kb.CreateKnowledgeBase(kb);
                return RedirectToAction("Index", "Panel");
            }
            return View(kb);
        }

        // READ/ показать страницу "База знаний"
        public async Task<IActionResult> ReadKnowledgeBase()
        {
            var kb = await _db_kb.ReadKnowledgeBase();
            return View(kb);
        }

        // UPDATE/ редактировать страницу "База знаний"
        [HttpGet]
        public async Task<IActionResult> UpdateKnowledgeBase()
        {
            var kb = await _db_kb.ReadKnowledgeBase();
            return View(kb);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateKnowledgeBase(KnowledgeBase kb)
        {
            if (ModelState.IsValid)
            {
                await _db_kb.UpdateKnowledgeBase(kb);
                return RedirectToAction("Index", "Panel");
            }
            return View(kb);
        }


        // DELETE/ удалить содержимое страницы "База знаний"
        public async Task<IActionResult> DeleteKnowledgeBase()
        {
            await _db_kb.DeleteKnowledgeBase();
            return RedirectToAction("Index", "Panel");
        }
        #endregion



    }
}