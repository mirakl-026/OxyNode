using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;


// интерфейс для драйвера - управление новостями
namespace OxyNode.Infrastructure.Interfaces
{
    public interface INewsService
    {
        // получить все объекты коллекции - новости
        public Task<List<NewsItem>> GetAllNewsItems();

        // получить страницу новостей (pageSize штук)
        public Task<List<NewsItem>> GetPageOfNewsItems(int pageNumber, int pageSize);

        // получить кол-во новостей в БД
        public Task<long> GetNewsItemsCount();


        #region CRUD

        // Create
        public Task CreateNewsItem(NewsItem ni);

        // Read
        public Task<NewsItem> ReadNewsItem(string id);

        // Update
        public Task UpdateNewsItem(NewsItem ni);

        // Delete
        public Task DeleteNewsItem(string id);
        #endregion
    }
}
