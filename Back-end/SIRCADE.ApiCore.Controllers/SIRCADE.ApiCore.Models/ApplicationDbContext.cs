using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Permissions.Entities;
using SIRCADE.ApiCore.Models.RolePermissions.Entities;
using SIRCADE.ApiCore.Models.Roles.Entities;
using SIRCADE.ApiCore.Models.Unities.Entities;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models;

public class ApplicationDbContext(DbContextOptions options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<RolePermission>()
            .HasKey(rp => new { rp.RoleId, rp.PermissionId });
        
        modelBuilder
            .Entity<User>()
            .HasOne(user => user.Detail)
            .WithOne(detail => detail.User)
            .HasForeignKey<UserDetail>(detail => detail.Id);
    }

    public DbSet<User> Users { get; set; }

    public DbSet<Role> Roles { get; set; }

    public DbSet<Permission> Permissions { get; set; }

    public DbSet<RolePermission> RolePermissions { get; set; }

    public DbSet<Unity> Unities { get; set; }

    public DbSet<UserDetail> UserDetails { get; set; }
}