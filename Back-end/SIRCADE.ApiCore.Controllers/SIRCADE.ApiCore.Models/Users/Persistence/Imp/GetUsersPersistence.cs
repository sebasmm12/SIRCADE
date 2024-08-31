using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence.Imp;

public class GetUsersPersistence(ApplicationDbContext applicationDbContext) : IGetUsersPersistence
{
    public async Task<User> ExecuteAsync(string nsa)
    {
        var user = await applicationDbContext
                            .Users
                            .Include(user => user.Role)
                            .Include(user => user.Detail)
                            .FirstOrDefaultAsync(user => user.NSA == nsa);

        if (user is null)
            throw new("Credenciales inválidas");

        return user;
    }
}