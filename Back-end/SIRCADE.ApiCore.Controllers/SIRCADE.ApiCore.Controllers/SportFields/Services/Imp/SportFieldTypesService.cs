using SIRCADE.ApiCore.Controllers.SportFields.Mappers;
using SIRCADE.ApiCore.Controllers.SportFields.Responses;
using SIRCADE.ApiCore.Models.SportFields.Persistence;

namespace SIRCADE.ApiCore.Controllers.SportFields.Services.Imp;

public class SportFieldTypesService(
    IGetSportFieldTypesPersistence getSportFieldTypesPersistence) : ISportFieldTypesService
{
    public async Task<IEnumerable<SportFieldTypeResponse>> GetSportFieldTypesAsync()
    {
        var sportFieldTypes = await getSportFieldTypesPersistence.ExecuteAsync();

        var sportFieldTypeResponses = sportFieldTypes.Select(sportFieldType => sportFieldType.ToResponse());

        return sportFieldTypeResponses;
    }
}