using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Persistence.Imp;

public class CreateRolePersistence(ApplicationDbContext applicationDbContext) : ICreateRolePersistence
{
    public async Task<int> ExecuteAsync(Role role)
    {
        await applicationDbContext.AddAsync(role);

        await applicationDbContext.SaveChangesAsync();

        return role.Id;
    }
}