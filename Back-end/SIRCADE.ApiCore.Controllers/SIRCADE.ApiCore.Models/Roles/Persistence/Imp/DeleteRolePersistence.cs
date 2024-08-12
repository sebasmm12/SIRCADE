using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Persistence.Imp;

public class DeleteRolePersistence(ApplicationDbContext applicationDbContext) : IDeleteRolePersistence
{

    public async Task ExecuteAsync(Role role)
    {
        applicationDbContext.Remove(role);

        await applicationDbContext.SaveChangesAsync();
    }
}