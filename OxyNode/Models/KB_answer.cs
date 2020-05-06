using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

// касается вопрос-ответ
// модель ответа
namespace OxyNode.Models
{
    public class KB_answer
    {
        // Id для хранения в MongoDb
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // ФИО вопросителя - текст
        [Display(Name = "ФИО")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "ФИО должно быть от 4 до 60 символов")]
        public string FullNameQestionary { get; set; }

        // содержание вопроса - текст
        [Display(Name = "Содержание вопроса")]
        [Required(ErrorMessage = "Содержание вопроса обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Вопрос должен быть от 10 до 500 символов")]
        public string questionText { get; set; }

        // содержание ответа - текст
        [Display(Name = "Содержание ответа")]
        [Required(ErrorMessage = "Содержание ответа обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Ответ должен быть от 10 до 500 символов")]
        public string answerText { get; set; }

        // флаг публикации - если True - ответ виден на сайте
        [Display(Name = "Флаг публикации на сайт")]
        public bool publishToSite { get; set; }

        // ссылка на Id вопроса 
        [Display(Name = "Ссылка на Id вопроса")]
        public string QuestionId { get; set; }

    }
}
