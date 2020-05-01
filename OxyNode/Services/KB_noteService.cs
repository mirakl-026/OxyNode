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
    public class KB_noteService
    {
        private IMongoCollection<KB_note> NoteCollection;

        public KB_noteService()
        {
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);

            // обращаемся к коллекции статей NoteCollection
            NoteCollection = db.GetCollection<KB_note>("NoteCollection");


        }


        // получить все объекты коллекции - статьи
        public async Task<List<KB_note>> GetAllNotes()
        {
            return await NoteCollection.Find(new BsonDocument()).ToListAsync();
        }

        // получить страницу статей (6 чтук)
        public async Task<List<KB_note>> GetPageOfNotes(int pageNumber)
        {
            // общее кол-во статей в БД
            //long notesCounter = NoteCollection.CountDocuments(new BsonDocument());

            // выборка по номеру страницы - 
            // кол-во статей/6 - кол-во возможных страниц
            // limit - 6, skip = (pageNumber-1)*6
            return await NoteCollection.Find(new BsonDocument()).Skip((pageNumber-1)*6).Limit(6).ToListAsync();
        }



        #region CRUD

        // Create
        public async Task CreateNote(KB_note note)
        {
            await NoteCollection.InsertOneAsync(note);
        }

        // Read
        public async Task<KB_note> ReadNote(string id)
        {
            return await NoteCollection.Find(new BsonDocument("_id", new ObjectId(id))).FirstOrDefaultAsync();
        }

        // Update
        public async Task UpdateNote(KB_note newNote)
        {
            await NoteCollection.ReplaceOneAsync(new BsonDocument("_id", new ObjectId(newNote.Id)), newNote);
        }

        // Delete
        public async Task DeleteNote(string id)
        {
            await NoteCollection.DeleteOneAsync(new BsonDocument("_id", new ObjectId(id)));
        }
        #endregion
    }
}
