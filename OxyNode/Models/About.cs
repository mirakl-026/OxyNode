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
    // модель страницы "О нас"
    public class About
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // обязательные описание - html5
        [Required(ErrorMessage = "Описание обязательно", AllowEmptyStrings = false)]
        [StringLength(100000, MinimumLength = 10, ErrorMessage = "Должно иметь от 10 до 100000 символов")]
        public string DescriptionHtml5 { get; set; }
    }
}
