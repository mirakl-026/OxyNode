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


namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditRegularDocumentsController : Controller
    {
        // Контроллер для редактирования "Нормативные документы" - файлов этой категории
        private IWebHostEnvironment _appEnvironment;
        private IKB_regularDocumentService _db;
        private IFileRegularDocumentService _fsContext;

        public EditRegularDocumentsController(IWebHostEnvironment appEnvironment, IKB_regularDocumentService context, IFileRegularDocumentService fsContext)
        {
            _appEnvironment = appEnvironment;
            _db = context;
            _fsContext = fsContext;
        }


        #region Edit regular documents

        // загрузить файл сертификата на сервер
        [HttpGet]
        public IActionResult CreateRegularDocument()
        {
            return View();
        }

        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> CreateRegularDocument(KB_regularDocument rd, IFormFile uploadedRd)
        {
            if (ModelState.IsValid & uploadedRd != null)
            {
                // Определение пути 
                string path = _fsContext.GetFSRegularDocumentsPath() + uploadedRd.FileName;

                // Сохранение файла на сервере
                await _fsContext.AddRegularDocument(uploadedRd);

                // Сохранение в БД
                // Надпись
                if (rd.rd_Name == null || rd.rd_Name.Length == 0)
                {
                    rd.rd_Name = uploadedRd.FileName;
                }

                // цвет поля
                var ext = uploadedRd.FileName.Split('.').Last();
                rd.rd_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    rd.rd_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    rd.rd_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    rd.rd_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    rd.rd_fieldCssColor = "background-color:lightgreen";
                }

                // путь к файлу
                rd.rd_Path = path;
                rd.rd_FileName = uploadedRd.FileName;

                await _db.CreateRegularDocument(rd);
                return RedirectToAction("Index", "Panel");
            }
            return View(rd);
        }


        // показать файлы нормативных документов на сервере
        [HttpGet]
        public async Task<IActionResult> ReadAllRegularDocuments()
        {
            RegularDocumentsViewModel rdvm = new RegularDocumentsViewModel();
            rdvm.regularDocuments = await _db.GetAllRegularDocuments();
            return View(rdvm);
        }


        // показать файл нормативного документа на сервере 
        [HttpGet]
        public async Task<IActionResult> ReadRegularDocument(string id)
        {
            var rd = await _db.ReadRegularDocument(id);
            return View(rd);
        }


        // обновить файл нормативного документа на сервере
        [HttpGet]
        public async Task<IActionResult> UpdateRegularDocument(string id)
        {
            var rd = await _db.ReadRegularDocument(id);
            return View(rd);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateRegularDocument(string id, KB_regularDocument nrd, IFormFile newUploadedRd)
        {
            if (ModelState.IsValid & newUploadedRd != null)
            {
                var currentRd = await _db.ReadRegularDocument(id);

                // Определение пути по текущему файлу нормативного документа
                string pathToUpdate = _fsContext.GetFSRegularDocumentsPath() + newUploadedRd.FileName;

                // удаление файла нормативного документа с сервера
                _fsContext.DeleteRegularDocument(currentRd.rd_FileName);

                // Сохранение файла на сервере
                await _fsContext.AddRegularDocument(newUploadedRd);

                // Сохранение в БД
                // Надпись
                if (nrd.rd_Name == null || nrd.rd_Name.Length == 0)
                {
                    nrd.rd_Name = newUploadedRd.FileName;
                }

                // цвет поля
                var ext = newUploadedRd.FileName.Split('.').Last();
                nrd.rd_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    nrd.rd_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    nrd.rd_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    nrd.rd_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    nrd.rd_fieldCssColor = "background-color:lightgreen";
                }

                // путь к файлу
                nrd.rd_Path = pathToUpdate;

                // id
                nrd.Id = id;
                nrd.rd_FileName = newUploadedRd.FileName;

                await _db.UpdateRegularDocument(nrd);
                return RedirectToAction("Index", "Panel");
            }
            return View(nrd);
        }


        // удалить файл сертификата с сервера
        [HttpGet]
        public async Task<IActionResult> DeleteRegularDocument(string id)
        {
            var currentRd = await _db.ReadRegularDocument(id);

            // удаление файла с сервера
            _fsContext.DeleteRegularDocument(currentRd.rd_FileName);

            // удаление файла из Бд
            await _db.DeleteRegularDocument(id);

            return RedirectToAction("Index", "Panel");
        }

        #endregion

        [HttpGet]
        public async Task<IActionResult> DeleteAllRegularDocuments()
        {
            // удаление файлов
            _fsContext.DeleteAllRegularDocuments();

            // удаление из БД
            await _db.DeleteAllRegularDocuments();
            return RedirectToAction("Index", "Panel");
        }

    }
}