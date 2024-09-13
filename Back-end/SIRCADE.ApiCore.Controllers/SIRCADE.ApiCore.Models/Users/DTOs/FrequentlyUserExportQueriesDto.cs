using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Models.Users.DTOs;

public record FrequentlyUserExportQueriesDto(
    string ReportTitle, 
    IEnumerable<ScheduleProgrammingState> ReservationStates) : FrequentlyUserDataTableQueriesDto(ReservationStates);