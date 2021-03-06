﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Infrastructure.Interfaces;

using OxyNode.Models;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using System.IO;
using Microsoft.AspNetCore.Hosting;


// сервис управления новостями
namespace OxyNode.Services.MongoDB
{
    public class MDB_NewsService : INewsService
    {
        private IMongoCollection<NewsItem> NewsCollection;
        private IWebHostEnvironment _appEnvironment;

        public MDB_NewsService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции новостей NewsCollection
            NewsCollection = db.GetCollection<NewsItem>("NewsCollection");
        }


        // получить все объекты коллекции - новости
        public async Task<List<NewsItem>> GetAllNewsItems()
        {
            return await NewsCollection.Find(new BsonDocument()).ToListAsync();
        }

        // получить страницу новостей (pageSize штук)
        public async Task<List<NewsItem>> GetPageOfNewsItems(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во статей/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await NewsCollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        // получить кол-во новостей в БД
        public async Task<long> GetNewsItemsCount()
        {
            return await NewsCollection.CountDocumentsAsync(new BsonDocument());
        }


        #region CRUD

        // Create
        public async Task CreateNewsItem(NewsItem ni)
        {
            await NewsCollection.InsertOneAsync(ni);
        }

        // Read
        public async Task<NewsItem> ReadNewsItem(string id)
        {
            return await NewsCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateNewsItem(NewsItem ni)
        {
            await NewsCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(ni.Id)), ni);
        }

        // Delete
        public async Task DeleteNewsItem(string id)
        {
            await NewsCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion
        
        public async Task DeleteAllNewsItems()
        {
            // удалить все файлы 
            var allNewsItems = await GetAllNewsItems();
            foreach (var newsItem in allNewsItems)
            {
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + newsItem.news_LinkToPreviewImage);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }

            // удалить коллекцию в БД
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);
            await db.DropCollectionAsync("NewsCollection");
        }
    }
}
