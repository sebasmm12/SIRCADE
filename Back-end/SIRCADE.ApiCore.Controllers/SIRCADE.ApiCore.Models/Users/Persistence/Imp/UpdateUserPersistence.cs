using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence.Imp;

public class UpdateUserPersistence(ApplicationDbContext applicationDbContext) : IUpdateUsersPersistence
{
    public async Task ExecuteAsync(User user)
    {
        applicationDbContext.Update(user);

        await applicationDbContext.SaveChangesAsync();
    }
}