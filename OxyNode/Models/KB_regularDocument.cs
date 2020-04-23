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
    // Модель - База знаний - нормативный документ
    public class KB_regularDocument
    {
        // id для хранения в БД
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // краткое описание
        [Display(Name = "Краткое описание нормативного документа")]
        [Required(ErrorMessage = "Краткое описание обязательно", AllowEmptyStrings = false)]
        public string rd_ShortDescription { get; set; }

        // текст ссылки на файл
        [Display(Name = "Текст ссылки на файл")]
        public string rd_Name { get; set; }


        // путь к файлу
        [Display(Name = "Путь к файлу")]
        public string rd_Path { get; set; }
    }
}
