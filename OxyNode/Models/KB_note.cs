using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace OxyNode.Models
{
    // Модель - База знаний - Статья - содержит код html5
    public class KB_note
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // обязательное название статьи
        [Required(ErrorMessage = "Название статьи обязательно", AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Должно иметь от 5 до 100 символов")]
        public string note_NoteName { get; set; }

        // обязательные краткое описание
        [Required(ErrorMessage = "Краткое описание обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Должно иметь от 10 до 500 символов")]
        public string note_ShortDescription { get; set; }

        // обязательные содержание - html5
        [Required(ErrorMessage = "Содержимое обязательно", AllowEmptyStrings = false)]
        [StringLength(100000, MinimumLength = 10, ErrorMessage = "Должно иметь от 10 до 100000 символов")]
        public string note_ContentHtml5 { get; set; }

        // необязательные содержание - html5
        [Display(Name = "Ссылка на картинку в превью")]
        public string note_LinkToPreviewImage { get; set; }

    }
}
