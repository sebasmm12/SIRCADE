export interface UserInfoResponse {
  id: number;
  nsa: string;
  grade: string;
  paternalLastName: string;
  maternalLastName: string;
  names: string;
  unityId: string;
  birthDate: Date;
  phone: string;
  email: string;
  cellPhone: string;
  situation: number;
  documentNumber: number;
  maritalStatus: number;
  address: string;
  observation?: string;
  roleId: string;
}
