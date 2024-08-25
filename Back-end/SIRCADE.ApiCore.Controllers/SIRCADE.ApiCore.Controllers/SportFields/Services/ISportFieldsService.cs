using SIRCADE.ApiCore.Controllers.SportFields.Requests;
using SIRCADE.ApiCore.Controllers.SportFields.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;

namespace SIRCADE.ApiCore.Controllers.SportFields.Services;

public interface ISportFieldsService
{
    Task<DataTableDto<SportFieldResponse>> GetAsync(DataTableQueriesDto dataTableQueries);
    
    Task<int> CreateAsync(SportFieldCreationRequest sportFieldCreationRequest);

    Task UpdateAsync(SportFieldUpdateRequest sportFieldUpdateRequest);
    
    Task<SportFieldInfoResponse> GetAsync(int sportFieldId);

    Task<IEnumerable<SportFieldInfoResponse>> GetAsync();
}