using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


// сервис для управления файлами отраслевых решений
namespace OxyNode.Infrastructure.Interfaces.FileSystem
{
    public interface IFileIndustrySolutionService
    {
        // получить путь к папке с файлами
        public string GetFSIndustrySolutionsPath();

        // добавить файл отраслевого решения на сервер
        public Task AddIndustrySolution(IFormFile file);

        // получить список файлов отраслевых решений на сервере
        public List<string> GetIndustrySolutionsFilesList();

        // удалить отраслевое решение по имени файла
        public void DeleteIndustrySolution(string fileName);

        // удалить все файлы отраслевых решений
        public void DeleteAllIndustrySolutions();
    }
}
