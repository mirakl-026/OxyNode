﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;

// объединённая модель - вопрос + ответ
// имеет все поля вопрос (KB_question) и все поля ответа (KB_answer) - кроме ссылок
namespace OxyNode.Models
{
    public class KB_QA
    {
        // Id для хранения в MongoDb
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // KB_question
        // ФИО вопросителя - текст
        [Display(Name = "ФИО")]
        [StringLength(60, MinimumLength = 4, ErrorMessage = "ФИО должно быть от 4 до 60 символов")]
        public string FullName { get; set; }

        // Адрес (Город)
        [Display(Name = "Адрес/Город")]
        [StringLength(60, MinimumLength = 2, ErrorMessage = "Адрес/город должен быть от 2 до 60 символов")]
        public string Address { get; set; }

        // содержание - текст
        [Display(Name = "Содержание вопроса")]
        [Required(ErrorMessage = "Содержание обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Содержание вопроса должно быть от 10 до 500 символов")]
        public string questionText { get; set; }

        // e-mail
        [Display(Name = "e-mail")]
        [StringLength(60, MinimumLength = 6, ErrorMessage = "E-mail должен быть от 6 до 60 символов")]
        public string e_mail { get; set; }


        // KB_answer
        // содержание ответа - текст
        [Display(Name = "Содержание ответа")]
        [Required(ErrorMessage = "Содержание ответа обязательно", AllowEmptyStrings = false)]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "Ответ должен быть от 10 до 500 символов")]
        public string answerText { get; set; }

        // флаг публикации - если True - ответ виден на сайте
        [Display(Name = "Флаг публикации на сайт")]
        public bool publishToSite { get; set; }
    }
}
