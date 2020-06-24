using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// вьюмодель для QA
namespace OxyNode.ViewModels
{
    public class QAsViewModel
    {
        // список QA как объектов
        public List<KB_QA> qas { get; set; }

        // номер текущей страницы вопрос-ответов (смотрится по ответам, у них должен быть флаг публикации true)
        public int currentPageNumber;

        // общее кол-во вопрос-ответов (смотрится по ответам, у них должен быть фалг публикации true)
        public long qaCount;
    }
}
