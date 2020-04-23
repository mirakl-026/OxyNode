using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    public class RegularDocumentsViewModel
    {
        public List<KB_regularDocument> regularDocuments { get; set; }

        public string globalDescription = "<h3>Нормативные документы</h3><p>В разделе собраны документы по эксплуатации газоанализаторов, регламентирующие правила установки и применения газоанализаторов, в том числе, во взрывоопасных зонах.</p>";
    }
}
