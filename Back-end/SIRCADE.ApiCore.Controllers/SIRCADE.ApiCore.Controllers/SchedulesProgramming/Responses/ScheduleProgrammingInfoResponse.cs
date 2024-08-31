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
    int Type,
    string TypeName);