using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Models.Common.Extensions;

public static class MonthsExtensions
{
    public static IEnumerable<OptionDto<int>> GetMonths()
    {
        return
        [
            new(1, "Enero"),
            new(2, "Febrero"),
            new(3, "Marzo"),
            new(4, "Abril"),
            new(5, "Mayo"),
            new(6, "Junio"),
            new(7, "Julio"),
            new(8, "Agosto"),
            new(9, "Septiembre"),
            new(10, "Octubre"),
            new(11, "Noviembre"),
            new(12, "Diciembre")
        ];
    }
}