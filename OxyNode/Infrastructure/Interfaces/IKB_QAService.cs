using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление "Вопрос-Ответ"ами базы знаний

namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKB_QAService
    {
        // получить все объекты коллекции - QA (нужно для админки)
        public Task<List<KB_QA>> GetAllQAs();

        // получить страницу QA (pageSize штук)
        public Task<List<KB_QA>> GetPageOfQAs(int pageNumber, int pageSize);

        // получить кол-во QA в БД
        public Task<long> GetQAsCount();


        // получить все объекты коллекции - QA, с флагом публикации "publishToSite" : true
        public Task<List<KB_QA>> GetAllPublishedQAs();


        // получить страницу QA, с флагом публикации "publishToSite" : true (pageSize штук)
        public Task<List<KB_QA>> GetPageOfPublishedQAs(int pageNumber, int pageSize);

        // получить кол-во QA в БД, готовых к публикации на основной сайт "publishToSite" : true
        public Task<long> GetPublisedQAsCount();


        #region CRUD
        // Create
        public Task CreateQA(KB_QA qa);

        // Read
        public Task<KB_QA> ReadQA(string id);

        // Update
        public Task UpdateQA(KB_QA nqa);

        // Delete
        public Task DeleteQA(string id);
        #endregion

        public Task DeleteAllQAs();
    }
}
