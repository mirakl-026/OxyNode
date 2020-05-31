using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Infrastructure.Interfaces;

using OxyNode.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

// Сервис для управлениями ответами в "QA"
namespace OxyNode.Services.MongoDB
{
    public class MDB_KB_answerService
    {
        private IMongoCollection<KB_answer> AnswerCollection;

        public MDB_KB_answerService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции ответов AnswerCollection
            AnswerCollection = db.GetCollection<KB_answer>("AnswerCollection");
        }


        // получить все объекты коллекции - ответы
        public async Task<List<KB_answer>> GetAllAnswers()
        {
            return await AnswerCollection.Find(new BsonDocument()).ToListAsync();
        }

        // получить страницу ответов (pageSize штук)
        public async Task<List<KB_answer>> GetPageOfAnswers(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во статей/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await AnswerCollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        // получить кол-во ответов в БД
        public async Task<long> GetAnswersCount()
        {
            return await AnswerCollection.CountDocumentsAsync(new BsonDocument());
        }



        // получить все объекты коллекции - ответы, с флагом публикации True
        // "publishToSite" : true
        public async Task<List<KB_answer>> GetAllPublishedAnswers()
        {
            var filter = Builders<KB_answer>.Filter.Eq("publishToSite", true);
            return await AnswerCollection.Find(filter).ToListAsync();
        }

        // получить страницу ответов, с флагом публикации True (pageSize штук)
        // "publishToSite" : true
        public async Task<List<KB_answer>> GetPageOfPublishedAnswers(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во статей/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            var filter = Builders<KB_answer>.Filter.Eq("publishToSite", true);
            return await AnswerCollection.Find(filter).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        // получить кол-во ответов в БД, готовых к публикации на основной сайт
        public async Task<long> GetPublisedAnswersCount()
        {
            var filter = Builders<KB_answer>.Filter.Eq("publishToSite", true);
            return await AnswerCollection.CountDocumentsAsync(filter);
        }



        #region CRUD

        // Create
        public async Task CreateAnswer(KB_answer answer)
        {
            await AnswerCollection.InsertOneAsync(answer);
        }

        // Read
        public async Task<KB_answer> ReadAnswer(string id)
        {
            return await AnswerCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateAnswer(KB_answer newAnswer)
        {
            await AnswerCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(newAnswer.Id)), newAnswer);
        }

        // Delete
        public async Task DeleteAnswer(string id)
        {
            await AnswerCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion
    }
}
