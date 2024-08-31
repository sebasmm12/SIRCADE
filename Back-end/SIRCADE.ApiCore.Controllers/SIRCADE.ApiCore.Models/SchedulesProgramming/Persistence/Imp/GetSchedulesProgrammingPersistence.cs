using Microsoft.EntityFrameworkCore;
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
                                            .Where(scheduleProgramming => scheduleProgramming.StartDate >= schedulesProgrammingWeeklyQueries.StartDate &&
                                                                          scheduleProgramming.EndDate <= schedulesProgrammingWeeklyQueries.EndDate)
                                            .ToListAsync();

        return schedulesProgramming;
    }
}