using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;


using OxyNode.Infrastructure.Interfaces;
using OxyNode.Infrastructure.Interfaces.FileSystem;
using OxyNode.Models;
using OxyNode.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditAboutController : Controller
    {
        // контроллер для редактирования содержимого страницы "О нас"

        private IAboutService _db_about;
        private IAboutSertificateService _db_aboutSertificate;
        private IFileAboutSertificateService _fsContext;

        public EditAboutController(IAboutService aboutContext, IAboutSertificateService aboutSertificateContext, IFileAboutSertificateService fsContext)
        {
            _db_about = aboutContext;
            _db_aboutSertificate = aboutSertificateContext;
            _fsContext = fsContext;
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


        #region Edit About-Sertificates

        // загрузить файл сертификата на сервер
        [HttpGet]
        public IActionResult AddAboutSertificate()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> AddAboutSertificate(string sertificateLabel, IFormFile uploadedSertificate)
        {
            if (uploadedSertificate != null)
            {
                // Определение пути 
                string path = _fsContext.GetFSAboutSertificatePath() + uploadedSertificate.FileName;

                // Сохранение файла на сервере
                await _fsContext.AddAboutSertificate(uploadedSertificate);

                // Сохранение в БД
                AboutSertificate newSertificate = new AboutSertificate()
                {
                    SertificateLabel = sertificateLabel,
                    SertificateFileName = uploadedSertificate.FileName,
                    SertificatePath = path
                };

                await _db_aboutSertificate.CreateAboutSertificate(newSertificate);

            }
            return RedirectToAction("Index", "Panel");
        }

        // показать файлы сертификатов на сервере
        [HttpGet]
        public async Task<IActionResult> ReadAllAboutSertificate()
        {
            var sertificates = await _db_aboutSertificate.GetAllAboutSertificates();
            return View(sertificates);
        }


        // показать файл сертификат на сервере 
        [HttpGet]
        public async Task<IActionResult> ReadAboutSertificate(string id)
        {
            var sertificate = await _db_aboutSertificate.ReadAboutSertificate(id);
            return View(sertificate);
        }


        // удалить файл сертификата с сервера
        [HttpGet]
        public async Task<IActionResult> DeleteAboutSertificate(string id)
        {
            var sertificate = await _db_aboutSertificate.ReadAboutSertificate(id);

            // удаление файла с сервера
            _fsContext.DeleteAboutSertificate(sertificate.SertificateFileName);

            // удаление файла из Бд
            await _db_aboutSertificate.DeleteAboutSertificate(id);

            return RedirectToAction("Index", "Panel");
        }

        // удалить все файлы сертификатов с сервера
        [HttpGet]
        public async Task<IActionResult> DeleteAllAboutSertificates()
        {
            // удалить все файлы
            _fsContext.DeleteAllAboutSertificates();

            // удалить все элементы коллекции
            await _db_aboutSertificate.DeleteAllAboutSertificates();

            return RedirectToAction("Index", "Panel");
        }
        #endregion
    }
}