using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Users.Persistence;

namespace SIRCADE.ApiCore.Controllers.Clients.Services.Imp;

public class ClientsService
    (IGetClientsPersistence getClientsPersistence): IClientsService
{
    public async Task<IEnumerable<OptionDto<int>>> GetAllAsync()
    {
        var clients = await getClientsPersistence.ExecuteAsync();

        var clientsResponse = clients.Select(client => new OptionDto<int>(client.Id, client.GetFullName()));

        return clientsResponse;
    }
}