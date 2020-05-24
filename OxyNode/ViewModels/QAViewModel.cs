using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// модель вопрос-ответ, содержащая и вопросы и ответы - нужна для главного сайта
namespace OxyNode.ViewModels
{
    public class QAViewModel
    {
        // список вопросов как объектов
        public List<KB_question> questions { get; set; }

        // список ответов как объектов
        public List<KB_answer> answers { get; set; }

        // номер текущей страницы вопрос-ответов (смотрится по ответам, у них должен быть флаг публикации true)
        public int currentPageNumber;

        // общее кол-во вопрос-ответов (смотрится по ответам, у них должен быть фалг публикации true)
        public long qaCount;
    }
}
