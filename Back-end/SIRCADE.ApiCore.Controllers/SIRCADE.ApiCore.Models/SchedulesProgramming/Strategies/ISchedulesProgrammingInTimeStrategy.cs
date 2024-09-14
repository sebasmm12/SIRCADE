using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies;

public interface ISchedulesProgrammingInTimeStrategy
{
    IQueryable<ScheduleProgramming> Execute(IQueryable<ScheduleProgramming> context, object? filters = null);

    DashboardTimeType DashboardTimeType { get;}
}