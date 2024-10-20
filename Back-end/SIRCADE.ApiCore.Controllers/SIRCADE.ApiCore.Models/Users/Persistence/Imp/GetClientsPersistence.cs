using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence.Imp;

public class GetClientsPersistence
    (ApplicationDbContext applicationDbContext): IGetClientsPersistence
{
    public async Task<IEnumerable<User>> ExecuteAsync()
    {
        var clients = await applicationDbContext
                                .Users
                                .Include(user => user.Role)
                                .Include(user => user.Detail)
                                .Where(user => user.Role.Name == "Socio" &&
                                               user.Detail.Associated == true)
                                .AsNoTracking()
                                .ToListAsync();

        return clients;
    }
}