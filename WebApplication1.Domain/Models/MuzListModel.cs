using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Domain.Entities;

namespace WebApplication1.Domain.Models
{
    public class MuzListModel<T>
    {// запрошенный список объектов
        public List<T> Items { get; set; } = new();
        // номер текущей страницы
        public int CurrentPage { get; set; } = 1;
        // общее количество страниц
        public int TotalPages { get; set; } = 1;
        public static implicit operator MuzListModel<T>(MuzListModel<MusInstruments> v)
        {
            throw new NotImplementedException();
        }
    }
}
