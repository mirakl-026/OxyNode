using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    public class NotesViewModel
    {
        // список файлов - отраслевых решений
        public List<KB_note> notes { get; set; }

        // номер текущей страницы статей
        public int currentPageNumber;

        // общее кол-во статей
        public long notesCount;

    }
}
