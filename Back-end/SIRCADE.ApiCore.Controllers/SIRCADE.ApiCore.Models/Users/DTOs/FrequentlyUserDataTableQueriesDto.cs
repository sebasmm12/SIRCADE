using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Models.Users.DTOs;

public record FrequentlyUserDataTableQueriesDto(IEnumerable<ScheduleProgrammingState> ReservationStates) : UserDataTableQueriesDto;