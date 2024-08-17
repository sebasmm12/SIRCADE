using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.Persistence;

public interface ICreateSportFieldPersistence
{
    Task<int> ExecuteAsync(SportField sportField);
}