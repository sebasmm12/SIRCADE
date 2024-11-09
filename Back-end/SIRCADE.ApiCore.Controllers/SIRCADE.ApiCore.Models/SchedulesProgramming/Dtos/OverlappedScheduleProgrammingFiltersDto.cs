namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;

public record OverlappedScheduleProgrammingFiltersDto(
    int SportFieldId,
    DateTime StartDate,
    DateTime EndDate);