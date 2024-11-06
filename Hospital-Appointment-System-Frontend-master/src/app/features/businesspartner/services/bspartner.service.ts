import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../../models/responseModel';
import { BusinessPartner } from '../models/businesspartner';
import { HttpClient,HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BusinessPartnerService {

  constructor(private httpClient:HttpClient) { }
   apiUrl="http://localhost:60805/api/BusinessPartners"

  getBusinessPartners(pageIndex:number,pageSize:number):Observable<ResponseModel<BusinessPartner>>
  {    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString());


    return this.httpClient.get<ResponseModel<BusinessPartner>>(this.apiUrl,{params});
  }

  getAllBusinessPartners(): Observable<BusinessPartner[]> {
    return this.httpClient.get<BusinessPartner[]>(`${this.apiUrl}/getall`);
  }

  addBusinessPartner(bspartner: any): Observable<BusinessPartner> {
    return this.httpClient.post<any>(this.apiUrl, bspartner);
  }

  updateBusinessPartner(bspartner: BusinessPartner): Observable<ResponseModel<BusinessPartner>> {
    return this.httpClient.put<ResponseModel<BusinessPartner>>(this.apiUrl, bspartner);
  }
  
  deleteBusinessPartner(id:number,pageIndex:number,pageSize:number): Observable<ResponseModel<any>> {
    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString());
    return this.httpClient.delete<ResponseModel<any>>(`${this.apiUrl}/${id}`,{params});
  }

  getByBusinessPartnerId(
    id: number,
    pageIndex: number,
    pageSize: number
  ): Observable<BusinessPartner> {
    let params = new HttpParams()
    //.set('PageIndex', pageIndex.toString())
    //.set('PageSize', pageSize.toString())
     .set('id', id);
     

    return this.httpClient.get<BusinessPartner>(`${this.apiUrl}/${id}`);
  }

}
