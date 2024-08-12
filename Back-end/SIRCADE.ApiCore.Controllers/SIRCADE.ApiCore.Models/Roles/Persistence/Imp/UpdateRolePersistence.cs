namespace SIRCADE.ApiCore.Models.Roles.Persistence.Imp;

public class UpdateRolePersistence(ApplicationDbContext applicationDbContext) : IUpdateRolePersistence
{
    public async Task ExecuteAsync()
    {
        await applicationDbContext.SaveChangesAsync();
    }
}