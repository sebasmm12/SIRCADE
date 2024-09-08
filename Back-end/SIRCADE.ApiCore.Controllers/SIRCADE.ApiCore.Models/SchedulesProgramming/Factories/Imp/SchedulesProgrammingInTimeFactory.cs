using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Factories.Imp;

public class SchedulesProgrammingInTimeFactory
    (IEnumerable<ISchedulesProgrammingInTimeStrategy> schedulesProgrammingInTimeStrategies): ISchedulesProgrammingInTimeFactory
{
    public ISchedulesProgrammingInTimeStrategy GetStrategyByTime(DashboardTimeType dashboardTimeType, IQueryable<ScheduleProgramming> context)
    {
        var strategy = schedulesProgrammingInTimeStrategies.FirstOrDefault(strategy => strategy.DashboardTimeType == dashboardTimeType);

        if (strategy is null)
            throw new("Strategy not found");

        return strategy;
    }
}