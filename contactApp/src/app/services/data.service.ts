import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';

import { Observable } from 'rxjs';
import { WebServiceResponse } from './service.model';

export type QueryStringParams =
  | HttpParams
  | {
      [_: string]:
        | string
        | number
        | boolean
        | undefined
        | Array<string | number | boolean | undefined>;
    };

@Injectable({ providedIn: 'root' })
export class DataService {
  constructor(public http: HttpClient) {}

  get<T>(
    url: string,
    params?: QueryStringParams,
    headers?: HttpHeaders
  ): Observable<WebServiceResponse<T>> {
    return this.http.get<WebServiceResponse<T>>(url, {
      params: this.buildQueryStringParams(params),
      headers,
    });
  }

  getBlob(
    url: string,
    params?: QueryStringParams,
    headers?: HttpHeaders
  ): Observable<Blob> {
    return this.http.get<Blob>(url, {
      params: this.buildQueryStringParams(params),
      responseType: 'blob' as 'json',
      headers,
    });
  }

  post<T>(
    url: string,
    data?: any,
    params?: QueryStringParams,
    headers?: HttpHeaders
  ): Observable<WebServiceResponse<T>> {
    return this.http.post<WebServiceResponse<T>>(url, data, {
      params: this.buildQueryStringParams(params),
      headers,
    });
  }

  put<T>(
    url: string,
    data?: any,
    params?: QueryStringParams,
    headers?: HttpHeaders
  ): Observable<WebServiceResponse<T>> {
    return this.http.put<WebServiceResponse<T>>(url, data, {
      params: this.buildQueryStringParams(params),
      headers,
    });
  }

  delete<T>(
    url: string,
    headers?: HttpHeaders
  ): Observable<WebServiceResponse<T>> {
    return this.http.delete<WebServiceResponse<T>>(url, { headers });
  }

  buildQueryStringParams(
    params?: QueryStringParams
  ): HttpParams | { [key: string]: string } {
    if (!params) {
      return {};
    }

    if (params instanceof HttpParams) {
      return params;
    }

    const csvParams: { [key: string]: string } = {};

    for (const [key, values] of Object.entries(params)) {
      if (Array.isArray(values)) {
        const csvValue = values.filter((value) => value != null).join(',');
        if (csvValue) {
          csvParams[key] = csvValue;
        }
      } else if (values != null) {
        csvParams[key] = String(values);
      }
    }

    return csvParams;
  }
}
