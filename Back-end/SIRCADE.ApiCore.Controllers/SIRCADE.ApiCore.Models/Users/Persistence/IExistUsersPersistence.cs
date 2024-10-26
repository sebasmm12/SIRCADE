namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface IExistUsersPersistence
{
    Task<bool> ExecuteAsync(string nsa, string documentNumber);

    Task<bool> ExecuteAsync(string nsa);
}