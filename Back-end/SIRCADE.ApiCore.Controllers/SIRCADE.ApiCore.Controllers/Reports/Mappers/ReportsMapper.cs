using SIRCADE.ApiCore.Controllers.Reports.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Controllers.Reports.Mappers;

public static class ReportsMapper
{
    public static IEnumerable<FrequentlyUserByReservationResponse> MapToFrequentlyUsersByReservationResponse(this IEnumerable<User> users, IEnumerable<string> sportFieldTypes, IEnumerable<ScheduleProgrammingState> reservationStates)
    {
        var frequentlyUsersByReservation = users.Select(user => GetFrequentlyUserByReservation(user, sportFieldTypes, reservationStates));

        return frequentlyUsersByReservation;
    }

    private static FrequentlyUserByReservationResponse GetFrequentlyUserByReservation(User user,
        IEnumerable<string> sportFieldTypes, IEnumerable<ScheduleProgrammingState> reservationStates)
    {
        var reservations = user
                            .ScheduleProgrammings!
                            .Where(scheduleProgramming => reservationStates.Contains(scheduleProgramming.State));

        var sportFieldTypeQuantities = sportFieldTypes.Select(sportFieldType => new TypeQuantity(sportFieldType, 
                                                                                                           reservations.Count(reservation => reservation.SportField.SportFieldType.Name == sportFieldType)));
        return new(user.GetFullName(), sportFieldTypeQuantities);
    }
}