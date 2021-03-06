﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

using OxyNode.Infrastructure.Interfaces.FileSystem;

namespace OxyNode.Areas.admin.Controllers
{
    /*
    [Area("admin")]
    public class ImageManagerController : Controller
    {
        private IWebHostEnvironment _appEnvironment;
        private string FilesPath = "/resources/images/";

        public ImageManagerController(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        #region Image load routing

        // загрузить картинку
        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> LoadImage(IFormFile uploadedImage)
        {
            if (uploadedImage != null)
            {
                // Определение пути 
                string path = FilesPath + uploadedImage.FileName;

                // удаление файла картинки с сервера
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
                if (fi.Exists)
                {
                    return new JsonResult(path);
                }

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(fileStream);
                }
                return new JsonResult(path);
            }
            return new JsonResult("");
        }

        // получить ссылки всех картинок
        [HttpGet]
        public IActionResult GetAllImages()
        {
            string[] images = Directory.GetFiles(_appEnvironment.WebRootPath + FilesPath);
            for (int i = 0; i < images.Length; i++)
            {
                images[i] = images[i].Substring(_appEnvironment.WebRootPath.Length);
            }
            return new JsonResult(images);
        }


        // удалить картинку



        #endregion

    }
    */

    [Area("admin")]
    public class ImageManagerController : Controller
    {
        private readonly IFileImageService _fsContext;

        public ImageManagerController(IFileImageService fsContext)
        {
            _fsContext = fsContext;
        }

        #region Image load routing

        // загрузить картинку
        [HttpPost]
        [RequestSizeLimit(52428800)]
        public async Task<IActionResult> LoadImage(IFormFile uploadedImage)
        {
            if (uploadedImage != null)
            {
                // Определение пути 
                string path = _fsContext.GetFSImagesPath() + uploadedImage.FileName;

                // удаление файла картинки с сервера
                _fsContext.DeleteImage(uploadedImage.FileName);

                // Сохранение файла на сервере
                await _fsContext.AddImage(uploadedImage);
                return new JsonResult(path);
            }
            return new JsonResult("");
        }

        // получить ссылки всех картинок
        [HttpGet]
        public IActionResult GetAllImages()
        {
            string[] images = _fsContext.GetImagesFilesList();
            return new JsonResult(images);
        }


        // удалить картинку



        #endregion

    }
}