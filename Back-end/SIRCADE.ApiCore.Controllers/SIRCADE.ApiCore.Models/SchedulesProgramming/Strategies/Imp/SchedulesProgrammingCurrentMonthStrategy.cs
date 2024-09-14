﻿using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies.Imp;

public class SchedulesProgrammingCurrentMonthStrategy : ISchedulesProgrammingInTimeStrategy
{
    public IQueryable<ScheduleProgramming> Execute(IQueryable<ScheduleProgramming> context, object? filters = null)
    {
        var currentDate = DateTime.Now;

        var currentMonth = currentDate.Month;

        var currentYear = currentDate.Year;

        return context.Where(scheduleProgramming => scheduleProgramming.StartDate.Month == currentMonth &&
                                                    scheduleProgramming.StartDate.Year == currentYear);
    }

    public DashboardTimeType DashboardTimeType => DashboardTimeType.CurrentMonth;
}