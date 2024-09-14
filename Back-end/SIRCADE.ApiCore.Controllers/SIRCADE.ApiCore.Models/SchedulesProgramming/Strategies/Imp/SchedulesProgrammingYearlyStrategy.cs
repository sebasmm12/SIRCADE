using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies.Imp;

public class SchedulesProgrammingYearlyStrategy : ISchedulesProgrammingInTimeStrategy
{
    public IQueryable<ScheduleProgramming> Execute(IQueryable<ScheduleProgramming> context, object? filters = null)
    {
        var currentDate = DateTime.Now;

        var currentYear = currentDate.Year;

        const int totalYears = 5;

        return context.Where(scheduleProgramming => scheduleProgramming.StartDate.Year >= currentYear - totalYears);
    }

    public DashboardTimeType DashboardTimeType => DashboardTimeType.Yearly;
}