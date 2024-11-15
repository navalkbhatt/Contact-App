import { Injectable } from '@angular/core';
import { DataService } from './data.service';
import { ServiceRoutes } from './service.routes';
import { ContactResponse } from '../models/contact.model';
import { catchError, map, Observable, of } from 'rxjs';

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
    let cont = convertToPascalCase(contact);
    return this.dataService
      .post<boolean>(this.serviceRoutes.contactsRoute(), cont)
      .pipe(
        map((res) => res.Body),
        catchError((error) => {
          // Handle the error here
          console.error('Error adding contact:', error);

          // Return a fallback value or an Observable with a default value
          return of(false);
        })
      );
  }
}
function convertToPascalCase(obj: any): any {
  if (Array.isArray(obj)) {
    return obj.map(convertToPascalCase);
  } else if (obj !== null && typeof obj === 'object') {
    return Object.keys(obj).reduce((acc, key) => {
      const pascalKey = key.charAt(0).toUpperCase() + key.slice(1);
      acc[pascalKey] = convertToPascalCase(obj[key]);
      return acc;
    }, {} as { [key: string]: any });
  }
  return obj; // Return the value if it's not an object or array
}
