using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.SchedulesProgramming.Persistence.Imp;

public class GetProgrammingTypesPersistence(ApplicationDbContext applicationDbContext) : IGetProgrammingTypesPersistence
{
    public async Task<IEnumerable<ProgrammingType>> ExecuteAsync()
    {
        var programmingTypes = await applicationDbContext
                                .ProgrammingTypes
                                .AsNoTracking()
                                .ToListAsync();

        return programmingTypes;
    }


    public async Task<ProgrammingType> ExecuteAsync(int programmingTypeId)
    {
        var programmingType = await applicationDbContext
                                        .ProgrammingTypes
                                        .AsNoTracking()
                                        .FirstOrDefaultAsync(programmingType => programmingType.Id == programmingTypeId);

        ArgumentNullException.ThrowIfNull(programmingType);

        return programmingType;
    }
}