namespace SIRCADE.ApiCore.Models.Common.DTOs;

public record DataTableDto<T>(IEnumerable<T> Data, int TotalElements) where T : class
{
}