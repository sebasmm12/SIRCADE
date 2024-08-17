using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence.Imp;

public class CreateUserPersistence(ApplicationDbContext applicationDbContext) : ICreateUserPersistence
{
    public async Task<int> ExecuteAsync(User user)
    {
        await applicationDbContext.AddAsync(user);

        await applicationDbContext.SaveChangesAsync();

        return user.Id;
    }
}