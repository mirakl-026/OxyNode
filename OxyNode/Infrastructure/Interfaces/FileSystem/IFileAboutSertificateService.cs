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
        // получить путь к папке с файлами
        public string GetFSAboutSertificatePath();

        // добавить файл сертификата на сервер
        public Task AddAboutSertificate(IFormFile file);

        // получить список файлов сертификатов на сервере
        public List<string> GetAboutSertificatesFilesList();

        // удалить файл сертификата по имени файла
        public void DeleteAboutSertificate(string fileName);

        // удалить все файлы сертификатов
        public void DeleteAllAboutSertificates();
    }
}
