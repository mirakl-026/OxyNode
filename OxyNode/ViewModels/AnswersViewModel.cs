using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    public class AnswersViewModel
    {
        // список ответов как объектов
        public List<KB_answer> answers { get; set; }

        // номер текущей страницы ответов
        public int currentPageNumber;

        // общее кол-во ответов
        public long answersCount;
    }
}
