using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// Контроллер страницы - База Знаний - Вопрос-Ответ

namespace OxyNode.Controllers
{
    [Route("KnowledgeBase/{controller}/{action}")]
    public class QuestionAnswerController : Controller
    {

        // Index - основная страница Вопросов-ответов (первая страница из БД)
        public IActionResult Index()
        {
            return View();
        }

        // конкретная страница с несколькими вопросами-ответами
        public IActionResult Page()
        {
            return View();
        }

        // конкретная страница вопроса - ответа
        public IActionResult ReadQA()
        {
            return View();
        }
    }
}