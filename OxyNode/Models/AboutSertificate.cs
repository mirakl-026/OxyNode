using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OxyNode.Models
{
    // модель дл сертификатов страницы "О нас"
    // подразумевается, что сертификат - картинка 
    public class AboutSertificate
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }


        [Display(Name = "Заголовок к сертификату")]
        public string SertificateLabel { get; set; }


        [Display(Name = "Название файла сертификата")]
        public string SertificateFileName { get; set; }


        [Display(Name = "Путь к файлу сертификата")]
        public string SertificatePath { get; set; }
    }
}
