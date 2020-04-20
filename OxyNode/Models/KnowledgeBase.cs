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
    // модель страницы - база знаний
    public class KnowledgeBase
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // обязательные описание
        [Required(ErrorMessage = "Описание обязательно", AllowEmptyStrings = false)]
        public string DescriptionHtml5 { get; set; }
    }
}
