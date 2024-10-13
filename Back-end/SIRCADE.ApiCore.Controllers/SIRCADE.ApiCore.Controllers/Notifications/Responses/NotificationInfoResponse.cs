using SIRCADE.ApiCore.Models.Notifications.Enums;

namespace SIRCADE.ApiCore.Controllers.Notifications.Responses;

public record NotificationInfoResponse(
    int Id,
    string Subject,
    string Message, 
    DateTime DeliveringDate, 
    NotificationStatus Status);