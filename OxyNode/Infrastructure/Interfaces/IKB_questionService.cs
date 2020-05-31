using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// интерфейс для драйвера - управление вопросами в "Вопрос-Ответ" базы знаний
namespace OxyNode.Infrastructure.Interfaces
{
    public interface IKB_questionService
    {

        // получить все объекты коллекции - вопросы (нужно для админки)
        public Task<List<KB_question>> GetAllQuestions();

        // получить страницу вопросов (pageSize штук)
        public Task<List<KB_question>> GetPageOfQuestions(int pageNumber, int pageSize);

        // получить кол-во вопросов в БД
        public Task<long> GetQuestionsCount();


        #region CRUD

        // Create
        public Task CreateQuestion(KB_question question);

        // Read
        public Task<KB_question> ReadQuestion(string id);

        // Update
        public Task UpdateQuestion(KB_question newQuestion);

        // Delete
        public Task DeleteQuestion(string id);
        #endregion
    }
}
