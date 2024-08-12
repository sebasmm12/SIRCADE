using SIRCADE.ApiCore.Models.Roles.Entities;

namespace SIRCADE.ApiCore.Models.Roles.Persistence;

public interface IDeleteRolePersistence
{
    Task ExecuteAsync(Role role);
}