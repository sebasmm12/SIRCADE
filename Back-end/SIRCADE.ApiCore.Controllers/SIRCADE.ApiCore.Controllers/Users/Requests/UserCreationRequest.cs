﻿using SIRCADE.ApiCore.Models.Users.Enums;

namespace SIRCADE.ApiCore.Controllers.Users.Requests;

public record UserCreationRequest(
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
    bool Associated,
    Situation Situation,
    string DocumentNumber,
    MaritalStatus MaritalStatus,
    string Address,
    string? Observation);