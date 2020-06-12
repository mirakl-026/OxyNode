using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

// модель прибора
namespace OxyNode.Models
{
    public class Device
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [HiddenInput(DisplayValue = false)]
        public string Id { get; set; }

        // Название
        [Display(Name = "Название прибора")]
        public string Name { get; set; }

        // Производитель
        [Display(Name = "Производитель")]
        public string Manufacturer { get; set; }

        // Срок поставки
        [Display(Name = "Срок поставки")]
        public string DeliveryTime { get; set; }

        // тип прибора - переносной / стационарный
        [Display(Name = "Тип")]
        public string Type { get; set; }

        // сфера применения
        [Display(Name = "Сфера применения")]
        public string ScopeOfApplication { get; set; }

        // Описание (html5)
        [Display(Name = "Описание")]
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
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
    }

    // в будущем для оптимизации - возможно надо будет использовать enum
    // public enum DeviceType
    // {
    //     Portable,
    //     Stationary
    // }
}
