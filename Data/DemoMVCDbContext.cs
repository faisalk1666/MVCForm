using DemoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoMVC.Data
{
    public class DemoMVCDbContext : DbContext
    {
        public DemoMVCDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<DemoMVC.Models.InfoModel> InfoModel { get; set; }
    }
}                                                   