using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class JourneyDbContext : DbContext
{
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Activity> Activities { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=C:\\Users\\renan\\Downloads\\JourneyDatabase.db");
    }
}
