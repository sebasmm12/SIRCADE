export interface PasswordUpdateRequest {
  currentPassword: string;
  newPassword: string;
  newPasswordConfirmation: string;
}
