using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using OxyNode.Infrastructure.Interfaces.FileSystem;

namespace OxyNode.Services.FileSystem
{
    public class FS_AboutSertificateService : IFileAboutSertificateService
    {
        private string AboutSertificatesPath = "/resources/aboutSertificates/";
        private string FullServerAboutSertificatesPath;
        private IWebHostEnvironment _appEnvironment;

        public FS_AboutSertificateService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            FullServerAboutSertificatesPath = _appEnvironment.WebRootPath + AboutSertificatesPath;
        }

        public string GetFSAboutSertificatePath()
        {
            return AboutSertificatesPath;
        }

        public async Task AddAboutSertificate(IFormFile file)
        {
            // Определение пути 
            string path = FullServerAboutSertificatesPath + file.FileName;

            // Сохранение файла на сервере
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public void DeleteAboutSertificate(string fileName)
        {
            // Определение пути
            string path = FullServerAboutSertificatesPath + fileName;

            // удаление файла сертификата с сервера
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
        }

        public Task DeleteAllAboutSertificates()
        {
            throw new NotImplementedException();
        }

        public Task<List<string>> GetAboutSertificatesFilesList()
        {
            throw new NotImplementedException();
        }
    }
}
