namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;

public record ScheduleProgrammingRegisterRequest(
    int SportFieldId,
    DateTime StartDate,
    DateTime EndDate,
    int? ClientId,
    int Type,
    string Comment);