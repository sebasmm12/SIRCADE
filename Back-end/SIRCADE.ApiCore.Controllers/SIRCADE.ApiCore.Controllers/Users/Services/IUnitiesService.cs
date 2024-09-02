using SIRCADE.ApiCore.Controllers.Users.Responses;

namespace SIRCADE.ApiCore.Controllers.Users.Services;

public interface IUnitiesService
{
    Task<IEnumerable<UnityResponse>> GetAllAsync();
}