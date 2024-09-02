using Microsoft.EntityFrameworkCore;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class CountSchedulesProgrammingPersistence(ApplicationDbContext applicationDbContext) : ICountSchedulesProgrammingPersistence
{
    public async Task<int> ExecuteAsync(int clientId, DateTime startDate)
    {
        var totalSchedulesProgramming = await applicationDbContext
                                            .SchedulesProgramming
                                            .AsNoTracking()
                                            .CountAsync(scheduleProgramming => scheduleProgramming.ClientId == clientId &&
                                                                            scheduleProgramming.StartDate.Date == startDate.Date);

        return totalSchedulesProgramming;
    }
}