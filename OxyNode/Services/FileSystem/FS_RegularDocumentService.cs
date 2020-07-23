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
    public class FS_RegularDocumentService : IFileRegularDocumentService
    {

        private string RegularDocumentsPath = "/resources/regularDocuments/";
        private string FullServerRegularDocumentsPath;
        private IWebHostEnvironment _appEnvironment;

        public FS_RegularDocumentService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            FullServerRegularDocumentsPath = _appEnvironment.WebRootPath + RegularDocumentsPath;
        }

        public async Task AddRegularDocument(IFormFile file)
        {
            // Определение пути 
            string path = FullServerRegularDocumentsPath + file.FileName;

            // Сохранение файла на сервере
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public void DeleteAllRegularDocuments()
        {
            // получение списка всех файлов
            string[] filesPaths = Directory.GetFiles(FullServerRegularDocumentsPath);
            foreach (var filePath in filesPaths)
            {
                // удаление файла нормативного документа с сервера
                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
        }

        public void DeleteRegularDocument(string fileName)
        {
            // Определение пути
            string path = FullServerRegularDocumentsPath + fileName;

            // удаление файла нормативного документа с сервера
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
        }

        public string GetFSRegularDocumentsPath()
        {
            return RegularDocumentsPath;
        }

        public List<string> GetRegularDocumentsFilesList()
        {
            return Directory.GetFiles(FullServerRegularDocumentsPath).ToList();
        }
    }
}
