using System.Text.Json;
using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Strategies.Imp;

public class SchedulesProgrammingByTurnStrategy : ISchedulesProgrammingInTimeStrategy
{
    public IQueryable<ScheduleProgramming> Execute(IQueryable<ScheduleProgramming> context, object? filters = null)
    {
        var dateFilters = JsonSerializer.Deserialize<ScheduleProgrammingByTurnDto>(JsonSerializer.Serialize(filters))!;

        if (dateFilters.StartDate is null && dateFilters.EndDate is null)
        {
            var currentYear = DateTime.Now.Year;

            return context.Where(scheduleProgramming => scheduleProgramming.StartDate.Year == currentYear);

        }

        if (dateFilters.StartDate is not null)
        {
            context = context.Where(scheduleProgramming => scheduleProgramming.StartDate >= dateFilters.StartDate.Value.Date);
        }

        if (dateFilters.EndDate is not null)
        {
            context = context.Where(scheduleProgramming => scheduleProgramming.StartDate <= dateFilters.EndDate.Value.Date);
        }

        return context;
    }

    public DashboardTimeType DashboardTimeType => DashboardTimeType.Turn;
}