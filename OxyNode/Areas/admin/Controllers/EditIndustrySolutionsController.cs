using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.ViewModels;
using OxyNode.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Bson;


namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditIndustrySolutionsController : Controller
    {
        // Контроллер для редактирования "Нормативные документы" - файлов этой категории
        private string FilesPath = "/resources/knowledgeBase/industrySolutions/";
        private IWebHostEnvironment _appEnvironment;
        private IKB_industrySolutionService _db;


        public EditIndustrySolutionsController (IWebHostEnvironment appEnvironment, IKB_industrySolutionService context)
        {
            _appEnvironment = appEnvironment;
            _db = context;
        }



        #region Edit industry solutions

        // загрузить файл сертификата на сервер
        [HttpGet]
        public IActionResult CreateIndustrySolution()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> CreateIndustrySolution(KB_industrySolution IS, IFormFile uploadedIS)
        {
            if (ModelState.IsValid & uploadedIS != null)
            {
                // Определение пути 
                string path = FilesPath + uploadedIS.FileName;

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedIS.CopyToAsync(fileStream);
                }

                // Сохранение в БД
                // Надпись
                if (IS.is_Name == null || IS.is_Name.Length == 0)
                {
                    IS.is_Name = uploadedIS.FileName;
                }

                // иконка
                var ext = uploadedIS.FileName.Split('.').Last();
                string icon_path = "/resources/partial/text.png";
                IS.is_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    icon_path = "/resources/partial/pdf.png";
                    IS.is_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    icon_path = "/resources/partial/word.png";
                    IS.is_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    icon_path = "/resources/partial/zip.png";
                    IS.is_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    icon_path = "/resources/partial/excel.png";
                    IS.is_fieldCssColor = "background-color:lightgreen";
                }

                IS.is_IconPath = icon_path;

                // путь к файлу
                IS.is_Path = path;

                await _db.CreateIndustrySolution(IS);
                return RedirectToAction("Index", "Panel");
            }
            return View(IS);
        }


        // показать файлы нормативных документов на сервере
        [HttpGet]
        public async Task<IActionResult> ReadAllIndustrySolutions()
        {
            IndustrySolutionsViewModel isvm = new IndustrySolutionsViewModel();
            isvm.industrySolutions = await _db.GetAllIndustrySolutions();
            return View(isvm);
        }


        // показать файл нормативного документа на сервере 
        [HttpGet]
        public async Task<IActionResult> ReadIndustrySolution(string id)
        {
            var IS = await _db.ReadIndustrySolution(id);
            return View(IS);
        }


        // обновить файл нормативного документа на сервере
        [HttpGet]
        public async Task<IActionResult> UpdateIndustrySolution(string id)
        {
            var IS = await _db.ReadIndustrySolution(id);
            return View(IS);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateIndustrySolution(string id, KB_industrySolution nIS, IFormFile newUploadedIS)
        {
            if (ModelState.IsValid & newUploadedIS != null)
            {
                var currentIS = await _db.ReadIndustrySolution(id);

                // Определение пути по текущему файлу нормативного документа
                string pathToDelete = currentIS.is_Path;
                string pathToUpdate = FilesPath + newUploadedIS.FileName;

                // удаление файла нормативного документа с сервера
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + pathToDelete);
                if (fi.Exists)
                {
                    fi.Delete();
                }

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToUpdate, FileMode.Create))
                {
                    await newUploadedIS.CopyToAsync(fileStream);
                }

                // Сохранение в БД
                // Надпись
                if (nIS.is_Name == null || nIS.is_Name.Length == 0)
                {
                    nIS.is_Name = newUploadedIS.FileName;
                }

                // иконка
                var ext = newUploadedIS.FileName.Split('.').Last();
                string icon_path = "/resources/partial/text.png";
                nIS.is_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    icon_path = "/resources/partial/pdf.png";
                    nIS.is_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    icon_path = "/resources/partial/word.png";
                    nIS.is_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    icon_path = "/resources/partial/zip.png";
                    nIS.is_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    icon_path = "/resources/partial/excel.png";
                    nIS.is_fieldCssColor = "background-color:lightgreen";
                }

                nIS.is_IconPath = icon_path;

                // путь к файлу
                nIS.is_Path = pathToUpdate;

                // id
                nIS.Id = id;

                await _db.UpdateIndustrySolution(nIS);
                return RedirectToAction("Index", "Panel");
            }
            return View(nIS);
        }


        // удалить файл отраслевого решения с сервера
        [HttpGet]
        public async Task<IActionResult> DeleteIndustrySolution(string id)
        {
            var currentIS = await _db.ReadIndustrySolution(id);

            // Определение пути
            string pathToDelete = currentIS.is_Path;

            // удаление файла с сервера
            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + pathToDelete);
            if (fi.Exists)
            {
                fi.Delete();
            }

            // удаление файла из Бд
            await _db.DeleteIndustrySolution(id);

            return RedirectToAction("Index", "Panel");
        }
        #endregion

        // удалить все файлы отраслевых решений
        [HttpGet]
        public async Task<IActionResult> DeleteAllIndustrySolutions ()
        {
            await _db.DeleteAllIndustrySolutions();
            return RedirectToAction("Index", "Panel");
        }
    }
}