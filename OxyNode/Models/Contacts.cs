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
    // Модель данных для страницы "Контакты"
    public class Contacts
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        [Display(Name = "Название компании")]
        [Required(ErrorMessage = "Название обязательно", AllowEmptyStrings = false)]
        [StringLength(50, MinimumLength = 4, ErrorMessage = "Название должно иметь от 4 до 50 символов")]
        public string CompanyName { get; set; }


        [Display(Name = "Телефон")]
        [Required(ErrorMessage = "Телефон обязателен", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 11, ErrorMessage = "Категория должна иметь от 11 до 30 символов")]
        public List<string> PhoneNumbers { get; set; }


        [Display(Name = "E-mail")]
        [Required(ErrorMessage = "E-mail обязателен", AllowEmptyStrings = false)]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Производитель должен иметь от 5 до 30 символов")]
        public string Email { get; set; }


        // необязательная информация
        [Display(Name = "Схема проезда")]
        public string PathToScheme { get; set; }

        [Display(Name = "Дополнительная информация - key")]
        public string Description { get; set; }

        [Display(Name = "Дополнительная информация - values")]
        public List<string> AdditionalInfoKeys { get; set; }
        public List<string> AdditionalInfoValues { get; set; }
    }
}
