using SIRCADE.ApiCore.Controllers.Users.Requests;
using SIRCADE.ApiCore.Controllers.Users.Responses;
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
                Associated = true,
                Situation = userCreationRequest.Situation,
                DocumentNumber = userCreationRequest.DocumentNumber,
                MaritalStatus = userCreationRequest.MaritalStatus,
                Address = userCreationRequest.Address,
                Observation = userCreationRequest.Observation
            }
        };
    }

    public static UserResponse MapToUserResponse(this User user)
    {
        return new(
            user.Id, 
            user.NSA, 
            user.GetFullName(), 
            user.Detail.Grade, 
            user.Detail.Unity.Name,
            user.Detail.Associated,
            user.Role.Name);
    }

    public static UserInfoResponse MapToUserInfoResponse(this User user)
    {
        return new(
            user.Id,
            user.NSA,
            user.RoleId,
            user.Detail.Grade,
            user.Detail.PaternalLastName,
            user.Detail.MaternalLastName,
            user.Detail.Names,
            user.Detail.UnityId,
            user.Detail.BirthDate,
            user.Detail.Phone,
            user.Detail.Email,
            user.Detail.CellPhone,
            user.Detail.Situation,
            user.Detail.DocumentNumber,
            user.Detail.MaritalStatus,
            user.Detail.Address,
            user.Detail.Observation);
    }

    public static User MapToUser(this UserUpdateRequest userUpdateRequest, User user)
    {
        user.NSA = userUpdateRequest.Nsa;
        user.RoleId = userUpdateRequest.RoleId;
        user.Detail.Grade = userUpdateRequest.Grade;
        user.Detail.PaternalLastName = userUpdateRequest.PaternalLastName;
        user.Detail.MaternalLastName = userUpdateRequest.MaternalLastName;
        user.Detail.Names = userUpdateRequest.Names;
        user.Detail.UnityId = userUpdateRequest.UnityId;
        user.Detail.BirthDate = userUpdateRequest.BirthDate;
        user.Detail.Phone = userUpdateRequest.Phone;
        user.Detail.Email = userUpdateRequest.Email;
        user.Detail.CellPhone = userUpdateRequest.CellPhone;
        user.Detail.Situation = userUpdateRequest.Situation;
        user.Detail.DocumentNumber = userUpdateRequest.DocumentNumber;
        user.Detail.MaritalStatus = userUpdateRequest.MaritalStatus;
        user.Detail.Address = userUpdateRequest.Address;
        user.Detail.Observation = userUpdateRequest.Observation;

        return user;
    }
}