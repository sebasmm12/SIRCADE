namespace SIRCADE.ApiCore.Controllers.SchedulesProgramming.Requests;

public record ScheduleProgrammingUpdateRequest(
    int Id,
    DateTime StartDate,
    DateTime EndDate);