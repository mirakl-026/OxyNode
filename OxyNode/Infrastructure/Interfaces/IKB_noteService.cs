using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление статьями в базе знаний
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKB_noteService
    {

        // получить все объекты коллекции - статьи
        public Task<List<KB_note>> GetAllNotes();

        // получить страницу статей (pageSize штук)
        public Task<List<KB_note>> GetPageOfNotes(int pageNumber, int pageSize);

        // получить кол-во статей в БД
        public Task<long> GetNotesCount();


        #region CRUD

        // Create
        public Task CreateNote(KB_note note);

        // Read
        public Task<KB_note> ReadNote(string id);

        // Update
        public Task UpdateNote(KB_note newNote);

        // Delete
        public Task DeleteNote(string id);

        #endregion

        public Task DeleteAllNotes();
    }
}
