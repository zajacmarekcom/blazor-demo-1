using BlazorYoutube.Web.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorYoutube.Web.Database;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
    }
    
    public DbSet<TaskEntity> Tasks { get; set; }
}