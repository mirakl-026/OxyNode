using System;
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

namespace OxyNode.Services.MongoDB
{
    public class MDB_KB_industrySolutionService : IKB_industrySolutionService
    {
        private IMongoCollection<KB_industrySolution> IndustrySolutionCollection;
        private IWebHostEnvironment _appEnvironment;

        public MDB_KB_industrySolutionService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции нормативных документов RegularDocumentCollection
            IndustrySolutionCollection = db.GetCollection<KB_industrySolution>("IndustrySolutionCollection");
        }


        // получить все объекты коллекции - отраслевые решения
        public async Task<List<KB_industrySolution>> GetAllIndustrySolutions()
        {
            return await IndustrySolutionCollection.Find(new BsonDocument()).ToListAsync();
        }

        #region CRUD

        // Create
        public async Task CreateIndustrySolution(KB_industrySolution indSol)
        {
            await IndustrySolutionCollection.InsertOneAsync(indSol);
        }

        // Read
        public async Task<KB_industrySolution> ReadIndustrySolution(string id)
        {
            return await IndustrySolutionCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateIndustrySolution(KB_industrySolution indSol)
        {
            await IndustrySolutionCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(indSol.Id)), indSol);
        }

        // Delete
        public async Task DeleteIndustrySolution(string id)
        {
            await IndustrySolutionCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion

        public async Task DeleteAllIndustrySolutions()
        {
            // удалить коллекцию в БД
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);
            await db.DropCollectionAsync("IndustrySolutionCollection");
        }
    }
}
