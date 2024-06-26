using WebApplication1.Domain.Models;
using WebApplication1.Domain.Entities;
namespace WebApplication1.Services;
    public class MemoryCategoryService: ICategoryService
{
    public Task<ResponseData<List<Category>>> GetCategoryListAsync()
    {
        var categories = new List<Category>
            {
                new Category {Id=1, Name="Клавишные",NormalizedName="Keyboards"},
                new Category {Id=2, Name="Струнные",NormalizedName="Strings"},
                new Category {Id=3, Name="Духовые",NormalizedName="Airs"},
                new Category {Id=4, Name="Аксессуары",NormalizedName="Assesories"},
            };

        var result = new ResponseData<List<Category>>();
        result.Data = categories;
        return Task.FromResult(result);
    }
}

