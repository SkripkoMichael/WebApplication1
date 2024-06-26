﻿using WebApplication1.Domain.Models;
using WebApplication1.Domain.Entities;
namespace WebApplication1.Services
{
    public interface IMuzInstrumentService
    {/// <summary>
     /// Получение списка всех объектов
     /// </summary>
     /// <param name="categoryNormalizedName">нормализованное имя категории для фильтрации</param>
     /// <param name="pageNo">номер страницы списка</param>
     /// <returns></returns>
        public Task<ResponseData<MuzListModel<MusInstruments>>> GetInstListAsync(string? categoryNormalizedName, int pageNo = 1);
        /// <summary>
        /// Поиск объекта по Id
        /// </summary>
        /// <param name="id">Идентификатор объекта</param>
        /// <returns>Найденный объект или null, если объект не найден</returns>
        public Task<ResponseData<MusInstruments>> GetInstByIdAsync(int id);
        /// <summary>
        /// Обновление объекта
        /// </summary>
        /// <param name="id">Id изменяемомго объекта</param>
        /// <param name="mi">объект с новыми параметрами</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns></returns>
        public Task UpdateInstAsync(int id, MusInstruments mi, IFormFile? formFile);
        /// <summary>
        /// Удаление объекта
        /// </summary>
        /// <param name="id">Id удаляемомго объекта</param>
        /// <returns></returns>
        public Task DeleteInstAsync(int id);
        /// <summary>
        /// Создание объекта
        /// </summary>
        /// <param name="flowers">Новый объект</param>
        /// <param name="formFile">Файл изображения</param>
        /// <returns>Созданный объект</returns>
        public Task<ResponseData<MusInstruments>> CreateInstAsync(MusInstruments mi, IFormFile? formFile);
    }
}
