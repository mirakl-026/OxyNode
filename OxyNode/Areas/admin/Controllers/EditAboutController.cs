using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
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

        private string FilesPath = "/resources/about/sertificates/";
        private IWebHostEnvironment _appEnvironment;

        private IAboutService _db_about;
        private IAboutSertificateService _db_aboutSertificate;

        public EditAboutController(IWebHostEnvironment appEnvironment, IAboutService aboutContext, IAboutSertificateService aboutSertificateContext)
        {
            _appEnvironment = appEnvironment;
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
                string path = FilesPath + uploadedSertificate.FileName;

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedSertificate.CopyToAsync(fileStream);
                }

                // Сохранение в БД
                string newSertLabel = "";
                if (sertificateLabel == null || sertificateLabel.Length == 0)
                {
                    newSertLabel = uploadedSertificate.FileName;
                }
                else
                {
                    newSertLabel = sertificateLabel;
                }

                AboutSertificate newSertificate = new AboutSertificate()
                {
                    SertificateName = newSertLabel,
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


        // обновить файл сертификат на сервере
        [HttpGet]
        public async Task<IActionResult> UpdateAboutSertificate(string id)
        {
            var sertificate = await _db_aboutSertificate.ReadAboutSertificate(id);
            return View(sertificate);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAboutSertificate(string id, IFormFile newSertificate)
        {
            if (newSertificate != null)
            {
                var currentSertificate = await _db_aboutSertificate.ReadAboutSertificate(id);

                // Определение пути по текущему файлу сертификата
                string pathToUpdate = currentSertificate.SertificatePath;

                // удаление файла сертификата с сервера
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + pathToUpdate);
                if (fi.Exists)
                {
                    fi.Delete();
                }

                // Сохранение файла сертификата на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToUpdate, FileMode.Create))
                {
                    await newSertificate.CopyToAsync(fileStream);
                }
            }
            return RedirectToAction("Index", "Panel");
        }


        // удалить файл сертификата с сервера
        [HttpGet]
        public async Task<IActionResult> DeleteAboutSertificate(string id)
        {
            var sertificate = await _db_aboutSertificate.ReadAboutSertificate(id);

            // Определение пути
            string pathToDelete = sertificate.SertificatePath;

            // удаление файла с сервера
            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + pathToDelete);
            if (fi.Exists)
            {
                fi.Delete();
            }

            // удаление файла из Бд
            await _db_aboutSertificate.DeleteAboutSertificate(id);

            return RedirectToAction("Index", "Panel");
        }

        #endregion

    }
}