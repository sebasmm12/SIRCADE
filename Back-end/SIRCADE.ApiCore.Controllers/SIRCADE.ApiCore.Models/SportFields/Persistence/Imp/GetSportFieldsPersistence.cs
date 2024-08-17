using Microsoft.EntityFrameworkCore;
using SIRCADE.ApiCore.Models.Common.DTOs;
using SIRCADE.ApiCore.Models.Common.Queries;
using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Models.SportFields.Persistence.Imp;

public class GetSportFieldsPersistence(ApplicationDbContext context) : IGetSportFieldsPersistence
{
    public async Task<DataTableDto<SportField>> ExecuteAsync(DataTableQueriesDto dataTableQueries)
    {
        var sportFieldsContext = context
                                    .SportFields
                                    .Include(sportField => sportField.SportFieldType)
                                    .AsQueryable();

        if(!string.IsNullOrEmpty(dataTableQueries.Search))
            sportFieldsContext = sportFieldsContext
                                     .Where(sportField => sportField.Name.Contains(dataTableQueries.Search) ||
                                                          sportField.SportFieldType.Name.Contains(dataTableQueries.Search));


        var sportFields = await sportFieldsContext
                                        .OrderBy(sportField => sportField.Id)
                                        .Skip(dataTableQueries.Page)
                                        .Take(dataTableQueries.PageSize)
                                        .AsNoTracking()
                                        .ToListAsync();

        var totalSportFields = await sportFieldsContext.CountAsync();

        return new(sportFields, totalSportFields);
    }

    public async Task<SportField> ExecuteAsync(int roleId, bool isTracked = false)
    {
        var sportFieldsContext = context.SportFields.AsQueryable();

        if (!isTracked)
            sportFieldsContext = sportFieldsContext.AsNoTracking();

        var sportField = await sportFieldsContext.FirstOrDefaultAsync(sportField => sportField.Id == roleId);

        ArgumentNullException.ThrowIfNull(sportField);

        return sportField;
    }
}