using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface IGetUsersPersistence
{
    Task<User> ExecuteAsync(string nsa);
}