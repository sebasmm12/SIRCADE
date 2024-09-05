namespace SIRCADE.ApiCore.Controllers.Dashboards.Responses;

public record DashboardWidgetsResponse(
    int TotalReservations,
    int TotalCancelledReservations,
    int TotalRescheduledReservations);