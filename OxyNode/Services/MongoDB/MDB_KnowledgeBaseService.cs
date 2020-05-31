using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Infrastructure.Interfaces;

using OxyNode.Models;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

namespace OxyNode.Services.MongoDB
{
    public class MDB_KnowledgeBaseService : IKnowledgeBaseService
    {
        private IMongoCollection<KnowledgeBase> KnowledgeBaseCollection;

        public MDB_KnowledgeBaseService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции Contacts
            KnowledgeBaseCollection = db.GetCollection<KnowledgeBase>("KnowledgeBaseCollection");
        }

        #region CRUD

        // Create
        public async Task CreateKnowledgeBase(KnowledgeBase a)
        {
            // если до текущего момента в БД нет документа "База знаний", то документ - создаётся
            if (KnowledgeBaseCollection.Find(new BsonDocument()) == null)
            {
                await KnowledgeBaseCollection.InsertOneAsync(a);
            }
            // если на текущий момент в БД уже есть документ "База знаний", то он заменяется
            else
            {
                KnowledgeBaseCollection.DeleteOne(new BsonDocument());
                await KnowledgeBaseCollection.InsertOneAsync(a);
            }
        }

        // Read
        public async Task<KnowledgeBase> ReadKnowledgeBase()
        {
            if (KnowledgeBaseCollection.Find(new BsonDocument()).ToList().Count > 0)
            {
                // поскольку документ 1, ожно получить пустым Find
                return await KnowledgeBaseCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            }
            // если "База знаний" не созданы - вернуть заглушку
            else
            {
                return new KnowledgeBase()
                {
                    DescriptionHtml5 = "<div><h3>База знаний</h3><p>Описание страницы \"База знаний\"</p></div>"
                };
            }
        }

        // Update
        public async Task UpdateKnowledgeBase(KnowledgeBase a)
        {
            // если до текущего момента в БД нет документа "База знаний", то документ - создаётся
            if (KnowledgeBaseCollection.Find(new BsonDocument()).ToList().Count == 0)
            {
                await KnowledgeBaseCollection.InsertOneAsync(a);
            }
            // если на текущий момент в БД уже есть документ "База знаний", то он заменяется
            else
            {
                KnowledgeBaseCollection.DeleteOne(new BsonDocument());
                await KnowledgeBaseCollection.InsertOneAsync(a);
            }
        }

        // Delete
        public async Task DeleteKnowledgeBase()
        {
            // Если "База знаний" есть - удалить их
            if (KnowledgeBaseCollection.Find(new BsonDocument()).ToList().Count > 0)
            {
                await KnowledgeBaseCollection.DeleteOneAsync(new BsonDocument());
            }
        }

        #endregion
    }
}
