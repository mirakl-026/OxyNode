using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// сервис для управления файлами сертификатов 
namespace OxyNode.Infrastructure.Interfaces.FileSystem
{
    public interface IFileAboutSertificateService
    {
        // добавить файл сертификата на сервер
        public Task AddAboutSertificate(IFormFile file);

        // получить список файлов сертификатов на сервере
        public Task<List<string>> GetAboutSertificatesFilesList();

        // удалить файл сертификата по имени файла
        public Task DeleteAboutSertificate(string fileName);

        // удалить все файлы сертификатов
        public Task DeleteAllAboutSertificates();
    }
}
