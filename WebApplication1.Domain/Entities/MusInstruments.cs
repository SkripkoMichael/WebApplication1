using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace WebApplication1.Domain.Entities
{
    public class MusInstruments
    {
        public int Id { get; set; } 
        public string Name { get; set; } 
        public string Description { get; set; }
        public int Price { get; set; }
        public string? Image { get; set; } // путь к файлу изображения
       
        public int CategoryId { get; set; }
       [JsonIgnore]
        public Category? Category { get; set; }
    }
}
