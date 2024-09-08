using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies.Imp;

public class SchedulesProgrammingWeeklyStrategy : ISchedulesProgrammingInTimeStrategy
{
    public DashboardTimeType DashboardTimeType => DashboardTimeType.Weekly;

    public IQueryable<ScheduleProgramming> Execute(IQueryable<ScheduleProgramming> context)
    {
        var currentDate = DateTime.Now;

        var currentMonth = currentDate.Month;

        var currentDay = currentDate.DayOfYear;

        var currentYear = currentDate.Year;

        return context.Where(scheduleProgramming => scheduleProgramming.StartDate.Month == currentMonth &&
                                                    scheduleProgramming.StartDate.Year == currentYear &&
                                                    scheduleProgramming.StartDate.DayOfYear >= currentDay - 28 && 
                                                    scheduleProgramming.StartDate.DayOfYear <= currentDay);
    }
}