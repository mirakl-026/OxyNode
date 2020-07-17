using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// сервис для управления файлами - нормативными документами
namespace OxyNode.Infrastructure.Interfaces.FileSystem
{
    public interface IFileRegularDocumentService
    {
        // добавить файл нормативного документа на сервер
        public Task AddIndustrySolution(IFormFile file);

        // получить список файлов нормативных документов на сервере
        public Task<List<string>> GetIndustrySolutionsFilesList();

        // удалить нормативный документ по имени файла
        public Task DeleteIndustrySolution(string fileName);

        // удалить все файлы нормативных документов
        public Task DeleteAllIndustrySolutions();
    }
}
