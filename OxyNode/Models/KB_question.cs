using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

// касается вопрос-ответ
// модель вопроса
namespace OxyNode.Models
{
    public class KB_question
    {
        // Id для хранения в MongoDb
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        string Id { get; set; }

        // ФИО вопросителя - текст
        [Display(Name = "ФИО")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "ФИО должно быть от 4 до 60 символов")]
        string FullName { get; set; }

        // Адрес (Город)
        [Display(Name = "Адрес/Город")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Адрес/город должен быть от 2 до 50 символов")]
        string Address { get; set; }

        // содержание - текст
        [Display(Name = "Содержание вопроса")]
        [Required(ErrorMessage = "Содержание обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Содержание вопроса должно быть от 10 до 500 символов")]
        string questionText { get; set; }

        // e-mail
        [Display(Name = "e-mail")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "E-mail должен быть от 6 до 60 символов")]
        string e_mail { get; set; }

    }
}
