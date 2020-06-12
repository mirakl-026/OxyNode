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
    public class MDB_DeviceService : IDeviceService
    {

        private IMongoCollection<Device> DeviceCollection;

        public MDB_DeviceService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции статей NoteCollection
            DeviceCollection = db.GetCollection<Device>("DeviceCollection");

        }


        // получить все газоанализаторы
        public async Task<List<Device>> GetAllDevices()
        {
            return await DeviceCollection.Find(new BsonDocument()).ToListAsync();
        }


        // полчить все газоанализаторы по фильтру
        public async Task<List<Device>> GetAllDevicesFiltered(DeviceFilter filter)
        {
            var fb = Builders<Device>.Filter;

            FilterDefinition<Device> f =
                fb.Eq("Name", filter.ByName) |
                fb.Eq("Manufacturer", filter.ByManufacturer) |
                fb.Eq("ScopeOfApplication", filter.ByScopeOfApplication) |
                fb.Eq("Type", filter.ByType);

            if (filter.BySubstance != null)
            {
                f |= fb.All("Substance", filter.BySubstance);
            }

            return await DeviceCollection.Find(f).ToListAsync();
        }


        // получить страницу газоанализаторов
        public async Task<List<Device>> GetPageOfDevices(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во газоанализаторов/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await DeviceCollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }


        // получить страницу газоанализаторов с учётом фильтра
        public async Task<List<Device>> GetPageOfDevicesFiltered(int pageNumber, int pageSize, DeviceFilter filter)
        {
            var fb = Builders<Device>.Filter;

            FilterDefinition<Device> f =
                fb.Eq("Name", filter.ByName) |
                fb.Eq("Manufacturer", filter.ByManufacturer) |
                fb.Eq("ScopeOfApplication", filter.ByScopeOfApplication) |
                fb.Eq("Type", filter.ByType);

            if (filter.BySubstance != null)
            {
                f |= fb.All("Substance", filter.BySubstance);
            }

            return await DeviceCollection.Find(f).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }


        // получить кол-во газоанализаторов в БД
        public async Task<long> GetDevicesCount()
        {
            return await DeviceCollection.CountDocumentsAsync(new BsonDocument());
        }



        #region CRUD
        public async Task CreateDevice(Device dev)
        {
            await DeviceCollection.InsertOneAsync(dev);
        }

        public async Task<Device> ReadDevice(string id)
        {
            return await DeviceCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        public async Task UpdateDevice(Device newDev)
        {
            await DeviceCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(newDev.Id)), newDev);
        }

        public async Task DeleteDevice(string id)
        {
            await DeviceCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion

    }
}
