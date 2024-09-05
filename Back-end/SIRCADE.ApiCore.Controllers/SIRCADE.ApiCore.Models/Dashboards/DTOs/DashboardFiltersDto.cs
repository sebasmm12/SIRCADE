using SIRCADE.ApiCore.Models.Dashboards.Enums;

namespace SIRCADE.ApiCore.Models.Dashboards.DTOs;

public class DashboardFiltersDto
{
    public int? ClientId { get; set; }

    public bool IsSportTypeIncluded { get; set; }

    public bool IsClientTypeIncluded { get; set; }

    public DashboardTimeType TimeType { get; set; } = DashboardTimeType.Monthly;
}