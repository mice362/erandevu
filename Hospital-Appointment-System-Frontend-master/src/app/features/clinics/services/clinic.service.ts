import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../../models/responseModel';
import { Clinic } from '../models/clinic';
import { HttpClient,HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class ClinicService {

  constructor(private httpClient:HttpClient) { }
   apiUrl="http://localhost:60805/api/Clinics"

  getClinics(pageIndex:number,pageSize:number):Observable<ResponseModel<Clinic>>
  {    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString());


    return this.httpClient.get<ResponseModel<Clinic>>(this.apiUrl,{params});
  }

  getAllClinics(): Observable<Clinic[]> {
    return this.httpClient.get<Clinic[]>(`${this.apiUrl}/getall`);
  }

  addClinic(clinic: any): Observable<Clinic> {
    return this.httpClient.post<any>(this.apiUrl, clinic);
  }

  updateClinic(clinic: Clinic): Observable<ResponseModel<Clinic>> {
    return this.httpClient.put<ResponseModel<Clinic>>(this.apiUrl, clinic);
  }
  
  deleteClinic(id:number,pageIndex:number,pageSize:number): Observable<ResponseModel<any>> {
    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString());
    return this.httpClient.delete<ResponseModel<any>>(`${this.apiUrl}/${id}`,{params});
  }

  getByClinicId(
    id: number,
    pageIndex: number,
    pageSize: number
  ): Observable<Clinic> {
    let params = new HttpParams()
    //.set('PageIndex', pageIndex.toString())
    //.set('PageSize', pageSize.toString())
     .set('id', id);
     

    return this.httpClient.get<Clinic>(`${this.apiUrl}/${id}`);
  }

}
