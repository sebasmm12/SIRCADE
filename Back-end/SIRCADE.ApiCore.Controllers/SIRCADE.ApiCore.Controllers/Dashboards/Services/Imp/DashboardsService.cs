using System.Security.Claims;
using SIRCADE.ApiCore.Controllers.Dashboards.Responses;
using SIRCADE.ApiCore.Models.Common.Extensions;
using SIRCADE.ApiCore.Models.Dashboards.Builders;
using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence;
using SIRCADE.ApiCore.Models.SportFields.Persistence;
using SIRCADE.ApiCore.Models.Users.Extensions;

namespace SIRCADE.ApiCore.Controllers.Dashboards.Services.Imp;

public class DashboardsService
    (IGetSchedulesProgrammingPersistence getSchedulesProgrammingPersistence, 
     IGetSportFieldTypesPersistence getSportFieldTypesPersistence,
     IHttpContextAccessor httpContextAccessor): IDashboardsService
{
    private const string ClientRole = "Socio";

    public async Task<DashboardWidgetsResponse> GetWidgetsAsync()
    {
        
        var clientId = GetClientId();

        var dashboardFiltersDto = new DashboardFiltersBuilder()
                                            .WithClientId(clientId)
                                            .WithTimeType(DashboardTimeType.CurrentMonth)
                                            .Build();   

        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(dashboardFiltersDto);

        var widgetsMonthly = GetWidgetsMonthly(reservations);

        return widgetsMonthly;
    }

    public async Task<IEnumerable<DashboardResponse>> GetReservationsAsync(ScheduleProgrammingState? reservationState)
    {
        var clientId = GetClientId();

        var dashboardFiltersDto = new DashboardFiltersBuilder()
                                            .WithClientId(clientId)
                                            .WithTimeType(DashboardTimeType.CurrentMonth)
                                            .WithState(reservationState)
                                            .IncludeSportType()
                                            .Build();

        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(dashboardFiltersDto);

        var sportFieldTypes = await getSportFieldTypesPersistence.ExecuteAsync();

        var reservationsResponse = sportFieldTypes.Select(sportFieldType => new DashboardResponse(sportFieldType.Name,
                                                                                    reservations.Count(reservation => reservation.SportField.SportFieldType.Id == sportFieldType.Id)));

        return reservationsResponse;
    }

    public async Task<IEnumerable<DashboardResponse>> GetReservationsByGradeAsync()
    {
        var dashboardFiltersDto = new DashboardFiltersBuilder()
                                            .WithTimeType(DashboardTimeType.CurrentMonth)
                                            .IncludeClientType()
                                            .Build();

        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(dashboardFiltersDto);

        var clientGrades = UsersExtensions.GetClientGrades();

        var reservationsResponse = clientGrades.Select(clientGrade => new DashboardResponse(clientGrade.Label,
                                                                                    reservations.Count(reservation => reservation.Client!.Detail.Grade == clientGrade.Id)));

        return reservationsResponse;
    }

    public async Task<IEnumerable<DashboardResponse>> GetReservationsByMonthsAsync()
    {
        var clientId = GetClientId();

        var dashboardFiltersDto = new DashboardFiltersBuilder()
                                            .WithClientId(clientId)
                                            .WithTimeType(DashboardTimeType.Monthly)
                                            .Build();

        var reservations = await getSchedulesProgrammingPersistence.ExecuteAsync(dashboardFiltersDto);

        var months = MonthsExtensions.GetMonths();

        var reservationsResponse = months.Select(month => new DashboardResponse(month.Label,
                                                                                    reservations.Count(reservation => reservation.StartDate.Month == month.Id)));

        return reservationsResponse;
    }

    #region private methods

    private int? GetClientId()
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

        return clientId;
    }

    private static DashboardWidgetsResponse GetWidgetsMonthly(IEnumerable<ScheduleProgramming> reservations)
    {
        var totalReservations = reservations.Count();

        var totalCancelledReservations = reservations.Count(reservation => reservation.State == ScheduleProgrammingState.Cancelled);

        var totalReScheduledReservation = reservations.Count(reservation => reservation.State == ScheduleProgrammingState.ReScheduled);

        return new(totalReservations, totalCancelledReservations, totalReScheduledReservation);
    }
    #endregion
}