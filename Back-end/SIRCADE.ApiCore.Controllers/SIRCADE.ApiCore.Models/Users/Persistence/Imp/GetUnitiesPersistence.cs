using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence.Imp;

public class GetUnitiesPersistence(ApplicationDbContext applicationDbContext) : IGetUnitiesPersistence
{
    public async Task<IEnumerable<Unity>> ExecuteAsync()
    {
        var unities = await applicationDbContext.Unities.ToListAsync();

        return unities;
    }
}