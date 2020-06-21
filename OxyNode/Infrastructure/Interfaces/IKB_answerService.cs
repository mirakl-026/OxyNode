using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление ответами в "Вопрос-Ответ" базы знаний

namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKB_answerService
    {
        // получить все объекты коллекции - ответы
        public Task<List<KB_answer>> GetAllAnswers();

        // получить страницу ответов (pageSize штук)
        public Task<List<KB_answer>> GetPageOfAnswers(int pageNumber, int pageSize);

        // получить кол-во ответов в БД
        public Task<long> GetAnswersCount();

        // получить все объекты коллекции - ответы, с флагом публикации True
        // "publishToSite" : true
        public Task<List<KB_answer>> GetAllPublishedAnswers();


        // получить страницу ответов, с флагом публикации True (pageSize штук)
        // "publishToSite" : true
        public Task<List<KB_answer>> GetPageOfPublishedAnswers(int pageNumber, int pageSize);

        // получить кол-во ответов в БД, готовых к публикации на основной сайт
        public Task<long> GetPublisedAnswersCount();

        #region CRUD

        // Create
        public Task CreateAnswer(KB_answer answer);

        // Read
        public Task<KB_answer> ReadAnswer(string id);

        // Update
        public Task UpdateAnswer(KB_answer newAnswer);

        // Delete
        public Task DeleteAnswer(string id);

        #endregion

        public Task DeleteAllAnswers();
    }
}
