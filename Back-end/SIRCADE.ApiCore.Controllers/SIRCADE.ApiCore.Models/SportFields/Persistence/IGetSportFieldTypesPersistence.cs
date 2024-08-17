using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.Persistence;

public interface IGetSportFieldTypesPersistence
{
    Task<IEnumerable<SportFieldType>> ExecuteAsync();
}