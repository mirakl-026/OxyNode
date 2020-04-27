﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;


namespace OxyNode.Services
{
    // Сервис по управлению файлами "Нормативный документ" - со стороны БД
    public class KB_regularDocumentService
    {
        private IMongoCollection<KB_regularDocument> RegularDocumentCollection;

        public KB_regularDocumentService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции нормативных документов RegularDocumentCollection
            RegularDocumentCollection = db.GetCollection<KB_regularDocument>("RegularDocumentCollection");
        }


        // получить все объекты коллекции - нормативные документы
        public async Task<List<KB_regularDocument>> GetAllRegularDocuments()
        {
            return await RegularDocumentCollection.Find(new BsonDocument()).ToListAsync();
        }

        #region CRUD

        // Create
        public async Task CreateRegularDocument(KB_regularDocument rd)
        {
            await RegularDocumentCollection.InsertOneAsync(rd);
        }

        // Read
        public async Task<KB_regularDocument> ReadRegularDocument(string id)
        {
            return await RegularDocumentCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateRegularDocument(KB_regularDocument rd)
        {
            await RegularDocumentCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(rd.Id)), rd);
        }

        // Delete
        public async Task DeleteRegularDocument(string id)
        {
            await RegularDocumentCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion
    }
}