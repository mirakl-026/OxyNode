using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;
using OxyNode.Models;


namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditNewsController : Controller
    {
        private INewsService _db;

        public EditNewsController(INewsService context)
        {
            _db = context;
        }

        // получить все новости
        public async Task<IActionResult> ReadAllNews()
        {
            // вьюмодель
            NewsItemsViewModel nivm = new NewsItemsViewModel();
            nivm.newsItems = await _db.GetAllNewsItems();

            nivm.currentPageNumber = 1;

            nivm.newsItemsCount = await _db.GetNewsItemsCount();

            return View(nivm);
        }


        #region CRUD
        // Create - создать новость
        [HttpGet]
        public IActionResult CreateNews()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateNews(NewsItem ni)
        {
            if (ModelState.IsValid)
            {
                await _db.CreateNewsItem(ni);
                return RedirectToAction("ReadAllNews", "EditNews");
            }
            return View(ni);
        }

        // Read - страница новости
        [HttpGet]
        public async Task<IActionResult> ReadNews(string id)
        {
            var ni = await _db.ReadNewsItem(id);
            return View(ni);
        }

        // Update - изменить новость
        [HttpGet]
        public async Task<IActionResult> UpdateNews(string id)
        {
            var niToUpdate = await _db.ReadNewsItem(id);
            return View(niToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateNews(NewsItem updatedNi)
        {
            if (ModelState.IsValid)
            {
                await _db.UpdateNewsItem(updatedNi);
                return RedirectToAction("ReadAllNews", "EditNews");
            }
            return View(updatedNi);
        }

        // Delete
        [HttpGet]
        public async Task<IActionResult> DeleteNews(string id)
        {
            await _db.DeleteNewsItem(id);
            return RedirectToAction("ReadAllNews", "EditNews");
        }
        #endregion

        // удалить все новости
        [HttpGet]
        public async Task<IActionResult> DeleteAllNews ()
        {
            await _db.DeleteAllNewsItems();
            return RedirectToAction("ReadAllNews", "EditNews");
        }

    }
}
