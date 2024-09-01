namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Responses;

public record ScheduleProgrammingInfoResponse(
    int Id,
    int SportFieldId,
    string SportFieldName,
    int? ClientId,
    string? ClientName,
    DateTime StartDate,
    DateTime EndDate,
    string? Comment,
    string RegisterUser,
    int Type,
    string TypeName,
    string LightColor,
    string DarkColor);