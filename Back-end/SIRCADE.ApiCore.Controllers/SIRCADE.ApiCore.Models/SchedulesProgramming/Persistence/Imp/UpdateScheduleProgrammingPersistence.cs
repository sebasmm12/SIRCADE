using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Enums;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class UpdateScheduleProgrammingPersistence(ApplicationDbContext applicationDbContext)
    : IUpdateScheduleProgrammingPersistence
{
    public async Task ExecuteAsync()
    {
        await applicationDbContext.SaveChangesAsync();
    }

    public async Task ExecuteAsync(IEnumerable<int> scheduleProgrammingIds, ScheduleProgrammingState state)
    {
        await using var transaction = await applicationDbContext.Database.BeginTransactionAsync();

        try
        {
            await applicationDbContext.SchedulesProgramming
                .Where(scheduleProgramming => scheduleProgrammingIds.Contains(scheduleProgramming.Id))
                .ExecuteUpdateAsync(scheduleProgramming =>
                    scheduleProgramming
                        .SetProperty(property => property.State, property => state)
                        .SetProperty(property => property.ModifyDate, property => DateTime.UtcNow.AddHours(-5)));

            await applicationDbContext.SaveChangesAsync();

            await transaction.CommitAsync();

        }
        catch (Exception)
        {
            await transaction.RollbackAsync();

            throw;
        }
    }
}