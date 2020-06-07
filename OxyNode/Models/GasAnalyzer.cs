using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// модель газоанализатора
namespace OxyNode.Models
{
    public class GasAnalyzer
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // Название
        public string ga_Name { get; set; }

        // Производитель
        public string ga_Manufacturer { get; set; }

        // Срок поставки
        public string ga_DeliveryTime { get; set; }

        // Описание (html5)
        public string ga_DescriptionHTML5 { get; set; }

        // список - ссылки на картинки
        public List<string> ga_Images { get; set; }

        // список - ссылки на аналоги
        public List<string> ga_Analogues { get; set; }

        // список - <K,V> - характеристики
        public Dictionary<string, string> ga_Values { get; set}

        // Цена
        public decimal ga_Price { get; set; }
    }
}
