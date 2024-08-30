using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class CreateScheduleProgrammingPersistence(ApplicationDbContext applicationDbContext) : ICreateScheduleProgrammingPersistence
{
    public async Task<int> ExecuteAsync(ScheduleProgramming scheduleProgramming)
    {
        await applicationDbContext.AddAsync(scheduleProgramming);

        await applicationDbContext.SaveChangesAsync();

        return scheduleProgramming.Id;
    }
}
