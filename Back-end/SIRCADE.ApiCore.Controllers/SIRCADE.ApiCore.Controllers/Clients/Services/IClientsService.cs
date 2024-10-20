using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Controllers.Clients.Services;

public interface IClientsService
{
    Task<IEnumerable<OptionDto<int>>> GetAllAsync();
}