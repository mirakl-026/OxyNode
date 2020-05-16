using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.ViewModels;
using OxyNode.Models;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditQuestionsController : Controller
    {
        private KB_questionService _dbQ;
        private KB_answerService _dbA;

        public EditQuestionsController(KB_questionService contextQ, KB_answerService contextA)
        {
            _dbQ = contextQ;
            _dbA = contextA;
        }

        // вывести список всех вопросов
        [HttpGet]
        public async Task<IActionResult> GetAllQuestions()
        {
            QuestionsViewModel qvm = new QuestionsViewModel();
            qvm.questions = await _dbQ.GetAllQuestions();
            qvm.currentPageNumber = 1;
            qvm.questionsCount = await _dbQ.GetQuestionsCount();

            return View(qvm);
        }

        // ответить на вопрос
        [HttpGet]
        public async Task<IActionResult> AnswerToQuestion(string questionId)
        {
            var q = await _dbQ.ReadQuestion(questionId);
            return View(q);
        }

        [HttpPost]
        public async Task<IActionResult> AnswerToQuestion(string questionId, KB_answer answer)
        {
            if (ModelState.IsValid)
            {
                answer.QuestionId = questionId;
                await _dbA.CreateAnswer(answer);

                var q = await _dbQ.ReadQuestion(questionId);
                q.AnswerId = answer.Id;

                return RedirectToAction("Index", "Panel");
            }
            return View(answer);
        }

        // -- CRUD
        // создать вопрос

        // посмотреть вопрос
        [HttpGet]
        public async Task<IActionResult> ReadOneQuestion (string questionId)
        {
            var q = await _dbQ.ReadQuestion(questionId);
            return View(q);
        }

        // удалить вопрос

        // редактировать вопрос

    }
}