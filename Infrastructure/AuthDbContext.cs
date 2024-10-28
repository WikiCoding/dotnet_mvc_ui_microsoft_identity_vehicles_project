using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VehiclesWebApp.Infrastructure;

public class AuthDbContext(DbContextOptions<AuthDbContext> options) : IdentityDbContext(options)
{
}
