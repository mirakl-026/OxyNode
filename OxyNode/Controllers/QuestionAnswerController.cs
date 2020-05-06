using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OxyNode.Models;
using OxyNode.Services;
using OxyNode.ViewModels;

// Контроллер страницы - База Знаний - Вопрос-Ответ

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}/{action}")]
    public class QuestionAnswerController : Controller
    {
        private KB_answerService _dbA;
        private KB_questionService _dbQ;
        private int pageSize = 6;
        public QuestionAnswerController(KB_answerService contextA, KB_questionService contextQ)
        {
            _dbA = contextA;
            _dbQ = contextQ;
        }

        // Index - основная страница Вопросов-ответов (первая страница из БД)
        public async Task<IActionResult> Index()
        {
            // вьюмодель для передачи в представления
            AnswersViewModel avm = new AnswersViewModel();

            // передача во вьюмодель общего кол-ва ответов на вопросы
            avm.answersCount = await _dbA.GetAnswersCount();

            // номер текущей страницы
            avm.currentPageNumber = 1;

            // сами ответы
            avm.answers = await _dbA.GetPageOfAnswers(1, pageSize);

            return View(avm);
        }

        // конкретная страница с несколькими вопросами-ответами
        public async Task<IActionResult> Page(int pageNumber)
        {
            AnswersViewModel avm = new AnswersViewModel();
            avm.answersCount = await _dbA.GetAnswersCount();

            avm.currentPageNumber = pageNumber;

            avm.answers = await _dbA.GetPageOfAnswers(pageNumber, pageSize);

            return View();
        }

        // конкретная страница вопроса - ответа
        public async Task<IActionResult> ReadQA(string id)
        {
            var answer = await _dbA.ReadAnswer(id);
            return View(answer);
        }

        // форма задания вопроса
        [HttpGet]
        public IActionResult AskQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(KB_question question)
        {
            if(ModelState.IsValid)
            {
                // если форма заполнениа правильно
                // добавить вопрос в коллекцию вопросов
                await _dbQ.CreateQuestion(question);

                return RedirectToAction("Index", "QuestionAnswer");
            }           
            return View(question);
        }


    }
}