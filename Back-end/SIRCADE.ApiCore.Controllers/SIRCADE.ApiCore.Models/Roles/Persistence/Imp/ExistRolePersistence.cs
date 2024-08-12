using Microsoft.EntityFrameworkCore;

namespace SIRCADE.ApiCore.Models.Roles.Persistence.Imp;

public class ExistRolePersistence(ApplicationDbContext applicationDbContext) : IExistRolePersistence
{
    public async Task<bool> ExecuteAsync(int roleId)
    {
        var exists = await applicationDbContext.Roles.AnyAsync(x => x.Id == roleId);
        
        return exists;
    }
}