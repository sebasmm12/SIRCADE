using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Models.Users.Persistence;

public interface IGetUnitiesPersistence
{
    Task<IEnumerable<Unity>> ExecuteAsync();
}