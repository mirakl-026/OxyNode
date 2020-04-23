using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    public class IndustrySolutionsViewModel
    {
        // список файлов - отраслевых решений
        public List<KB_industrySolution> industrySolutions { get; set; }

        // описание страницы
        public string globalDescription = "<h3>Отраслевые решения</h3><p>Отраслевые решения отраслевые решения отраслевые решения отраслевые решения отраслевые решения отраслевые решения отраслевые решения отраслевые решения.</p>";
    }
}
