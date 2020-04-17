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
    // MongoDb -> AboutSertificate / "Сертификаты" в странице "О нас"
    public class AboutSertificateService
    {
        private IMongoCollection<AboutSertificate> AboutSertificateCollection;

        public AboutSertificateService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции Contacts
            AboutSertificateCollection = db.GetCollection<AboutSertificate>("AboutSertificateCollection");
        }

        // получить все объекты коллекции - сертификаты
        public async Task<List<AboutSertificate>> GetAllAboutSertificates()
        {
            return await AboutSertificateCollection.Find(new BsonDocument()).ToListAsync();
        }


        #region CRUD

        // Create
        public async Task CreateAboutSertificate(AboutSertificate sertificate)
        {
            await AboutSertificateCollection.InsertOneAsync(sertificate);
        }

        // Read
        public async Task<AboutSertificate> ReadAboutSertificate(string id)
        {
            // поскольку документ 1, ожно получить пустым Find
            return await AboutSertificateCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateAboutSertificate(AboutSertificate sertificate)
        {
            await AboutSertificateCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(sertificate.Id)), sertificate);
        }

        // Delete
        public async Task DeleteAboutSertificate(string id)
        {
            await AboutSertificateCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }

        #endregion
    }
}
