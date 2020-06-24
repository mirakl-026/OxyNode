using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;
using OxyNode.Infrastructure.Interfaces;
using MongoDB.Driver;
using MongoDB.Bson;

// сервис для управления QA
namespace OxyNode.Services.MongoDB
{
    public class MDB_KB_QAService : IKB_QAService
    {
        private IMongoCollection<KB_QA> QACollection;

        public MDB_KB_QAService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции вопросов QuestionCollection
            QACollection = db.GetCollection<KB_QA>("QACollection");
        }

        public async Task<List<KB_QA>> GetAllQAs()
        {
            return await QACollection.Find(new BsonDocument()).ToListAsync();
        }

        public async Task<List<KB_QA>> GetPageOfQAs(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во QA/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await QACollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        public async Task<long> GetQAsCount()
        {
            return await QACollection.CountDocumentsAsync(new BsonDocument());
        }



        public async Task<List<KB_QA>> GetAllPublishedQAs()
        {
            var filter = Builders<KB_QA>.Filter.Eq("publishToSite", true);
            return await QACollection.Find(filter).ToListAsync();
        }

        public async Task<List<KB_QA>> GetPageOfPublishedQAs(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во QA/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            var filter = Builders<KB_QA>.Filter.Eq("publishToSite", true);
            return await QACollection.Find(filter).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        public async Task<long> GetPublisedQAsCount()
        {
            var filter = Builders<KB_QA>.Filter.Eq("publishToSite", true);
            return await QACollection.CountDocumentsAsync(filter);
        }



        #region CRUD
        public async Task CreateQA(KB_QA qa)
        {
            await QACollection.InsertOneAsync(qa);
        }

        public async Task<KB_QA> ReadQA(string id)
        {
            return await QACollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task UpdateQA(KB_QA nqa)
        {
            await QACollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(nqa.Id)), nqa);
        }
        public async Task DeleteQA(string id)
        {
            await QACollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        #endregion
        public async Task DeleteAllQAs()
        {
            // удалить коллекцию в БД
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);
            await db.DropCollectionAsync("QACollection");
        }
    }
}
