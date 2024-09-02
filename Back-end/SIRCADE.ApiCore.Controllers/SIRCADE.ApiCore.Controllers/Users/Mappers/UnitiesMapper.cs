using SIRCADE.ApiCore.Controllers.Users.Responses;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Controllers.Users.Mappers;

public static class UnitiesMapper
{
    public static UnityResponse ToUnityResponse(this Unity unity)
    {
        return new(unity.Id, unity.Name);
    }
}