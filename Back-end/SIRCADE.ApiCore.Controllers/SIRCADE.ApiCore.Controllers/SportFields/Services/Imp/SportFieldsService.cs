using SIRCADE.ApiCore.Controllers.SportFields.Mappers;
using SIRCADE.ApiCore.Controllers.SportFields.Requests;
using SIRCADE.ApiCore.Controllers.SportFields.Responses;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;
using SIRCADE.ApiCore.Models.SportFields.Persistence;

namespace SIRCADE.ApiCore.Controllers.SportFields.Services.Imp;

public class SportFieldsService(
    IGetSportFieldsPersistence getSportFieldsPersistence,
    ICreateSportFieldPersistence createSportFieldPersistence,
    IUpdateSportFieldPersistence updateSportFieldPersistence) : ISportFieldsService
{
    public async Task<DataTableDto<SportFieldResponse>> GetAsync(DataTableQueriesDto dataTableQueries)
    {
        var sportFields = await getSportFieldsPersistence.ExecuteAsync(dataTableQueries);

        var sportFieldResponses = sportFields.Data.Select(sportField => sportField.ToSportFieldResponse());

        return new(sportFieldResponses, sportFields.TotalElements);
    }

    public async Task<SportFieldInfoResponse> GetAsync(int sportFieldId)
    {
        var sportField = await getSportFieldsPersistence.ExecuteAsync(sportFieldId);

        var sportFieldResponse = sportField.ToSportFieldInfoResponse();

        return sportFieldResponse;
    }

    public async Task<int> CreateAsync(SportFieldCreationRequest sportFieldCreationRequest)
    {
        var sportField = sportFieldCreationRequest.ToSportField();

        await createSportFieldPersistence.ExecuteAsync(sportField);

        return sportField.Id;
    }

    public async Task UpdateAsync(SportFieldUpdateRequest sportFieldUpdateRequest)
    {
        var sportField = await getSportFieldsPersistence.ExecuteAsync(sportFieldUpdateRequest.Id, isTracked: true);

        sportFieldUpdateRequest.ToSportField(sportField);

        await updateSportFieldPersistence.ExecuteAsync();
    }
}