using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Models.Common.Extensions;

public static class DaysExtensions
{
    public static IEnumerable<OptionDto<DayOfWeek>> GetDays()
    {
        return
        [
            new(DayOfWeek.Monday, "Lunes"),
            new(DayOfWeek.Tuesday, "Martes"),
            new(DayOfWeek.Wednesday, "Miércoles"),
            new(DayOfWeek.Thursday, "Jueves"),
            new(DayOfWeek.Friday, "Viernes"),
            new(DayOfWeek.Saturday, "Sábado"),
            new(DayOfWeek.Sunday, "Domingo")
        ];
    }
}