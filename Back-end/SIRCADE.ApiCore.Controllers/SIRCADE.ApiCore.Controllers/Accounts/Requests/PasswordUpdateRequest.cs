namespace SIRCADE.ApiCore.Controllers.Accounts.Requests;

public record PasswordUpdateRequest(string CurrentPassword, string NewPassword, string NewPasswordConfirmation);