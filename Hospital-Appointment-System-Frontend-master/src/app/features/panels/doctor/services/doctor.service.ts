import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { jwtDecode } from 'jwt-decode';
import { Doctor } from '../models/doctor';
import { ResponseModel } from '../../../models/responseModel';
import { DoctorForAppointment } from '../models/doctorForAppointment';





@Injectable({
  providedIn: 'root',
})
export class DoctorService {
  apiUrl = 'http://localhost:60805/api/Doctors';
  registerDoctorUrl = 'http://localhost:60805/api/Auth/Register/Doctor';

  constructor(private httpClient: HttpClient) {}

  private decodeToken(token: string): any {
    try {
      return jwtDecode(token);
    } catch (Error) {
      return null;
    }
  }

  getDoctors(
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Doctor>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());

    return this.httpClient.get<ResponseModel<Doctor>>(this.apiUrl, { params });
  }

  getDoctorById(id: string): Observable<Doctor> {
    return this.httpClient.get<Doctor>(`${this.apiUrl}/${id}`);
  }

  getDoctorsByBranch(branchId: string): Observable<Doctor[]> {
    return this.httpClient.get<Doctor[]>(`${this.apiUrl}/branch/${branchId}`);
  }

  addDoctor(doctor: any): Observable<Doctor> {
    return this.httpClient.post<any>(this.registerDoctorUrl, doctor);
  }

  updateDoctor(doctor: Doctor): Observable<ResponseModel<Doctor>> {
    return this.httpClient.put<ResponseModel<Doctor>>(this.apiUrl, doctor);
  }

  deleteDoctor(id: string): Observable<ResponseModel<any>> {
    return this.httpClient.delete<ResponseModel<any>>(`${this.apiUrl}/${id}`);
  }

  getDoctorProfile(): Observable<Doctor> {
    const token = localStorage.getItem('token');
    if (!token) {
      throw new Error('');
    }

    const decodedToken: any = this.decodeToken(token);
    if (!decodedToken || !decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier']) {
      throw new Error('Token decode edilemedi veya ID bulunamadı');
    }

    const doctorId = decodedToken['http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier'];
    return this.httpClient.get<Doctor>(`${this.apiUrl}/${doctorId}`);
  }

//BranchId ye göre doktorları getir
  getListByBranchId(PageIndex:number,PageSize:number,branchId:number):Observable<ResponseModel<DoctorForAppointment>>
  {    let newPath=this.apiUrl+"/getByBranchId"
        let params = new HttpParams()
      .set('PageIndex', PageIndex.toString())
      .set('PageSize', PageSize.toString())
      .set('BranchId',branchId);

      return this.httpClient.get<ResponseModel<DoctorForAppointment>>(newPath, { params });
  }
}
