namespace SIRCADE.ApiCore.Models.Common.DTOs;

public record ValidateMessageDto<T>(
    bool IsValid, 
    string Message,
    IEnumerable<T>? Entities = null) where T: class;