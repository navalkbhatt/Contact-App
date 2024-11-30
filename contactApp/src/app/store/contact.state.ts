export interface RootState {
  results: Array<Contact>;
}
export interface Contact {
  Id: string;
  FirstName: string;
  LastName: string;
  Email: string;
}
