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
            if (HasContacts == true)
            {
                // поскольку документ 1, ожно получить пустым Find
                return await ContactsCollection.Find(new BsonDocument()).FirstOrDefaultAsync();
            }
            // если "Контакты" не созданы - вернуть заглушку
            else
            {
                return new Contacts()
                {
                    CompanyName = "_Имя_компании",
                    PhoneNumbers = new List<string>()
                    {
                        "8(123)4567890",
                        "8(234)5678910"
                    },
                    Email = "info@имя_компании.com",

                    AdditionalInfoKeys = new List<string>()
                    {
                        "ОГРН: ",
                        "Юридический адрес: ",
                        "Почтовый адрес: ",
                        "Факсы: ",
                        "Время работы: "
                    },
                    AdditionalInfoValues = new List<string>()
                    {
                        "123456789",
                        "Планета Земля, Российская Федерация, Подольск",
                        "Планета Земля, Российская Федерация, Домодедово",
                        "поют романсы...",
                        "24/7/365"
                    },
                    PathToScheme = "<div style=\"position:relative; overflow: hidden; \"><a href=\"https://yandex.ru/maps/org/mospolitekh/1701016484/?utm_medium=mapframe&utm_source=maps\" style=\"color:#eee;font-size:12px;position:absolute;top:0px;\">Мосполитех</a><a href=\"https://yandex.ru/maps/213/moscow/category/university_college/?utm_medium=mapframe&utm_source=maps\" style=\"color:#eee;font-size:12px;position:absolute;top:14px;\">ВУЗ в Москве</a><iframe src=\"https://yandex.ru/map-widget/v1/-/CKce5ROb\" width=\"560\" height=\"400\" frameborder=\"1\" allowfullscreen=\"true\" style=\"position:relative;\"></iframe></div>"
                };
            }

            
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
            // Если "Контакты" есть - удалить их
            if (HasContacts == true)
            {
                HasContacts = false;    // установка флага, что "Контактов" нет
                await ContactsCollection.DeleteOneAsync(new BsonDocument());
            }
        }

        #endregion
    }
}
