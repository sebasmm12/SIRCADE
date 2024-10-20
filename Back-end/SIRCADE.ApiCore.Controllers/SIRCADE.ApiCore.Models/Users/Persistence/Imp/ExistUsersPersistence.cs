using Microsoft.EntityFrameworkCore;

namespace SIRCADE.ApiCore.Models.Users.Persistence.Imp;

public class ExistUsersPersistence
    (ApplicationDbContext context): IExistUsersPersistence
{
    public async Task<bool> ExecuteAsync(string nsa, string documentNumber)
    {
        var exists = await context
                                .Users
                                .Include(user => user.Detail)
                                .IgnoreQueryFilters()
                                .AnyAsync(user => user.NSA == nsa ||
                                                  user.Detail.DocumentNumber == documentNumber);

        return exists;
    }
}