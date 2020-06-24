using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using OxyNode.Models;
using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;

// Контроллер страницы - База Знаний - Вопрос-Ответ

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}/{action}")]
    public class QuestionAnswerController : Controller
    {
        private IKB_QAService _db;
        private int pageSize = 6;
        public QuestionAnswerController(IKB_QAService context)
        {
            _db = context;
        }


        // Index - основная страница Вопросов-ответов (первая страница из БД)
        public async Task<IActionResult> Index()
        {
            // вьюмодель для передачи в представления
            QAsViewModel qavm = new QAsViewModel();

            // передача во вьюмодель общего кол-ва ответов на вопросы
            qavm.qaCount = await _db.GetPublisedQAsCount();

            // номер текущей страницы
            qavm.currentPageNumber = 1;

            // сами вопрос-ответы
            qavm.qas = await _db.GetPageOfPublishedQAs(1, pageSize);

            return View(qavm);
        }


        // конкретная страница с несколькими вопросами-ответами
        public async Task<IActionResult> Page(int pageNumber)
        {
            // вьюмодель для передачи в представления
            QAsViewModel qavm = new QAsViewModel();

            // передача во вьюмодель общего кол-ва ответов на вопросы
            qavm.qaCount = await _db.GetPublisedQAsCount();

            // номер текущей страницы
            qavm.currentPageNumber = pageNumber;

            // сами вопрос-ответы
            qavm.qas = await _db.GetPageOfPublishedQAs(pageNumber, pageSize);

            return View(qavm);
        }


        // конкретная страница вопроса - ответа
        public async Task<IActionResult> ReadQA(string qaId)
        {
            OneQAViewModel oqavm = new OneQAViewModel();
            oqavm.qa = await _db.ReadQA(qaId);
            return View(oqavm);
        }

        // форма задания вопроса
        [HttpGet]
        public IActionResult AskQuestion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AskQuestion(KB_QA question)
        {
            if(ModelState.IsValid)
            {
                // если форма заполнениа правильно
                // добавить вопрос в коллекцию вопросов
                await _db.CreateQA(question);

                return RedirectToAction("Index", "QuestionAnswer");
            }           
            return View(question);
        }
    }
}