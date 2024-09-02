using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;
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
                            .FirstOrDefaultAsync(user => user.NSA == nsa
                                                 && user.Detail.Associated == true);

        if (user is null)
            throw new("Credenciales inválidas");

        return user;
    }

    public async Task<User> ExecuteAsync(int userId)
    {
        var user = await applicationDbContext
                            .Users
                            .Include(user => user.Detail)
                            .FirstOrDefaultAsync(user => user.Id == userId);

        if (user is null)
            throw new("Usuario no encontrado");

        return user;
    }

    public async Task<DataTableDto<User>> ExecuteAsync(UserDataTableQueriesDto userDataTableQueries)
    {
        var usersContext = applicationDbContext
                            .Users
                            .Include(user => user.Detail)
                            .ThenInclude(userDetail => userDetail.Unity)
                            .Include(user => user.Role)
                            .Where(user => userDataTableQueries.Roles.Contains(user.Role.Name))
                            .AsQueryable();

        if (!string.IsNullOrEmpty(userDataTableQueries.Search))
            usersContext = usersContext
                            .Where(user => user.NSA.Contains(userDataTableQueries.Search) ||
                                           user.Detail.Names.Contains(userDataTableQueries.Search) ||
                                           user.Detail.PaternalLastName.Contains(userDataTableQueries.Search) ||
                                           user.Detail.MaternalLastName.Contains(userDataTableQueries.Search) ||
                                           user.Detail.Unity.Name.Contains(userDataTableQueries.Search) ||
                                           user.Detail.Grade.Contains(userDataTableQueries.Search));


        var users = await usersContext
                            .OrderBy(user => user.Detail.Names)
                            .Skip(userDataTableQueries.Page)
                            .Take(userDataTableQueries.PageSize)
                            .AsNoTracking()
                            .ToListAsync();

        var totalUsers = await usersContext.CountAsync();

        return new(users, totalUsers);
    }
}