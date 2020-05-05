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
        [StringLength(60, MinimumLength = 4, ErrorMessage = "ABJ иметь от 4 до 60 символов")]
        string FullName { get; set; }

        // содержание - текст
        [Display(Name = "Содержание вопроса")]
        [Required(ErrorMessage = "Содержание обязательно", AllowEmptyStrings = false)]
        [StringLength(200, MinimumLength = 10, ErrorMessage = "Название должно иметь от 10 до 200 символов")]
        string questionText { get; set; }

        // e-mail
        [Display(Name = "e-mail")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "Название должно иметь от 6 до 60 символов")]
        string e_mail { get; set; }

    }
}
