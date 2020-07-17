using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// сервис управления файлами картинками
namespace OxyNode.Infrastructure.Interfaces.FileSystem
{
    public interface IFileImageService
    {
        // добавить файл картинки на сервер
        public Task AddImage(IFormFile file);

        // получить список картинок на сервере
        public Task<List<string>> GetImagesFilesList();

        // удалить картинку по имени файла
        public Task DeleteImage(string fileName);

        // удалить все картинки
        public Task DeleteAllImages();
    }
}
