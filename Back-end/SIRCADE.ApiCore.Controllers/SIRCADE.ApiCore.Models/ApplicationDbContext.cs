using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Permissions.Entities;
using SIRCADE.ApiCore.Models.RolePermissions.Entities;
using SIRCADE.ApiCore.Models.Roles.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SportFields.Entities;
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

        modelBuilder
            .Entity<SportField>()
            .HasOne(sportField => sportField.SportFieldType)
            .WithMany(sportFieldType => sportFieldType.SportFields)
            .HasForeignKey(sportField => sportField.Type);


        modelBuilder
            .Entity<ScheduleProgramming>()
            .HasOne(scheduleProgramming => scheduleProgramming.ProgrammingType)
            .WithMany(programmingType => programmingType.SchedulesProgramming)
            .HasForeignKey(scheduleProgramming => scheduleProgramming.Type);

        modelBuilder.Entity<Role>()
            .HasQueryFilter(x => x.Active);

    }

    public DbSet<User> Users { get; set; } = default!;

    public DbSet<Role> Roles { get; set; } = default!;

    public DbSet<Permission> Permissions { get; set; } = default!;

    public DbSet<RolePermission> RolePermissions { get; set; } = default!;

    public DbSet<Unity> Unities { get; set; } = default!;

    public DbSet<UserDetail> UserDetails { get; set; } = default!;

    public DbSet<SportFieldType> SportFieldTypes { get; set; } = default!;

    public DbSet<SportField> SportFields { get; set; } = default!;

    public DbSet<ScheduleProgramming> SchedulesProgramming { get; set; } = default!;

    public DbSet<ProgrammingType> ProgrammingTypes { get; set; } = default!;
}