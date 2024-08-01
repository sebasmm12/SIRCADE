using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{

    public DbSet<User> Users { get; set; }
}