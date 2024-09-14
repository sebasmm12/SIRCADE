using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.DTOs;

public record SportFieldReservationsDto(string SportFieldType, IEnumerable<ScheduleProgramming> Reservations);