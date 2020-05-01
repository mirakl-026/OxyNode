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

        // описание страницы
        public string globalDescription = "<h3>Статьи</h3><p>Статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи статьи.</p>";

    }
}
