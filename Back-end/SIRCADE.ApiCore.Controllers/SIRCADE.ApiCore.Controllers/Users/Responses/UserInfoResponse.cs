﻿using SIRCADE.ApiCore.Models.Users.Enums;

namespace SIRCADE.ApiCore.Controllers.Users.Responses;

public record UserInfoResponse(
    int Id,
    string Nsa,
    int RoleId,
    string Grade,
    string PaternalLastName,
    string MaternalLastName,
    string Names,
    int UnityId,
    DateTime BirthDate,
    string Phone,
    string Email,
    string CellPhone,
    Situation Situation,
    string DocumentNumber,
    MaritalStatus MaritalStatus,
    string? Address,
    string? Observation);