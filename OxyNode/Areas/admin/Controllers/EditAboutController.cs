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
    public class EditAboutController : Controller
    {
        // контроллер для редактирования содржимого страницы "О нас"
        private AboutService _db_about;
        private AboutSertificateService _db_aboutSertificate;

        public EditAboutController(AboutService aboutContext, AboutSertificateService aboutSertificateContext)
        {
            _db_about = aboutContext;
            _db_aboutSertificate = aboutSertificateContext;
        }

        #region Edit "About"
        // CREATE/ создать страницу "О нас"
        [HttpGet]
        public IActionResult CreateAbout()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAbout(About avm)
        {
            if (ModelState.IsValid)
            {
                await _db_about.CreateAbout(avm);                
                return RedirectToAction("Index", "Panel");
            }
            return View(avm);
        }

        // READ/ показать страницу "О нас"
        public async Task<IActionResult> ReadAbout()
        {
            AboutViewModel avm = new AboutViewModel();

            avm.VM_About = await _db_about.ReadAbout();
            avm.VM_AboutSertificates = await _db_aboutSertificate.GetAllAboutSertificates();

            return View(avm);
        }

        // UPDATE/ редактировать страницу "О нас"
        [HttpGet]
        public async Task<IActionResult> UpdateAbout()
        {
            var a = await _db_about.ReadAbout();
            return View(a);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAbout(About a)
        {
            if (ModelState.IsValid)
            {
                await _db_about.UpdateAbout(a);
                return RedirectToAction("Index", "Panel");
            }
            return View(a);
        }


        // DELETE/ удалить содержимое страницы "О нас"
        public async Task<IActionResult> DeleteAbout()
        {
            await _db_about.DeleteAbout();
            return RedirectToAction("Index", "Panel");
        }

        #endregion

    }
}