using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Dashboards.DTOs;
using SIRCADE.ApiCore.Models.Dashboards.Enums;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Factories;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class GetSchedulesProgrammingPersistence(
    ApplicationDbContext applicationDbContext,
    ISchedulesProgrammingInTimeFactory schedulesProgrammingInTimeFactory) : IGetSchedulesProgrammingPersistence
{
    private const string ReservationType = "Reserva";

    public async Task<IEnumerable<ScheduleProgramming>> ExecuteAsync(SchedulesProgrammingWeeklyQueries schedulesProgrammingWeeklyQueries)
    {
        var schedulesProgramming = await applicationDbContext
                                            .SchedulesProgramming
                                            .Include(scheduleProgramming => scheduleProgramming.SportField)
                                            .Include(scheduleProgramming => scheduleProgramming.ProgrammingType)
                                            .Include(scheduleProgramming => scheduleProgramming.Client)
                                            .ThenInclude(client => client.Detail)
                                            .Include(scheduleProgramming => scheduleProgramming.RegisterUser)
                                            .ThenInclude(registerUser => registerUser.Detail)
                                            .Where(scheduleProgramming => scheduleProgramming.StartDate >= schedulesProgrammingWeeklyQueries.StartDate &&
                                                                          scheduleProgramming.EndDate <= schedulesProgrammingWeeklyQueries.EndDate)
                                            .ToListAsync();

        return schedulesProgramming;
    }

    public async Task<IEnumerable<ScheduleProgramming>> ExecuteAsync(DashboardFiltersDto dashboardFilters)
    {
        var schedulesProgrammingContext = applicationDbContext
                                                .SchedulesProgramming
                                                .Include(scheduleProgramming => scheduleProgramming.ProgrammingType)
                                                .IgnoreQueryFilters()
                                                .AsQueryable();

        schedulesProgrammingContext = ResolverFiltersForDashboards(schedulesProgrammingContext, dashboardFilters);

        var schedulesProgramming = await schedulesProgrammingContext.ToListAsync();

        return schedulesProgramming;
    }

    public async Task<IEnumerable<ScheduleProgramming>> ExecuteAsync(DashboardTimeType dashboardTimeType, object? filters = null, bool canIgnoreQueryFilters = true)
    {
        var schedulesProgrammingContext = applicationDbContext
                                            .SchedulesProgramming
                                            .Where(scheduleProgramming => scheduleProgramming.ClientId != null)
                                            .Include(scheduleProgramming => scheduleProgramming.SportField)
                                            .AsQueryable();

        var schedulesProgrammingByTimeStrategy = schedulesProgrammingInTimeFactory.GetStrategyByTime(dashboardTimeType, schedulesProgrammingContext);

        schedulesProgrammingContext = schedulesProgrammingByTimeStrategy.Execute(schedulesProgrammingContext, filters);

        if (canIgnoreQueryFilters)
            schedulesProgrammingContext = schedulesProgrammingContext.IgnoreQueryFilters();

        var schedulesProgramming = await schedulesProgrammingContext.ToListAsync();

        return schedulesProgramming;
    }

    public async Task<ScheduleProgramming> ExecuteAsync(int scheduleProgrammingId, bool needsInclude = false,
        bool isTracked = false)
    {
        var schedulesProgrammingContext = applicationDbContext.SchedulesProgramming.AsQueryable();

        if (needsInclude)
            schedulesProgrammingContext = schedulesProgrammingContext
                                            .Include(scheduleProgramming => scheduleProgramming.SportField)
                                            .Include(scheduleProgramming => scheduleProgramming.ProgrammingType)
                                            .Include(scheduleProgramming => scheduleProgramming.Client)
                                            .ThenInclude(client => client.Detail);

        if (!isTracked)
            schedulesProgrammingContext = schedulesProgrammingContext.AsNoTracking();

        var scheduleProgramming = await schedulesProgrammingContext.FirstOrDefaultAsync(scheduleProgramming => scheduleProgramming.Id == scheduleProgrammingId);

        ArgumentNullException.ThrowIfNull(scheduleProgramming);

        return scheduleProgramming;
    }


    public async Task<ScheduleProgramming?> ExecuteAsync(ScheduleProgrammingFiltersDto scheduleProgrammingFiltersDto)
    {
        var scheduleProgramming = await applicationDbContext
                                            .SchedulesProgramming
                                            .Include(scheduleProgramming => scheduleProgramming.ProgrammingType)
                                            .FirstOrDefaultAsync(scheduleProgramming => scheduleProgramming.SportFieldId == scheduleProgrammingFiltersDto.SportFieldId
                                                                     && (scheduleProgramming.StartDate >= scheduleProgrammingFiltersDto.StartDate
                                                                         && scheduleProgramming.EndDate <= scheduleProgrammingFiltersDto.EndDate));

        return scheduleProgramming;
    }

    #region private methods

    private IQueryable<ScheduleProgramming> ResolverFiltersForDashboards(IQueryable<ScheduleProgramming> schedulesProgrammingContext, DashboardFiltersDto dashboardFilters)
    {
        schedulesProgrammingContext = schedulesProgrammingContext
            .Where(scheduleProgramming => scheduleProgramming.ProgrammingType.Name == ReservationType);

        if (dashboardFilters.ClientId.HasValue)
            schedulesProgrammingContext = schedulesProgrammingContext.Where(scheduleProgramming => scheduleProgramming.ClientId == dashboardFilters.ClientId);

        if(dashboardFilters.State.HasValue)
            schedulesProgrammingContext = schedulesProgrammingContext.Where(scheduleProgramming => scheduleProgramming.State == dashboardFilters.State);

        if(dashboardFilters.IsSportTypeIncluded)
            schedulesProgrammingContext = schedulesProgrammingContext
                                            .Include(scheduleProgramming => scheduleProgramming.SportField)
                                            .ThenInclude(sportField => sportField.SportFieldType);

        if(dashboardFilters.IsClientTypeIncluded)
            schedulesProgrammingContext = schedulesProgrammingContext
                                            .Include(scheduleProgramming => scheduleProgramming.Client)
                                            .ThenInclude(client => client.Detail);

        var schedulesProgrammingByTimeStrategy = schedulesProgrammingInTimeFactory.GetStrategyByTime(dashboardFilters.TimeType, schedulesProgrammingContext);

        schedulesProgrammingContext = schedulesProgrammingByTimeStrategy.Execute(schedulesProgrammingContext);

        return schedulesProgrammingContext;
    }
    #endregion
}