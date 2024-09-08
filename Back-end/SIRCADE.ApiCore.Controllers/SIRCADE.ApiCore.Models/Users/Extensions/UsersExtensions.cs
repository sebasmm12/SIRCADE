using SIRCADE.ApiCore.Models.Common.DTOs;

namespace SIRCADE.ApiCore.Models.Users.Extensions;

public static class UsersExtensions
{
    public static IEnumerable<OptionDto<string>> GetClientGrades()
    {
        return
        [
            new("Teniente General", "TTG"),
            new("Mayor General", "MAG"),
            new("Coronel", "COR"),
            new("Comandante", "COM"),
            new("Mayor", "MAY"),
            new("Capitán", "CAP"),
            new("Teniente", "TEN"),
            new("Alferez", "ARF")
        ];
    }
}