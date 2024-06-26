using WebApplication1.Domain.Models;
using WebApplication1.Domain.Entities;
namespace WebApplication1.Services
{
    public interface ICategoryService
    {
        public Task<ResponseData<List<Category>>> GetCategoryListAsync();
    }
}
