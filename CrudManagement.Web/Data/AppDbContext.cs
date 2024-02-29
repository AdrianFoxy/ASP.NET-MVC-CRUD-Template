using CrudManagement.Web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace CrudManagement.Web.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
        {    
        }

        public DbSet<Worker> Worker { get; set; }

    }
}
