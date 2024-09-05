using System.Security.Claims;
using SIRCADE.ApiCore.Controllers.Dashboards.Responses;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;

namespace SIRCADE.ApiCore.Controllers.Dashboards.Services.Imp;

public class DashboardsService
    (IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence, 
     IHttpContextAccessor httpContextAccessor): IDashboardsService
{
    private const string ClientRole = "Cliente";

    public async Task<DashboardWidgetsResponse> GetWidgetsAsync()
    {
        var userClaims = httpContextAccessor
                                .HttpContext!
                                .User;

        int? clientId = null;

        var userRole = userClaims
                            .Claims
                            .FirstOrDefault(claim => claim.Type == ClaimTypes.Role)?
                            .Value;

        if (userRole == ClientRole)
            clientId = Convert.ToInt32(userClaims
                                        .Claims
                                        .FirstOrDefault(claim => claim.Type == ClaimTypes.NameIdentifier)?
                                        .Value);


        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(clientId);

        var widgetsMonthly = GetWidgetsMonthly(reservations);

        return widgetsMonthly;
    }




    #region private methods

    private static DashboardWidgetsResponse GetWidgetsMonthly(IEnumerable<ScheduleProgramming> reservations)
    {
        var totalReservations = reservations.Count();

        var totalCancelledReservations = reservations.Count(reservation => reservation.State == ScheduleProgrammingState.Cancelled);

        var totalReScheduledReservation = reservations.Count(reservation => reservation.State == ScheduleProgrammingState.ReScheduled);

        return new(totalReservations, totalCancelledReservations, totalReScheduledReservation);
    }
    #endregion
}