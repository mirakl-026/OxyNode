using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление статьями в базе знаний
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IFileService
    {

        public Task<List<KB_note>> GetThreeOfFiles(int pageNumber, int pageSize);

        public Task<long> GetFilesCount();


        #region CRUD

        // Create
        public Task AddFile();

        // Read
        public Task<KB_note> ReadFile();

        // Delete
        public Task DeleteFile(string id);

        #endregion

        public Task DeleteAllFiles();
    }
}