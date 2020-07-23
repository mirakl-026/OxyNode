using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Infrastructure.Interfaces;
using OxyNode.Infrastructure.Interfaces.FileSystem;
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
        private IWebHostEnvironment _appEnvironment;
        private IKB_industrySolutionService _db;
        private IFileIndustrySolutionService _fsContext;


        public EditIndustrySolutionsController (IWebHostEnvironment appEnvironment, IKB_industrySolutionService context, IFileIndustrySolutionService fsContext)
        {
            _appEnvironment = appEnvironment;
            _db = context;
            _fsContext = fsContext;
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
                string path = _fsContext.GetFSIndustrySolutionsPath() + uploadedIS.FileName;

                // Сохранение файла на сервере
                await _fsContext.AddIndustrySolution(uploadedIS);

                // Сохранение в БД
                // Надпись
                if (IS.is_Name == null || IS.is_Name.Length == 0)
                {
                    IS.is_Name = uploadedIS.FileName;
                }

                // цвет
                var ext = uploadedIS.FileName.Split('.').Last();
                IS.is_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    IS.is_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    IS.is_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    IS.is_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    IS.is_fieldCssColor = "background-color:lightgreen";
                }

                // путь к файлу
                IS.is_Path = path;
                IS.is_FileName = uploadedIS.FileName;

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
                string pathToUpdate = _fsContext.GetFSIndustrySolutionsPath() + newUploadedIS.FileName;

                // удаление файла нормативного документа с сервера
                _fsContext.DeleteIndustrySolution(currentIS.is_FileName);

                // Сохранение файла на сервере
                await _fsContext.AddIndustrySolution(newUploadedIS);

                // Сохранение в БД
                // Надпись
                if (nIS.is_Name == null || nIS.is_Name.Length == 0)
                {
                    nIS.is_Name = newUploadedIS.FileName;
                }

                // цвет
                var ext = newUploadedIS.FileName.Split('.').Last();
                nIS.is_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    nIS.is_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    nIS.is_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    nIS.is_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    nIS.is_fieldCssColor = "background-color:lightgreen";
                }

                // путь к файлу
                nIS.is_Path = pathToUpdate;
                nIS.is_FileName = newUploadedIS.FileName;

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

            // удаление файла с сервера
            _fsContext.DeleteIndustrySolution(currentIS.is_FileName);

            // удаление файла из Бд
            await _db.DeleteIndustrySolution(id);

            return RedirectToAction("Index", "Panel");
        }
        #endregion

        // удалить все файлы отраслевых решений
        [HttpGet]
        public async Task<IActionResult> DeleteAllIndustrySolutions ()
        {
            // удалить все файлы
            _fsContext.DeleteAllIndustrySolutions();

            // удалить из БД
            await _db.DeleteAllIndustrySolutions();
            return RedirectToAction("Index", "Panel");
        }
    }
}