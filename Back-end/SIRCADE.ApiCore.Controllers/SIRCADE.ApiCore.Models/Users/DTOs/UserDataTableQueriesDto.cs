using SIRCADE.ApiCore.Models.Common.Queries;

namespace SIRCADE.ApiCore.Models.Users.DTOs;

public record UserDataTableQueriesDto : DataTableQueriesDto
{
    public IEnumerable<string> Roles { get; set; } = default!;
}