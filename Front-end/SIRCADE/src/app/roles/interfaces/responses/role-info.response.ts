import { RolePermissionDto } from "../dtos/role-permission.dto";

export interface RoleInfoResponse {
  id: number;
  name: string;
  permissions: RolePermissionDto[];
}
