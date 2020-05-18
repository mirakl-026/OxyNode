using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.Models;
using OxyNode.ViewModels;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditAnswersController : Controller
    {
        private KB_answerService _dbA;
        private KB_questionService _dbQ;

        public EditAnswersController(KB_answerService contextA, KB_questionService contextQ)
        {
            _dbA = contextA;
            _dbQ = contextQ;
        }

        // вывести список всех ответов
        [HttpGet]
        public async Task<IActionResult> ReadAllAnswers()
        {
            AnswersViewModel avm = new AnswersViewModel();
            avm.answers = await _dbA.GetAllAnswers();
            avm.currentPageNumber = 1;
            avm.answersCount = await _dbA.GetAnswersCount();

            return View(avm);
        }

        // посмотреть конкретный ответ
        [HttpGet]
        public async Task<IActionResult> ReadOneAnswer(string answerId)
        {
            var a = await _dbA.ReadAnswer(answerId);

            var q = await _dbQ.ReadQuestion(a.QuestionId);
            ViewData["questionId"] = q.Id;
            ViewData["questionFullName"] = q.FullName;
            ViewData["questionAddress"] = q.Address;
            ViewData["questionEmail"] = q.e_mail;
            ViewData["questionText"] = q.questionText;

            return View(a);
        }

        // редактировать ответ

        // удалить ответ
        [HttpGet]
        public async Task<IActionResult> DeleteAnswer(string answerId)
        {
            var ans = await _dbA.ReadAnswer(answerId);
            if (ans != null)
            {
                // при удалении ответа - нужно отчистиь id ответа в поле вопроса
                var q = await _dbQ.ReadQuestion(ans.QuestionId);
                q.AnswerId = null;
                await _dbQ.UpdateQuestion(q);

                await _dbA.DeleteAnswer(answerId);
            }
            return RedirectToAction("Index", "Panel");
        }

        // установить флаг разрешения на отображение ответа-вопроса на главной странице
        [HttpPost]
        public async Task<IActionResult> PublishQuestionAnswer (string answerId)
        {
            var a = await _dbA.ReadAnswer(answerId);
            a.publishToSite = true;
            await _dbA.UpdateAnswer(a);
            //return RedirectToAction("ReadOneAnswer", "EditAnswers", answerId);
            return new JsonResult(a.publishToSite);
        }

        // снять флаг разрешения на отображение ответа-вопроса на главной странице
        [HttpPost]
        public async Task<IActionResult> HideQuestionAnswer (string answerId)
        {
            var a = await _dbA.ReadAnswer(answerId);
            a.publishToSite = false;
            await _dbA.UpdateAnswer(a);
            //return RedirectToAction("ReadOneAnswer", "EditAnswers", answerId);
            return new JsonResult(a.publishToSite);
        }
    }
}