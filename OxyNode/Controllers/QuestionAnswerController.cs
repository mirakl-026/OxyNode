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
            QAViewModel qavm = new QAViewModel();

            // передача во вьюмодель общего кол-ва ответов на вопросы
            qavm.qaCount = await _dbA.GetPublisedAnswersCount();

            // номер текущей страницы
            qavm.currentPageNumber = 1;

            // сами вопрос-ответы
            qavm.answers = await _dbA.GetPageOfPublishedAnswers(1, pageSize);
            qavm.questions = new List<KB_question>();
            foreach (var ans in qavm.answers)
            {
                var q = await _dbQ.ReadQuestion(ans.QuestionId);
                qavm.questions.Add(q);
            }

            return View(qavm);
        }

        // конкретная страница с несколькими вопросами-ответами
        public async Task<IActionResult> Page(int pageNumber)
        {
            // вьюмодель для передачи в представления
            QAViewModel qavm = new QAViewModel();

            // передача во вьюмодель общего кол-ва ответов на вопросы
            qavm.qaCount = await _dbA.GetPublisedAnswersCount();

            // номер текущей страницы
            qavm.currentPageNumber = pageNumber;

            // сами вопрос-ответы
            qavm.answers = await _dbA.GetPageOfPublishedAnswers(pageNumber, pageSize);
            foreach (var ans in qavm.answers)
            {
                var q = await _dbQ.ReadQuestion(ans.QuestionId);
                qavm.questions.Add(q);
            }

            return View(qavm);
        }


        // конкретная страница вопроса - ответа
        public async Task<IActionResult> ReadQA(string answerId, string questionId)
        {
            OneQAViewModel oqavm = new OneQAViewModel();
            oqavm.answer = await _dbA.ReadAnswer(answerId);
            oqavm.question = await _dbQ.ReadQuestion(questionId);

            return View(oqavm);
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