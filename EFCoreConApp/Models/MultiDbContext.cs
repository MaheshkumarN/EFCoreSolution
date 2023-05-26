using EFCoreConApp.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace EFCoreConApp.Models
{
  public class MultiDbContext : DbContext
  {
    public MultiDbContext(DbContextOptions<MultiDbContext> dbContextOptions) : base(dbContextOptions)
    { }

    public DbSet<Dept> Dept { get; set; }
    public DbSet<Emp> Emp { get; set; }
  }
}
