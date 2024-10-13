using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Notifications.Entities;
using SIRCADE.ApiCore.Models.Permissions.Entities;
using SIRCADE.ApiCore.Models.RolePermissions.Entities;
using SIRCADE.ApiCore.Models.Roles.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.SportFields.Entities;
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

        modelBuilder
            .Entity<ScheduleProgramming>()
            .HasOne(scheduleProgramming => scheduleProgramming.RegisterUser)
            .WithMany(user => user.ScheduleProgrammingsRegister)
            .HasForeignKey(scheduleProgramming => scheduleProgramming.RegisterUserId);

        modelBuilder
            .Entity<ScheduleProgramming>()
            .HasOne(scheduleProgramming => scheduleProgramming.ModifyUser)
            .WithMany(user => user.ScheduleProgrammingsModify)
            .HasForeignKey(scheduleProgramming => scheduleProgramming.ModifyUserId);

        modelBuilder
            .Entity<ScheduleProgramming>()
            .HasOne(scheduleProgramming => scheduleProgramming.Client)
            .WithMany(user => user.ScheduleProgrammings)
            .HasForeignKey(scheduleProgramming => scheduleProgramming.ClientId);

        modelBuilder.Entity<Role>()
            .HasQueryFilter(x => x.Active);

        modelBuilder.Entity<ScheduleProgramming>()
            .HasQueryFilter(x => x.State != ScheduleProgrammingState.Cancelled);

        modelBuilder
            .Entity<UserNotification>()
            .HasOne(userNotification => userNotification.SenderUser)
            .WithMany(user => user.SenderNotifications)
            .HasForeignKey(userNotification => userNotification.SenderUserId);

        modelBuilder
            .Entity<UserNotification>()
            .HasOne(userNotification => userNotification.ReceiverUser)
            .WithMany(user => user.ReceiverNotifications)
            .HasForeignKey(userNotification => userNotification.ReceiverUserId);

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

    public DbSet<Notification> Notifications { get; set; } = default!;

    public DbSet<UserNotification> UserNotifications { get; set; } = default!;
}