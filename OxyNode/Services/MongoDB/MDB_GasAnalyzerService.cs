using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;
using OxyNode.Infrastructure.Interfaces;
using OxyNode.Infrastructure.Aditional;

using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;

// сервис для управления газоанализатороми в MongoDB

namespace OxyNode.Services.MongoDB
{
    public class MDB_GasAnalyzerService : IGasAnalyzerService
    {

        private IMongoCollection<GasAnalyzer> GasAnalyzerCollection;

        public MDB_GasAnalyzerService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции статей NoteCollection
            GasAnalyzerCollection = db.GetCollection<GasAnalyzer>("GasAnalyzerCollection");

        }


        // получить все газоанализаторы
        public async Task<List<GasAnalyzer>> GetAllGasAnalyzers()
        {
            return await GasAnalyzerCollection.Find(new BsonDocument()).ToListAsync();
        }


        // полчить все газоанализаторы по фильтру
        public async Task<List<GasAnalyzer>> GetAllGasAnalyzersFiltered(GasAnalyzerFilter filter)
        {
            var fb = Builders<GasAnalyzer>.Filter;

            FilterDefinition<GasAnalyzer> f =
                fb.Eq("Name", filter.ByName) |
                fb.Eq("Manufacturer", filter.ByManufacturer) |
                fb.Eq("ScopeOfApplication", filter.ByScopeOfApplication) |
                fb.Eq("Type", filter.ByType);

            if (filter.BySubstance != null)
            {
                f |= fb.All("Substance", filter.BySubstance);
            }

            return await GasAnalyzerCollection.Find(f).ToListAsync();
        }


        // получить страницу газоанализаторов
        public async Task<List<GasAnalyzer>> GetPageOfGasAnalyzers(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во газоанализаторов/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await GasAnalyzerCollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }


        // получить страницу газоанализаторов с учётом фильтра
        public async Task<List<GasAnalyzer>> GetPageOfGasAnalyzersFiltered(int pageNumber, int pageSize, GasAnalyzerFilter filter)
        {
            var fb = Builders<GasAnalyzer>.Filter;

            FilterDefinition<GasAnalyzer> f =
                fb.Eq("Name", filter.ByName) |
                fb.Eq("Manufacturer", filter.ByManufacturer) |
                fb.Eq("ScopeOfApplication", filter.ByScopeOfApplication) |
                fb.Eq("Type", filter.ByType);

            if (filter.BySubstance != null)
            {
                f |= fb.All("Substance", filter.BySubstance);
            }

            return await GasAnalyzerCollection.Find(f).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }


        // получить кол-во газоанализаторов в БД
        public async Task<long> GetGasAnalyzersCount()
        {
            return await GasAnalyzerCollection.CountDocumentsAsync(new BsonDocument());
        }



        #region CRUD
        public async Task CreateGasAnalyzer(GasAnalyzer ga)
        {
            await GasAnalyzerCollection.InsertOneAsync(ga);
        }

        public async Task<GasAnalyzer> ReadGasAnalyzer(string id)
        {
            return await GasAnalyzerCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task UpdateGasAnalyzer(GasAnalyzer newGa)
        {
            await GasAnalyzerCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(newGa.Id)), newGa);
        }

        public async Task DeleteGasAnalyzer(string id)
        {
            await GasAnalyzerCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion

    }
}
