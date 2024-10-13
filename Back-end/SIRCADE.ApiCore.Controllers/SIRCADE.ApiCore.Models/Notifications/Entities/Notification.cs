using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Notifications.Enums;

namespace SIRCADE.ApiCore.Models.Notifications.Entities;

[Table("Notificaciones")]
public class Notification
{
    public int Id { get; set; }

    [Column("Tipo")]
    public string Type { get; set; } = default!;

    [Column("Plantilla")]
    public string Template { get; set; } = default!;

    [Column("TipoEnvio")]
    public DeliveringType DeliveringType { get; set; }

    [Column("Asunto")]
    public string Subject { get; set; } = default!;

    public ICollection<UserNotification>? UserNotifications { get; set; }
}