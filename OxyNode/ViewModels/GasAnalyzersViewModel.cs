using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

// вьюмодель для газоанализаторов
namespace OxyNode.ViewModels
{
    public class GasAnalyzersViewModel
    {
        // список объектов - газоанализаторов
        public List<GasAnalyzer> gasAnalyzers { get; set; }

    }
}
