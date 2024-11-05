import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { Clinic } from '../models/clinic';
import { ResponseModel } from '../../../models/responseModel';


@Injectable({
  providedIn: 'root',
})
export class ClinicService {
  apiUrl = 'http://localhost:60805/api/Clinics';
  registerClinicUrl = 'http://localhost:60805/api/Auth/Register/Clinic';

  constructor(private httpClient: HttpClient) {}

  private decodeToken(token: string): any {
    try {
      return jwtDecode(token);
    } catch (Error) {
      return null;
    }
  }

  getClinics(
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Clinic>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());

    return this.httpClient.get<ResponseModel<Clinic>>(this.apiUrl, { params });
  }

  getClinicById(id: string): Observable<Clinic> {
    return this.httpClient.get<Clinic>(`${this.apiUrl}/${id}`);
  }

  addClinic(clinic: any): Observable<Clinic> {
    return this.httpClient.post<any>(this.registerClinicUrl, clinic);
  }

  updateClinic(clinic: Clinic): Observable<ResponseModel<Clinic>> {
    return this.httpClient.put<ResponseModel<Clinic>>(this.apiUrl, clinic);
  }

  deleteClinic(id: string): Observable<ResponseModel<any>> {
    return this.httpClient.delete<ResponseModel<any>>(`${this.apiUrl}/${id}`);
  }

  getClinicProfile(): Observable<Clinic> {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('');
    }

    const decodedToken: any = this.decodeToken(token);
    if (!decodedToken || !decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']) {
      throw new Error('Token decode edilemedi veya ID bulunamadı');
    }

    const clinicId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    return this.httpClient.get<Clinic>(`${this.apiUrl}/${clinicId}`);
  }
}
