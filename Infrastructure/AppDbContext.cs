using Microsoft.EntityFrameworkCore;

namespace VehiclesWebApp.Infrastructure;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<VehicleDataModel> Vehicles { get; set; }
}
