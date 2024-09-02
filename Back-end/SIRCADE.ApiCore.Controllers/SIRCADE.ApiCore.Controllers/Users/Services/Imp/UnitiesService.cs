using SIRCADE.ApiCore.Controllers.Users.Mappers;
using SIRCADE.ApiCore.Controllers.Users.Responses;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Users.Services.Imp;

public class UnitiesService
    (IGetUnitiesPersistence getUnitiesPersistence): IUnitiesService
{
    public async Task<IEnumerable<UnityResponse>> GetAllAsync()
    {
        var unities = await getUnitiesPersistence.ExecuteAsync();

        var unitiesResponse = unities.Select(unity => unity.ToUnityResponse());

        return unitiesResponse;
    }
}