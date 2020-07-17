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
        // добавить файл отраслевого решения на сервер
        public Task AddIndustrySolution(IFormFile file);

        // получить список файлов отраслевых решений на сервере
        public Task<List<string>> GetIndustrySolutionsFilesList();

        // удалить отраслевое решение по имени файла
        public Task DeleteIndustrySolution(string fileName);

        // удалить все файлы отраслевых решений
        public Task DeleteAllIndustrySolutions();
    }
}
