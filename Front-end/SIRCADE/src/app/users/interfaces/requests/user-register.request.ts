export interface UserRegisterRequest {
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
  associated: boolean;
  situation: number;
  documentNumber: number;
  maritalStatus: number;
  address: string;
  observation?: string;
  roleId: string;
}
