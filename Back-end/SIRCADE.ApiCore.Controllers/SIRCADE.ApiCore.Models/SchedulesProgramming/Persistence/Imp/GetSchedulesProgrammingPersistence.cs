using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Queries;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class GetSchedulesProgrammingPersistence(ApplicationDbContext applicationDbContext) : IGetSchedulesProgrammingPersistence
{
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
}