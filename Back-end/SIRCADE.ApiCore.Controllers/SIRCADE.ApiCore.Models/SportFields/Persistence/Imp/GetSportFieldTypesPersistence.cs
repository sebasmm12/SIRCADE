using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;

public class GetSportFieldTypesPersistence(ApplicationDbContext context) : IGetSportFieldTypesPersistence
{
    public async Task<IEnumerable<SportFieldType>> ExecuteAsync()
    {
        var sportFieldTypes = await context
                                .SportFieldTypes
                                .AsNoTracking()
                                .ToListAsync();

        return sportFieldTypes;
    }
}