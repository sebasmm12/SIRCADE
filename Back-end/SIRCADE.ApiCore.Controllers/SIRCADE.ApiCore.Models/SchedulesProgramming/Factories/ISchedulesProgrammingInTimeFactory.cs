using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Factories;

public interface ISchedulesProgrammingInTimeFactory
{
    ISchedulesProgrammingInTimeStrategy GetStrategyByTime(DashboardTimeType dashboardTimeType, IQueryable<ScheduleProgramming> context);
}