﻿using System;
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
    public class MDB_KB_noteService : IKB_noteService
    {
        private IMongoCollection<KB_note> NoteCollection;
        private IWebHostEnvironment _appEnvironment;

        public MDB_KB_noteService(IWebHostEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;

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

        // получить страницу статей (pageSize штук)
        public async Task<List<KB_note>> GetPageOfNotes(int pageNumber, int pageSize)
        {
            // выборка по номеру страницы - 
            // кол-во статей/pageSize - кол-во возможных страниц
            // limit - pageSize, skip = (pageNumber-1)*pageSize
            return await NoteCollection.Find(new BsonDocument()).Skip((pageNumber - 1) * pageSize).Limit(pageSize).ToListAsync();
        }

        // получить кол-во статей в БД
        public async Task<long> GetNotesCount()
        {
            return await NoteCollection.CountDocumentsAsync(new BsonDocument());
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

        public async Task DeleteAllNotes()
        {
            // удалить все файлы 
            var allNotes = await GetAllNotes();
            foreach (var note in allNotes)
            {
                FileInfo fi = new FileInfo(_appEnvironment.WebRootPath + note.note_LinkToPreviewImage);
                if (fi.Exists)
                {
                    fi.Delete();
                }
            }

            // удалить коллекцию в БД
            // строка подключения к БД
            string connectionString = "mongodb://localhost:27017/OxyNode";
            var connection = new MongoUrlBuilder(connectionString);

            // получаем клиента для взаимодействия с БД
            MongoClient client = new MongoClient(connectionString);

            // получаем доступ к самой БД
            IMongoDatabase db = client.GetDatabase(connection.DatabaseName);
            await db.DropCollectionAsync("NoteCollection");
        }
    }
}
