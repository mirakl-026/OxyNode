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
        // получить путь к папке с файлами
        public string GetFSRegularDocumentsPath();

        // добавить файл нормативного документа на сервер
        public Task AddRegularDocument(IFormFile file);

        // получить список файлов нормативных документов на сервере
        public List<string> GetRegularDocumentsFilesList();

        // удалить нормативный документ по имени файла
        public void DeleteRegularDocument(string fileName);

        // удалить все файлы нормативных документов
        public void DeleteAllRegularDocuments();
    }
}
