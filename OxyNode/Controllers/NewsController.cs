using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.Models;
using OxyNode.ViewModels;


namespace OxyNode.Controllers
{
    // Контроллер новостей
    public class NewsController : Controller
    {
        private INewsService _db;
        public int pageSize = 6;

        public NewsController (INewsService context)
        {
            _db = context;
        }

        // главная страница новостей
        public async Task<IActionResult> Index()
        {
            // Вьюмодель для статей
            NewsItemsViewModel nivm = new NewsItemsViewModel();

            // передача во вьюмодель - общего ко-ва новостей
            nivm.newsItemsCount = await _db.GetNewsItemsCount();

            // номер текущей страницы
            nivm.currentPageNumber = 1;

            // сами новости
            nivm.newsItems = await _db.GetPageOfNewsItems(1, pageSize);

            return View(nivm);
        }

        // страница новостей pageSize
        public async Task<IActionResult> Page(int pageNumber)
        {
            // Вьюмодель для статей
            NewsItemsViewModel nivm = new NewsItemsViewModel();

            // передача во вьюмодель - общего ко-ва новостей
            nivm.newsItemsCount = await _db.GetNewsItemsCount();

            // номер текущей страницы
            nivm.currentPageNumber = 1;

            // сами новости
            nivm.newsItems = await _db.GetPageOfNewsItems(pageNumber, pageSize);

            return View(nivm);
        }


        // страница одной новости
        public async Task<IActionResult> ReadNews(string id)
        {
            NewsItem ni = await _db.ReadNewsItem(id);
            return View(ni);
        }


    }
}
