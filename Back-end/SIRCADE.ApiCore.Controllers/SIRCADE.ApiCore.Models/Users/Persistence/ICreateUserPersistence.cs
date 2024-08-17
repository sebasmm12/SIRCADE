using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface ICreateUserPersistence
{
    Task<int> ExecuteAsync(User user);
}