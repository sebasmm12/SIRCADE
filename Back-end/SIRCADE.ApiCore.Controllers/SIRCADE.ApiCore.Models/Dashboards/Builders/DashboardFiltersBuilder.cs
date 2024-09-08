using SIRCADE.ApiCore.Models.Dashboards.DTOs;
using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Models.Dashboards.Builders;

public class DashboardFiltersBuilder
{
    private DashboardFiltersDto dashboardFilters = new();


    public DashboardFiltersBuilder WithClientId(int? clientId)
    {
        dashboardFilters.ClientId = clientId;

        return this;
    }

    public DashboardFiltersBuilder WithState(ScheduleProgrammingState? state)
    {
        dashboardFilters.State = state;

        return this;
    }

    public DashboardFiltersBuilder IncludeSportType()
    {
        dashboardFilters.IsSportTypeIncluded = true;

        return this;
    }

    public DashboardFiltersBuilder IncludeClientType()
    {
        dashboardFilters.IsClientTypeIncluded = true;

        return this;
    }

    public DashboardFiltersBuilder WithTimeType(DashboardTimeType timeType)
    {
        dashboardFilters.TimeType = timeType;

        return this;
    }

    public DashboardFiltersDto Build()
    {
        try
        {
            return dashboardFilters;
        }
        finally
        {
            dashboardFilters = new();
        }
       
    }
}