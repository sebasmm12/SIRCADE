using SIRCADE.ApiCore.Controllers.SportFields.Requests;
using SIRCADE.ApiCore.Controllers.SportFields.Responses;
using SIRCADE.ApiCore.Models.SportFields.Entities;

namespace SIRCADE.ApiCore.Controllers.SportFields.Mappers;

public static class SportFieldsMapper
{
    public static SportFieldResponse ToSportFieldResponse(this SportField sportField)
    {
        return new(sportField.Id, sportField.Name, sportField.SportFieldType.Name);
    }

    public static SportFieldInfoResponse ToSportFieldInfoResponse(this SportField sportField)
    {
        return new(sportField.Id, sportField.Name, sportField.Type);
    }

    public static SportField ToSportField(this SportFieldCreationRequest sportFieldCreationRequest)
    {
        return new(sportFieldCreationRequest.Name, sportFieldCreationRequest.Type);
    }

    public static SportField ToSportField(this SportFieldUpdateRequest sportFieldUpdateRequest, SportField sportField)
    {
        sportField.Name = sportFieldUpdateRequest.Name;
        sportField.Type = sportFieldUpdateRequest.Type;

        return sportField;
    }
}