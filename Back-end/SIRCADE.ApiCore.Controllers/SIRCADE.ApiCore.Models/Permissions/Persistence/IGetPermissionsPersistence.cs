using SIRCADE.ApiCore.Models.Permissions.Entities;

namespace SIRCADE.ApiCore.Models.Permissions.Persistence;

public interface IGetPermissionsPersistence
{
    Task<IEnumerable<Permission>> ExecuteAsync();

    Task<IEnumerable<Permission>> ExecuteAsync(int roleId);
}