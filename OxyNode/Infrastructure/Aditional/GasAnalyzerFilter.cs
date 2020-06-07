using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

// дополнитеьная сущность - фильтр для выборки газоанализаторов из БД
namespace OxyNode.Infrastructure.Aditional
{
    public class GasAnalyzerFilter
    {
        // поля для фильтрации
        // фильтр по имени
        public string ByName { get; set; }

        // фильтр по производителю
        public string ByManufacturer { get; set; }

        // фильтр по веществу
        public List<string> BySubstance { get; set; }

        // фильтр по типу - переносной/стационарный
        public string ByType { get; set; }

        // фильтр по сфере применение
        public string ByScopeOfApplication { get; set; }
    }
}
