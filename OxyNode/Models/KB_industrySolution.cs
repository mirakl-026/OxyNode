﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OxyNode.Models
{
    // Модель - База знаний - отраслевое решение
    // файл на сервере - есть id, путь к файлу, краткое описание, название ссылки
    public class KB_industrySolution
    {
        // id для хранения в БД
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // краткое описание
        [Display(Name = "Краткое описание отраслевого решения")]
        [Required(ErrorMessage = "Краткое описание обязательно", AllowEmptyStrings = false)]
        public string is_ShortDescription { get; set; }

        // текст ссылки на файл
        [Display(Name = "Текст ссылки на файл")]
        public string is_Name { get; set; }

        [Display(Name = "Название файла отраслевого решения")]
        public string is_FileName { get; set; }

        // путь к файлу
        [Display(Name = "Путь к файлу")]
        public string is_Path { get; set; }

        // цвет поля
        [Display(Name = "Цвет поля файла")]
        public string is_fieldCssColor { get; set; }
    }
}
