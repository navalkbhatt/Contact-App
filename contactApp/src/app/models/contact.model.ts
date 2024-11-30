export interface ContactResponse {
  Id?: string;
  FirstName: string;
  LastName: string;
  Email: string;
}
export interface EditContactDto {
  Id: string;
  FirstName: string;
  LastName: string;
  Email: string;
}
