using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Models.Users.Entities;

namespace SIRCADE.ApiCore.Controllers.Users.Mappers;

public static class UsersMapper
{
    public static User MapToUser(this UserCreationRequest userCreationRequest)
    {
        return new()
        {
            NSA = userCreationRequest.Nsa,
            RoleId = userCreationRequest.RoleId,
            Detail = new()
            {
                Grade = userCreationRequest.Grade,
                PaternalLastName = userCreationRequest.PaternalLastName,
                MaternalLastName = userCreationRequest.MaternalLastName,
                Names = userCreationRequest.Names,
                UnityId = userCreationRequest.UnityId,
                BirthDate = userCreationRequest.BirthDate,
                Phone = userCreationRequest.Phone,
                Email = userCreationRequest.Email,
                CellPhone = userCreationRequest.CellPhone,
                Associated = userCreationRequest.Associated,
                Situation = userCreationRequest.Situation,
                DocumentNumber = userCreationRequest.DocumentNumber,
                MaritalStatus = userCreationRequest.MaritalStatus,
                Address = userCreationRequest.Address,
                Observation = userCreationRequest.Observation
            }
        };
    }
}