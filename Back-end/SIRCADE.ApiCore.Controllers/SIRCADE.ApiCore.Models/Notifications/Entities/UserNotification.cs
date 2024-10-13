using System.ComponentModel.DataAnnotations.Schema;
using SIRCADE.ApiCore.Models.Notifications.Enums;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Notifications.Entities;

[Table("NotificacionesUsuario")]
public class UserNotification
{
    public int Id { get; set; }

    [Column("IdNotificacion")]
    public int NotificationId { get; set; }

    [Column("IdUsuarioEmisor")]
    public int? SenderUserId { get; set; }

    [Column("IdUsuarioReceptor")]
    public int ReceiverUserId { get; set; }

    [Column("Asunto")]
    public string Subject { get; set; } = default!;

    [Column("Mensaje")]
    public string Message { get; set; } = default!;

    [Column("FechaEnvio")]
    public DateTime DeliveringDate { get; set; }

    [Column("Estado")]
    public NotificationStatus Status { get; set; }

    public Notification Notification { get; set; } = default!;

    public User? SenderUser { get; set; } = default!;

    public User ReceiverUser { get; set; } = default!;
}