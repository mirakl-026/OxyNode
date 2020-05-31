using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;


// интерфейс для драйвера - управление содержимым страницы "база знаний"
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKnowledgeBaseService
    {

        #region CRUD

        // Create
        public Task CreateKnowledgeBase(KnowledgeBase a);

        // Read
        public Task<KnowledgeBase> ReadKnowledgeBase();

        // Update
        public Task UpdateKnowledgeBase(KnowledgeBase a);


        // Delete
        public Task DeleteKnowledgeBase();

        #endregion
    }
}
