namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;

public record ScheduleProgrammingFiltersDto(
    int SportFieldId,
    DateTime StartDate,
    DateTime EndDate,
    int Type,
    string RestrictedType);