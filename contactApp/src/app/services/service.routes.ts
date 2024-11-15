import { Inject, Injectable } from '@angular/core';
import { DOCUMENT } from '@angular/common';
import { environment } from 'src/enviornments/environment';
@Injectable({
  providedIn: 'root',
})
export class ServiceRoutes {
  static readonly pageSize = 100;
  private API_URL = environment.API_URL;

  constructor() {}
  contactsRoute(): string {
    return `${this.API_URL}/api/contract`;
  }
}
