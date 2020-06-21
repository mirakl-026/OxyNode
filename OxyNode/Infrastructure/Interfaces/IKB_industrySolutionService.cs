using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление отраслевыми решениями в базе знаний
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKB_industrySolutionService
    {
        // получить все объекты коллекции - отраслевые решения
        public Task<List<KB_industrySolution>> GetAllIndustrySolutions();

        #region CRUD

        // Create
        public Task CreateIndustrySolution(KB_industrySolution indSol);

        // Read
        public Task<KB_industrySolution> ReadIndustrySolution(string id);

        // Update
        public Task UpdateIndustrySolution(KB_industrySolution indSol);

        // Delete
        public Task DeleteIndustrySolution(string id);

        #endregion

        public Task DeleteAllIndustrySolutions();
    }
}
