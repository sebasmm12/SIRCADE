namespace SIRCADE.ApiCore.Models.Roles.Persistence;

public interface IExistRolePersistence
{
    Task<bool> ExecuteAsync(int roleId);
}