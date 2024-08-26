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
}