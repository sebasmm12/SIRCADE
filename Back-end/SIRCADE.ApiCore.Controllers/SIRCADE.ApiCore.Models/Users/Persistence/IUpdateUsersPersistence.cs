using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface IUpdateUsersPersistence
{
    Task ExecuteAsync(User user);
}