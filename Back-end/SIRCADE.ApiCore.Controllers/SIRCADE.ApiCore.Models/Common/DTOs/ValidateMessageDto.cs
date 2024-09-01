using SIRCADE.ApiCore.Models.SchedulesProgramming.Entities;

namespace SIRCADE.ApiCore.Models.Common.DTOs;

public record ValidateMessageDto(
    bool IsValid, 
    string Message, 
    ScheduleProgramming? ScheduleProgramming = null);