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
    }
}