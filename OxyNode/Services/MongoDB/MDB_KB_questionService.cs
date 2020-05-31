using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Infrastructure.Interfaces;

using OxyNode.Models;
using MongoDB.Driver;
using MongoDB.Bson;

// сервис для управления вопросами в "QA"
namespace OxyNode.Services.MongoDB
{
    public class MDB_KB_questionService : IKB_questionService
    {
        private IMongoCollection<KB_question> QuestionCollection;

        public MDB_KB_questionService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции вопросов QuestionCollection
            QuestionCollection = db.GetCollection<KB_question>("QuestionCollection");
        }


        // получить все объекты коллекции - вопросы (нужно для админки)
        public async Task<List<KB_question>> GetAllQuestions()
        {
            return await QuestionCollection.Find(new BsonDocument()).ToListAsync();
        }

        // получить страницу вопросов (pageSize штук)
        public async Task<List<KB_question>> GetPageOfQuestions(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во статей/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await QuestionCollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        // получить кол-во вопросов в БД
        public async Task<long> GetQuestionsCount()
        {
            return await QuestionCollection.CountDocumentsAsync(new BsonDocument());
        }


        #region CRUD

        // Create
        public async Task CreateQuestion(KB_question question)
        {
            await QuestionCollection.InsertOneAsync(question);
        }

        // Read
        public async Task<KB_question> ReadQuestion(string id)
        {
            return await QuestionCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateQuestion(KB_question newQuestion)
        {
            await QuestionCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(newQuestion.Id)), newQuestion);
        }

        // Delete
        public async Task DeleteQuestion(string id)
        {
            await QuestionCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion
    }
}
