using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Persistence;

public interface ICreateRolePersistence
{
    Task<int> ExecuteAsync(Role role);
}