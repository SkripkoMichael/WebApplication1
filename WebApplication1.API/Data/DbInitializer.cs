using WebApplication1.Domain.Entities;

namespace WebApplication1.API.Data
{
    public static class DbInitializer
    {
        public static async Task SeedData(WebApplication app)
        {
            // Uri проекта
            var uri = "https://localhost:7002/";
            // Получение контекста БД
            using var scope = app.Services.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            // Заполнение данными
            if (!context.Categories.Any() && !context.MusInstruments.Any())
            {
                var _categories = new Category[]
                {
                new Category {Id=1, Name="Клавишные",NormalizedName="Keyboards"},
                new Category {Id=2, Name="Струнные",NormalizedName="Strings"},
                new Category {Id=3, Name="Духовые",NormalizedName="Airs"},
                new Category {Id=4, Name="Аксессуары",NormalizedName="Assesories"},
                };
                await context.Categories.AddRangeAsync(_categories);
                await context.SaveChangesAsync();
                var _mi = new List<MusInstruments>
            {
                new MusInstruments
                {
                    
                    Name="Гитара Акустическая",
                    Description="Дрэдноут,ельб паллисандр",
                    Price =85, Image="Images/Guitar.jpg",
                    Category=_categories.FirstOrDefault(c=>c.NormalizedName.Equals("Strings"))
                },

                new MusInstruments
                {
                    
                    Name="Синезатор Casio",
                    Description="72 клавиши",
                    Price =68, Image="Images/Sint.jpg",
                    Category=_categories.FirstOrDefault(c=>c.NormalizedName.Equals("Keyboards"))
                },

                new MusInstruments
                {
                    
                    Name="Флейта",
                    Description="Латунь",
                    Price =55, Image="Images/Fl.jpg",
                    Category=_categories.FirstOrDefault(c=>c.NormalizedName.Equals("Airs"))
                },

                new MusInstruments
                {
                   
                    Name="Струны",
                    Description="Струны для гитары",
                    Price =73, Image="Images/strings.jpg",
                    Category=_categories.FirstOrDefault(c=>c.NormalizedName.Equals("Assesories"))
                },

            };

                await context.AddRangeAsync(_mi);
                await context.SaveChangesAsync();
            }
        }
    }
}
