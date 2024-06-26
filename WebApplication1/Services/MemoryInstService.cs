using WebApplication1.Domain.Models;
using WebApplication1.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Text.Json;
namespace WebApplication1.Services
{
    public class MemoryInstService: IMuzInstrumentService
    {
        private readonly IConfiguration _config;
        List<MusInstruments> _mi;
        List<Category> _categories;
        public MemoryInstService([FromServices] IConfiguration config, ICategoryService categoryService)
        {
            _categories = categoryService.GetCategoryListAsync().Result.Data;
            _config = config;
            SetupData();
        }

        /// <summary>
        /// Инициализация списков
        /// </summary>
        private void SetupData()
        {
            _mi = new List<MusInstruments>
            {
                new MusInstruments
                {
                    Id = 1,
                    Name="Гитара Акустическая",
                    Description="Дрэдноут,ельб паллисандр",
                    Price =85, Image="Images/Guitar.jpg",
                    CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Strings")).Id
                },

                new MusInstruments
                {
                    Id = 2,
                    Name="Синезатор Casio",
                    Description="72 клавиши",
                    Price =68, Image="Images/Sint.jpg",
                    CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Keyboards")).Id
                },

                new MusInstruments
                {
                    Id = 3,
                    Name="Флейта",
                    Description="Латунь",
                    Price =55, Image="Images/Fl.jpg",
                    CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Airs")).Id
                },

                new MusInstruments
                {
                    Id = 4,
                    Name="Струны",
                    Description="Струны для гитары",
                    Price =73, Image="Images/strings.jpg",
                    CategoryId=_categories.Find(c=>c.NormalizedName.Equals("Assesories")).Id
                },
              
            };

        }
        public Task<ResponseData<MuzListModel<MusInstruments>>> GetInstListAsync(string? categoryNormalizedName, int pageNo = 1)
        {
            // Создать объект результата
            var result = new ResponseData<MuzListModel<MusInstruments>>();
            // Id категории для фильрации
            int? categoryId = null;
            // если требуется фильтрация, то найти Id категории
            // с заданным categoryNormalizedName
            if (categoryNormalizedName != null)
                categoryId = _categories.Find(c => c.NormalizedName.Equals(categoryNormalizedName))?.Id;
            // Выбрать объекты, отфильтрованные по Id категории,
            // если этот Id имеется
            var data = _mi.Where(d => categoryId == null || d.CategoryId.Equals(categoryId))?.ToList();

            // получить размер страницы из конфигурации
            int pageSize = _config.GetSection("ItemsPerPage").Get<int>();
            // получить общее количество страниц
            int totalPages = (int)Math.Ceiling(data.Count / (double)pageSize);
            // получить данные страницы
            var listData = new MuzListModel<MusInstruments>()
            {
                Items = data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбраннной категории";
            }
            // Вернуть результат
            return Task.FromResult(result);
            
        }

        public Task<ResponseData<MusInstruments>> GetInstByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateInstAsync(int id, MusInstruments mi, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }

        public Task DeleteInstAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseData<MusInstruments>> CreateInstAsync(MusInstruments mi, IFormFile? formFile)
        {
            throw new NotImplementedException();
        }
    }
}
