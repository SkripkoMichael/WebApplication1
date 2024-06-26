using WebApplication1.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
namespace WebApplication1.API.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<MusInstruments> MusInstruments { get; set; }
        public DbSet<Category> Categories { get; set; }
    }


}

