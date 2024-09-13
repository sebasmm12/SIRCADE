using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.DTOs;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface IGetUsersPersistence
{
    Task<User> ExecuteAsync(string nsa);

    Task<DataTableDto<User>> ExecuteAsync(UserDataTableQueriesDto userDataTableQueries);

    Task<User> ExecuteAsync(int userId);

    Task<DataTableDto<User>> ExecuteForReportsAsync(FrequentlyUserDataTableQueriesDto userDataTableQueriesDto, bool isPaginated = true);
}