using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OxyNode.Models
{
    // модель новости
    public class NewsItem
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // обязательное название новости
        [Display(Name = "Название новости")]
        [Required(ErrorMessage = "Название новости обязательно", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Должно иметь от 5 до 100 символов")]
        public string news_Name { get; set; }

        // обязательные краткое описание
        [Display(Name = "Краткое описание")]
        [Required(ErrorMessage = "Краткое описание обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Должно иметь от 10 до 500 символов")]
        public string news_ShortDescription { get; set; }

        // обязательные содержание - html5
        [Display(Name = "Содержание")]
        [Required(ErrorMessage = "Содержание обязательно", AllowEmptyStrings = false)]
        [StringLength(100000, MinimumLength = 10, ErrorMessage = "Должно иметь от 10 до 100000 символов")]
        public string news_ContentHtml5 { get; set; }

        // необязательные содержание - html5
        [Display(Name = "Ссылка на картинку в превью")]
        public string news_LinkToPreviewImage { get; set; }
    }
}
