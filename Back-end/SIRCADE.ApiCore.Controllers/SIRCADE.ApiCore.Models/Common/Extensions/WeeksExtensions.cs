using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Models.Common.Extensions;

public static class WeeksExtensions
{
    private static IEnumerable<OptionDto<int>> shortMonthNames =
    [
        new(1, "Ene"),
        new(2, "Feb"),
        new(3, "Mar"),
        new(4, "Abr"),
        new(5, "May"),
        new(6, "Jun"),
        new(7, "Jul"),
        new(8, "Ago"),
        new(9, "Sep"),
        new(10, "Oct"),
        new(11, "Nov"),
        new(12, "Dic")
    ];

    public static IEnumerable<OptionDto<WeeksDto>> GetWeeks(int totalWeeks = 4)
    {
        var currentDay = DateTime.Now.DayOfYear;

        var decreasedDays = 7 * totalWeeks;

        var lastDay = currentDay - decreasedDays;

        var weeks = Enumerable
                        .Range(lastDay + 1, decreasedDays)
                        .Chunk(7)
                        .Select(x => new OptionDto<WeeksDto>(new(x.First(), x.Last()), GetWeekRange(x.First(), x.Last())));

        return weeks;
    }


    public static string GetWeekRange(int firstDayOfYear, int lastDayOfYear)
    {
        var currentYear = DateTime.Now.Year;

        var firstDay = new DateTime(currentYear, 1, 1).AddDays(firstDayOfYear - 1);

        var firstMonthShortName = shortMonthNames.First(shortMonthName => shortMonthName.Id == firstDay.Month);

        var firstWeek = $"{firstDay.Day} {firstMonthShortName.Label}, {firstDay.Year}";

        var lastDay = new DateTime(currentYear, 1, 1).AddDays(lastDayOfYear - 1);

        var lastMonthShortName = shortMonthNames.First(shortMonthName => shortMonthName.Id == lastDay.Month);

        var lastWeek = $"{lastDay.Day} {lastMonthShortName.Label}, {lastDay.Year}";

        return $"{firstWeek} - {lastWeek}";
    }
}