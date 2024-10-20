using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface IGetClientsPersistence
{
    Task<IEnumerable<User>> ExecuteAsync();
}