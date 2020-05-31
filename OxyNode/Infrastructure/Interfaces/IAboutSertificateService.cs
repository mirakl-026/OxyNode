using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;


// интерфейс для сервиса - управление сертификатами страницы "О нас"
// все методы - асинхронные
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IAboutSertificateService
    {
        public Task<List<AboutSertificate>> GetAllAboutSertificates();

        #region CRUD

        public Task CreateAboutSertificate(AboutSertificate sertificate);

        public Task<AboutSertificate> ReadAboutSertificate(string id);

        public Task UpdateAboutSertificate(AboutSertificate sertificate);

        public Task DeleteAboutSertificate(string id);

        #endregion
    }
}
