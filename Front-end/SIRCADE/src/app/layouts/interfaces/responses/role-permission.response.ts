import { PermissionType } from '../enums/permission-type.enum';

export interface RolePermissionResponse {
  id: number;
  name: string;
  type: PermissionType;
  url?: string;
  icon?: string;
}
