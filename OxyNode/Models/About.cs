using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OxyNode.Models
{
    // модель страницы "О нас"
    public class About
    {
        // обязательные поля
        public string Description { get; set; }

        // необязательные поля
        public List<AboutSertificate> Sertifcates { get; set; }
    }
}
