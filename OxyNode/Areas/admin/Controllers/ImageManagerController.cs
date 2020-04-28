using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using OxyNode.Services;
using OxyNode.Models;
using OxyNode.ViewModels;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace OxyNode.Areas.admin.Controllers
{
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

                // удаление файла сертификата с сервера
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + path);
                if (fi.Exists)
                {
                    return RedirectToAction("Index", "Panel");
                }

                // Сохранение файла на сервере
                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await uploadedImage.CopyToAsync(fileStream);
                }
            }
            return RedirectToAction("Index", "Panel");
        }


        // удалить картинку


        #endregion

    }
}