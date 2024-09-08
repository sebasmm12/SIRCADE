using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Models.Common.Extensions;

public static class YearsExtensions
{
    public static IEnumerable<OptionDto<int>> GetYears(int minimumYears)
    {
        var currentYear = DateTime.Now.Year;

        return Enumerable
                    .Range(currentYear - minimumYears, minimumYears + 1)
                    .Select(year => new OptionDto<int>(year, year.ToString()));
    }
}