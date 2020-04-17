using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;


namespace OxyNode.Services
{
    // Сервис MongoDB -> About / "О нас"
    // страница "О нас" так-же как и "Контакты" хранится в БД в единичном представлении
    public class AboutService
    {
        private IMongoCollection<About> AboutCollection;

        public AboutService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции Contacts
            AboutCollection = db.GetCollection<About>("AboutCollection");
        }

        #region CRUD

        // Create
        public async Task CreateAbout(About a)
        {
            // если до текущего момента в БД нет документа "Контакты", то документ - создаётся
            if (AboutCollection.Find(new BsonDocument()) == null)
            {
                await AboutCollection.InsertOneAsync(a);
            }
            // если на текущий момент в БД уже есть документ "Контакты", то он заменяется
            else
            {
                AboutCollection.DeleteOne(new BsonDocument());
                await AboutCollection.InsertOneAsync(a);
            }
        }

        // Read
        public async Task<About> ReadAbout()
        {
            if (AboutCollection.Find(new BsonDocument()).ToList().Count > 0)
            {
                // поскольку документ 1, ожно получить пустым Find
                return await AboutCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            }
            // если "Контакты" не созданы - вернуть заглушку
            else
            {
                return new About()
                {
                    DescriptionHtml5 = "<div><h3>О нас</h3><p>Описание страницы \"О нас\"</p></div>"
                };
            }
        }

        // Update
        public async Task UpdateAbout (About a)
        {
            // если до текущего момента в БД нет документа "Контакты", то документ - создаётся
            if (AboutCollection.Find(new BsonDocument()).ToList().Count == 0)
            {
                await AboutCollection.InsertOneAsync(a);
            }
            // если на текущий момент в БД уже есть документ "Контакты", то он заменяется
            else
            {
                AboutCollection.DeleteOne(new BsonDocument());
                await AboutCollection.InsertOneAsync(a);
            }
        }

        // Delete
        public async Task DeleteAbout()
        {
            // Если "Контакты" есть - удалить их
            if (AboutCollection.Find(new BsonDocument()).ToList().Count > 0)
            {
                await AboutCollection.DeleteOneAsync(new BsonDocument());
            }
        }

        #endregion
    }
}
