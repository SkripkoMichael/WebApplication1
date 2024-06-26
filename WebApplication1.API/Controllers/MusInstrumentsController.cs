using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.API.Data;
using WebApplication1.Domain.Entities;
using WebApplication1.Domain.Models;

namespace WebApplication1.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusInstrumentsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _env;
        public MusInstrumentsController(AppDbContext context, IWebHostEnvironment env)
        {
            _context = context;
            _env = env;
        }

        // GET: api/MusInstruments
        [HttpGet]
        public async Task<ActionResult<ResponseData<MuzListModel<MusInstruments>>>> GetFlower(string? category, int pageNo = 1, int pageSize = 3)
        {
            // Создать объект результата
            var result = new ResponseData<MuzListModel<MusInstruments>>();
            // Фильтрация по категории загрузка данных категории
            var data = _context.MusInstruments.Include(d => d.Category).Where(d => String.IsNullOrEmpty(category) || d.Category.NormalizedName.Equals(category));
            // Подсчет общего количества страниц
            int totalPages = (int)Math.Ceiling(data.Count() / (double)pageSize);
            if (pageNo > totalPages)
                pageNo = totalPages;
            // Создание объекта ProductListModel с нужной страницей данных
            var listData = new MuzListModel<MusInstruments>()
            {
                Items = await data.Skip((pageNo - 1) * pageSize).Take(pageSize).ToListAsync(),
                CurrentPage = pageNo,
                TotalPages = totalPages
            };
            // поместить данные в объект результата
            result.Data = listData;
            // Если список пустой
            if (data.Count() == 0)
            {
                result.Success = false;
                result.ErrorMessage = "Нет объектов в выбранной категории";
            }
            return result;
        }

        // GET: api/MusInstruments/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MusInstruments>> GetMusInstruments(int id)
        {
            var musInstruments = await _context.MusInstruments.FindAsync(id);

            if (musInstruments == null)
            {
                return NotFound();
            }

            return musInstruments;
        }

        // PUT: api/MusInstruments/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMusInstruments(int id, MusInstruments musInstruments)
        {
            if (id != musInstruments.Id)
            {
                return BadRequest();
            }

            _context.Entry(musInstruments).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MusInstrumentsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/MusInstruments
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MusInstruments>> PostMusInstruments(MusInstruments musInstruments)
        {
            _context.MusInstruments.Add(musInstruments);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMusInstruments", new { id = musInstruments.Id }, musInstruments);
        }

        // DELETE: api/MusInstruments/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMusInstruments(int id)
        {
            var musInstruments = await _context.MusInstruments.FindAsync(id);
            if (musInstruments == null)
            {
                return NotFound();
            }

            _context.MusInstruments.Remove(musInstruments);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MusInstrumentsExists(int id)
        {
            return _context.MusInstruments.Any(e => e.Id == id);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveImage(int id, IFormFile image)
        {
            // Найти объект по Id
            var mi = await _context.MusInstruments.FindAsync(id);
            if (mi == null)
            {
                return NotFound();
            }

            // Путь к папке wwwroot/Images
            var imagesPath = Path.Combine(_env.WebRootPath, "Images");
            // получить случайное имя файла
            var randomName = Path.GetRandomFileName();
            // получить расширение в исходном файле
            var extension = Path.GetExtension(image.FileName);
            // задать в новом имени расширение как в исходном файле
            var fileName = Path.ChangeExtension(randomName, extension);
            // полный путь к файлу
            var filePath = Path.Combine(imagesPath, fileName);
            // создать файл и открыть поток для чтения
            using var stream = System.IO.File.OpenWrite(filePath);
            // скопировать файл в поток
            await image.CopyToAsync(stream);
            // получить Url хоста
            var host = "https://" + Request.Host;
            // Url файла изображения
            var url = $"{host}/Images/{fileName}";
            // Сохранить url файла в объекте
            mi.Image = url;
            await _context.SaveChangesAsync();
            return Ok();

        }
    }
}
