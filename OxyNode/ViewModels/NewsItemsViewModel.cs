using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    // вью-модель списка новостей
    public class NewsItemsViewModel
    {
        // список объектов - новостей
        public List<NewsItem> newsItems { get; set; }

        // номер текущей страницы новостей
        public int currentPageNumber;

        // общее кол-во новостей
        public long newsItemsCount;
    }
}
