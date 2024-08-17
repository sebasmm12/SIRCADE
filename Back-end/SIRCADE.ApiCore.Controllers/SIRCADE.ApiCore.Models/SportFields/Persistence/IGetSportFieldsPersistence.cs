using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;
using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.Persistence;

public interface IGetSportFieldsPersistence
{
    Task<DataTableDto<SportField>> ExecuteAsync(DataTableQueriesDto dataTableQueries);

    Task<SportField> ExecuteAsync(int roleId, bool isTracked = false);
}