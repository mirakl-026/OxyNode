using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using OxyNode.Models;

namespace OxyNode.ViewModels
{
    public class AboutViewModel
    {
        // содержание страницы "О нас" в формате Html5
        public About VM_About { get; set; }

        // массив сертификатов
        public List<AboutSertificate> VM_AboutSertificates { get; set; }
    }
}
