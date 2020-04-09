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
    // Сервис -> mongoDB - Contacts
    public class ContactsService
    {
        private IMongoCollection<Contacts> ContactsCollection;
        private bool HasContacts = false;   // поскольку "Контакты" в единственном числе

        public ContactsService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции Contacts
            ContactsCollection = db.GetCollection<Contacts>("ContactsCollection");
        }

        #region CRUD

        // Create
        public async Task CreateContacts(Contacts c)
        {
            // если до текущего момента в БД нет документа "Контакты", то документ - создаётся
            if (HasContacts == false)
            {
                await ContactsCollection.InsertOneAsync(c);
                HasContacts = true;
            }
            // если на текущий момент в БД уже есть документ "Контакты", то он заменяется
            else
            {
                await ContactsCollection.ReplaceOneAsync(new BsonDocument(), c);
            }            
        }

        // Read
        public async Task<Contacts> ReadContacts()
        {
            // поскольку документ 1, ожно получить пустым Find
            return await ContactsCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateContacts(Contacts c)
        {
            // если до текущего момента в БД нет документа "Контакты", то документ - создаётся
            if (HasContacts == false)
            {
                await ContactsCollection.InsertOneAsync(c);
                HasContacts = true;
            }
            // если на текущий момент в БД уже есть документ "Контакты", то он заменяется
            else
            {
                await ContactsCollection.ReplaceOneAsync(new BsonDocument(), c);
            }
        }

        // Delete
        public async Task DeleteContacts()
        {
            HasContacts = false;    // установка флага, что "Контактов" нет
            await ContactsCollection.DeleteOneAsync(new BsonDocument());
        }

        #endregion
    }
}
