using SIRCADE.ApiCore.Controllers.SportFields.Responses;

namespace SIRCADE.ApiCore.Controllers.SportFields.Services;

public interface ISportFieldTypesService
{
    Task<IEnumerable<SportFieldTypeResponse>> GetSportFieldTypesAsync();
}