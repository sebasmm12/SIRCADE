using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Notifications.Entities;
using SIRCADE.ApiCore.Models.Roles.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.Users.Entities;

[Table("Usuarios")]
public class User
{
    public int Id { get; set; }

    public string NSA { get; set; } = default!;

    [Column("Contrasena")]
    public string Password { get; set; } = default!;

    public string Salt { get; set; } = default!;

    [Column("IdRol")]
    public int RoleId { get; set; }

    public Role Role { get; set; } = default!;

    public UserDetail Detail { get; set; } = default!;

    public ICollection<ScheduleProgramming>? ScheduleProgrammings { get; set; }

    public ICollection<ScheduleProgramming>? ScheduleProgrammingsRegister { get; set; }

    public ICollection<ScheduleProgramming>? ScheduleProgrammingsModify { get; set; }

    public ICollection<UserNotification>? SenderNotifications { get; set; }

    public ICollection<UserNotification>? ReceiverNotifications { get; set; }

    public string GetFullName()
    {
        return $"{Detail.Names} {Detail.PaternalLastName} {Detail.MaternalLastName}";
    }
}