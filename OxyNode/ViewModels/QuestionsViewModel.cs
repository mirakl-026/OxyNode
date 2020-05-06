using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    public class QuestionsViewModel
    {
        // список вопросов как объектов
        public List<KB_question> questions { get; set; }

        // номер текущей страницы вопросов
        public int currentPageNumber;

        // общее кол-во вопросов
        public long questionsCount;
    }
}
