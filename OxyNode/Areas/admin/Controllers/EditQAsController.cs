using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Models;
using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditQAsController : Controller
    {
        private IKB_QAService _db;

        public EditQAsController(IKB_QAService context)
        {
            _db = context;
        }


        // вывести список всех QA
        [HttpGet]
        public async Task<IActionResult> ReadAllQAs()
        {
            QAsViewModel qavm = new QAsViewModel();
            qavm.qas = await _db.GetAllQAs();
            qavm.currentPageNumber = 1;
            qavm.qaCount = await _db.GetQAsCount();

            return View(qavm);
        }

        // посмотреть конкретный QA
        [HttpGet]
        public async Task<IActionResult> ReadOneQA(string qaId)
        {
            var qa = await _db.ReadQA(qaId);

            return View(qa);
        }

        // редактировать QA
        [HttpGet]
        public async Task<IActionResult> EditOneQA(string qaId)
        {
            var qa = await _db.ReadQA(qaId);
            return View(qa);
        }

        [HttpPost]
        public async Task<IActionResult> EditOneQA(KB_QA nqa)
        {
            if (ModelState.IsValid)
            {
                await _db.UpdateQA(nqa);
                return RedirectToAction("ReadAllQAs", "EditQAs");
            }
            return View(nqa);
        }

        // ответить на вопрос
        [HttpGet]
        public async Task<IActionResult> AnsToQuestion(string qaId)
        {
            var q = await _db.ReadQA(qaId);
            return View(q);
        }

        [HttpPost]
        public async Task<IActionResult> AnsToQuestion(KB_QA answer)
        {
            if (ModelState.IsValid)
            {
                await _db.UpdateQA(answer);

                return RedirectToAction("ReadAllQAs", "EditQAs");
            }
            return View(answer);
        }


        // удалить QA
        [HttpGet]
        public async Task<IActionResult> DeleteQA(string qaId)
        {
            await _db.DeleteQA(qaId);
            return RedirectToAction("ReadAllQAs", "EditQAs");
        }

        // установить флаг разрешения на отображение ответа-вопроса на главной странице
        [HttpPost]
        public async Task<IActionResult> PublishQA(string qaId)
        {
            var qa = await _db.ReadQA(qaId);
            qa.publishToSite = true;
            await _db.UpdateQA(qa);
            //return RedirectToAction("ReadOneQA", "EditQAs", qaId);
            return new JsonResult(qa.publishToSite);
        }

        // снять флаг разрешения на отображение ответа-вопроса на главной странице
        [HttpPost]
        public async Task<IActionResult> HideQA(string qaId)
        {
            var qa = await _db.ReadQA(qaId);
            qa.publishToSite = false;
            await _db.UpdateQA(qa);
            //return RedirectToAction("ReadOneQA", "EditQAs", qaId);
            return new JsonResult(qa.publishToSite);
        }


        // удалить все ответы
        [HttpGet]
        public async Task<IActionResult> DeleteAllQAs()
        {
            await _db.DeleteAllQAs();
            return RedirectToAction("ReadAllQAs", "EditQAs");
        }
    }
}
