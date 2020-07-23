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
    public class FS_IndustrySolutionService : IFileIndustrySolutionService
    {

        private string IndustrySolutionsPath = "/resources/industrySolutions/";
        private string FullServerIndustrySolutionsPath;
        private IWebHostEnvironment _appEnvironment;

        public FS_IndustrySolutionService (IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
            FullServerIndustrySolutionsPath = _appEnvironment.WebRootPath + IndustrySolutionsPath;
        }

        public async Task AddIndustrySolution(IFormFile file)
        {
            // Определение пути 
            string path = FullServerIndustrySolutionsPath + file.FileName;

            // Сохранение файла на сервере
            using (var fileStream = new FileStream(path, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        public void DeleteAllIndustrySolutions()
        {
            // получение списка всех файлов
            string[] filesPaths = Directory.GetFiles(FullServerIndustrySolutionsPath);
            foreach (var filePath in filesPaths)
            {
                // удаление файла отраслевого решения с сервера
                FileInfo fi = new FileInfo(filePath);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }
        }

        public void DeleteIndustrySolution(string fileName)
        {
            // Определение пути
            string path = FullServerIndustrySolutionsPath + fileName;

            // удаление файла отраслевого решения с сервера
            FileInfo fi = new FileInfo(path);
            if (fi.Exists)
            {
                fi.Delete();
            }
        }

        public string GetFSIndustrySolutionsPath()
        {
            return IndustrySolutionsPath;
        }

        public List<string> GetIndustrySolutionsFilesList()
        {
            return Directory.GetFiles(FullServerIndustrySolutionsPath).ToList();
        }
    }
}
