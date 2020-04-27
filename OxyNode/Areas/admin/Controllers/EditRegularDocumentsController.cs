using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.ViewModels;
using OxyNode.Models;
using System.IO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using MongoDB.Bson;

namespace OxyNode.Areas.admin.Controllers
{
    [Area("admin")]
    public class EditRegularDocumentsController : Controller
    {
        // Контроллер для редактирования "Нормативные документы" - файлов этой категории
        private string FilesPath = "/resources/knowledgeBase/regularDocuments/";
        private IWebHostEnvironment _appEnvironment;
        private KB_regularDocumentService _db;

        public EditRegularDocumentsController(IWebHostEnvironment appEnvironment, KB_regularDocumentService context)
        {
            _appEnvironment = appEnvironment;
            _db = context;
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
                string path = FilesPath + uploadedRd.FileName;

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedRd.CopyToAsync(fileStream);
                }

                // Сохранение в БД
                // Надпись
                if (rd.rd_Name == null || rd.rd_Name.Length == 0)
                {
                    rd.rd_Name = uploadedRd.FileName;
                }

                // иконка и цвет поля
                var ext = uploadedRd.FileName.Split('.').Last();
                string icon_path = "/resources/partial/text.png";
                rd.rd_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    icon_path = "/resources/partial/pdf.png";
                    rd.rd_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    icon_path = "/resources/partial/word.png";
                    rd.rd_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    icon_path = "/resources/partial/zip.png";
                    rd.rd_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    icon_path = "/resources/partial/excel.png";
                    rd.rd_fieldCssColor = "background-color:lightgreen";
                }

                rd.rd_IconPath = icon_path;

                // путь к файлу
                rd.rd_Path = path;

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
                string pathToDelete = currentRd.rd_Path;
                string pathToUpdate = FilesPath + newUploadedRd.FileName;

                // удаление файла нормативного документа с сервера
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + pathToDelete);
                if (fi.Exists)
                {
                    fi.Delete();
                }

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + pathToUpdate, FileMode.Create))
                {
                    await newUploadedRd.CopyToAsync(fileStream);
                }

                // Сохранение в БД
                // Надпись
                if (nrd.rd_Name == null || nrd.rd_Name.Length == 0)
                {
                    nrd.rd_Name = newUploadedRd.FileName;
                }

                // иконка и цвет поля
                var ext = newUploadedRd.FileName.Split('.').Last();
                string icon_path = "/resources/partial/text.png";
                nrd.rd_fieldCssColor = "background-color:lightgrey";
                if (ext.Equals("pdf"))
                {
                    icon_path = "/resources/partial/pdf.png";
                    nrd.rd_fieldCssColor = "background-color:lightpink";
                }
                else if (ext.Equals("doc") || ext.Equals("docx"))
                {
                    icon_path = "/resources/partial/word.png";
                    nrd.rd_fieldCssColor = "background-color:lightblue";
                }
                else if (ext.Equals("zip") || ext.Equals("rar") || ext.Equals("7z"))
                {
                    icon_path = "/resources/partial/zip.png";
                    nrd.rd_fieldCssColor = "background-color:lightyellow";
                }
                else if (ext.Equals("xls") || ext.Equals("xlsx"))
                {
                    icon_path = "/resources/partial/excel.png";
                    nrd.rd_fieldCssColor = "background-color:lightgreen";
                }

                nrd.rd_IconPath = icon_path;

                // путь к файлу
                nrd.rd_Path = pathToUpdate;

                // id
                nrd.Id = id;

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

            // Определение пути
            string pathToDelete = currentRd.rd_Path;

            // удаление файла с сервера
            FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + pathToDelete);
            if (fi.Exists)
            {
                fi.Delete();
            }

            // удаление файла из Бд
            await _db.DeleteRegularDocument(id);

            return RedirectToAction("Index", "Panel");
        }

        #endregion

    }
}