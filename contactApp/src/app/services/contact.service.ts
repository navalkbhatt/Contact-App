import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { ServiceRoutes } from './service.routes';
import { ContactResponse } from '../models/contact.model';
import { map, Observable } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ContactService {
  constructor(
    private dataService: DataService,
    private serviceRoutes: ServiceRoutes
  ) {}
  getContacts(): Observable<Array<ContactResponse>> {
    return this.dataService
      .get<Array<ContactResponse>>(this.serviceRoutes.contactsRoute())
      .pipe(map((res) => res.Body));
  }
  addContact(contact: ContactResponse): Observable<boolean> {
    return this.dataService
      .post<boolean>(this.serviceRoutes.contactsRoute(), contact)
      .pipe(map((res) => res.Body));
  }
}
