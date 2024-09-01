namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class UpdateScheduleProgrammingPersistence(ApplicationDbContext applicationDbContext)
    : IUpdateScheduleProgrammingPersistence
{
    public async Task ExecuteAsync()
    {
        await applicationDbContext.SaveChangesAsync();
    }
}