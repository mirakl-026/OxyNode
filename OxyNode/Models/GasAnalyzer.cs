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
        public string Name { get; set; }

        // Производитель
        public string Manufacturer { get; set; }

        // Срок поставки
        public string DeliveryTime { get; set; }

        // тип газонализатора - переносной / стационарный
        public string Type { get; set; }

        // сфера применения
        public string ScopeOfApplication { get; set; }

        // Описание (html5)
        public string DescriptionHTML5 { get; set; }

        // список - ссылки на картинки
        public List<string> Images { get; set; }

        // список - ссылки на аналоги
        public List<string> Analogues { get; set; }

        // список - рабоче вещества
        public List<string> Substances { get; set; }

        // список - <K,V> - характеристики
        public Dictionary<string, string> Values { get; set; }

        // Цена
        public decimal Price { get; set; }
    }

    // в будущем для оптимизации - возможно надо будет использовать enum
    // public enum GasAnalyzerType
    // {
    //     Portable,
    //     Stationary
    // }
}
