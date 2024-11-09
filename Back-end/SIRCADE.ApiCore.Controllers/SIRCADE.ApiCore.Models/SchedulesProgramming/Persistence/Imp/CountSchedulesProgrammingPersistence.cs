using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Dtos;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class CountSchedulesProgrammingPersistence(ApplicationDbContext applicationDbContext) : ICountSchedulesProgrammingPersistence
{
    public async Task<int> ExecuteAsync(int clientId, DateTime startDate, int? scheduleProgrammingId)
    {
        var totalSchedulesProgramming = await applicationDbContext
                                            .SchedulesProgramming
                                            .AsNoTracking()
                                            .CountAsync(scheduleProgramming => scheduleProgramming.ClientId == clientId &&
                                                                            scheduleProgramming.StartDate.Date == startDate.Date &&
                                                                            scheduleProgramming.Id != scheduleProgrammingId);

        return totalSchedulesProgramming;
    }

    public async Task<int> ExecuteAsync(OverlappedScheduleProgrammingFiltersDto overlappedScheduleProgrammingFiltersDto)
    {
        var totalSchedulesProgramming = await applicationDbContext
            .SchedulesProgramming
            .Include(scheduleProgramming => scheduleProgramming.Client)
            .ThenInclude(client => client.Detail)
            .CountAsync(scheduleProgramming =>
                scheduleProgramming.SportFieldId == overlappedScheduleProgrammingFiltersDto.SportFieldId
                && ((scheduleProgramming.StartDate < overlappedScheduleProgrammingFiltersDto.EndDate &&
                     scheduleProgramming.EndDate > overlappedScheduleProgrammingFiltersDto.EndDate) ||
                    (scheduleProgramming.StartDate < overlappedScheduleProgrammingFiltersDto.StartDate &&
                     scheduleProgramming.EndDate > overlappedScheduleProgrammingFiltersDto.StartDate) ||
                    (overlappedScheduleProgrammingFiltersDto.StartDate < scheduleProgramming.EndDate &&
                     overlappedScheduleProgrammingFiltersDto.EndDate > scheduleProgramming.EndDate) ||
                    (overlappedScheduleProgrammingFiltersDto.StartDate < scheduleProgramming.StartDate &&
                     overlappedScheduleProgrammingFiltersDto.EndDate > scheduleProgramming.StartDate) ||
                    (scheduleProgramming.StartDate == overlappedScheduleProgrammingFiltersDto.StartDate &&
                     scheduleProgramming.EndDate == overlappedScheduleProgrammingFiltersDto.EndDate)));

        return totalSchedulesProgramming;
    }
}