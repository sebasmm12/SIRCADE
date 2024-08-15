namespace SIRCADE.ApiCore.Models.Common.Queries;

public record DataTableQueriesDto
{
    public int Page { get; set; }

    public int PageSize { get; set; }

    public string? Search { get; set; } = string.Empty;
}