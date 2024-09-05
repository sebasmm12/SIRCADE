using SIRCADE.ApiCore.Controllers.Dashboards.Responses;

namespace SIRCADE.ApiCore.Controllers.Dashboards.Services;

public interface IDashboardsService
{
    Task<DashboardWidgetsResponse> GetWidgetsAsync();
}