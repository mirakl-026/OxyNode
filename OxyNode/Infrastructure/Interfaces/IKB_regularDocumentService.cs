using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

//интерфейс для драйвера - управление нормативными документами в базе знаний
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKB_regularDocumentService
    {

        // получить все объекты коллекции - нормативные документы
        public Task<List<KB_regularDocument>> GetAllRegularDocuments();

        #region CRUD

        // Create
        public Task CreateRegularDocument(KB_regularDocument rd);

        // Read
        public Task<KB_regularDocument> ReadRegularDocument(string id);

        // Update
        public Task UpdateRegularDocument(KB_regularDocument rd);

        // Delete
        public Task DeleteRegularDocument(string id);
        #endregion
    }
}
