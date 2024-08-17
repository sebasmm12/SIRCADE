using SIRCADE.ApiCore.Controllers.SportFields.Responses;
using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Controllers.SportFields.Mappers;

public static class SportFieldTypesMapper
{
    public static SportFieldTypeResponse ToResponse(this SportFieldType sportFieldType)
    {
        return new(sportFieldType.Id, sportFieldType.Name);
    }
}