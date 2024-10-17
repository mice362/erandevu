import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../../models/responseModel';
import { Branch } from '../models/branch';
import { HttpClient,HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BranchService {

  constructor(private httpClient:HttpClient) { }
   apiUrl="http://localhost:60805/api/Branches"

  getBranches(pageIndex:number,pageSize:number):Observable<ResponseModel<Branch>>
  {    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString());


    return this.httpClient.get<ResponseModel<Branch>>(this.apiUrl,{params});
  }

  getAllBranches(): Observable<Branch[]> {
    return this.httpClient.get<Branch[]>(`${this.apiUrl}/getall`);
  }

  addbranch(branch: any): Observable<Branch> {
    return this.httpClient.post<any>(this.apiUrl, branch);
  }

  updateBranch(branch: Branch): Observable<ResponseModel<Branch>> {
    return this.httpClient.put<ResponseModel<Branch>>(this.apiUrl, branch);
  }
  
  deleteBranch(id:number,pageIndex:number,pageSize:number): Observable<ResponseModel<any>> {
    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString());
    return this.httpClient.delete<ResponseModel<any>>(`${this.apiUrl}/${id}`,{params});
  }

  getByBranchId(
    id: string,
    pageIndex: number,
    pageSize: number
  ): Observable<Branch> {
    let params = new HttpParams()
    //.set('PageIndex', pageIndex.toString())
    //.set('PageSize', pageSize.toString())
     .set('id', id);
     

    return this.httpClient.get<Branch>(`${this.apiUrl}/${id}`);
  }

}
