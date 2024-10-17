// src/app/services/drschedule.service.ts
import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { CreateDrScheduleRequest } from '../models/create-request-drschedule';

import { jwtDecode } from 'jwt-decode';
import { DoctorSchedule } from '../models/doctorschedule';
import { ResponseModel } from '../../models/responseModel';
import { UpdateDoctorSchedule } from '../models/update-doctor-schedule';

@Injectable({
  providedIn: 'root'
})
export class DrscheduleService {
  apiUrl = 'http://localhost:60805/api/DoctorSchedules';

  constructor(private httpClient: HttpClient) {}

  add(CreateDrScheduleRequest:CreateDrScheduleRequest,):Observable<DoctorSchedule>
  {
     return this.httpClient.post<DoctorSchedule>(this.apiUrl,CreateDrScheduleRequest);
  }


  //o doktora ait takvimi getir

  getDoctorSchedule(pageIndex:number,pageSize:number,doctorId:string):Observable<ResponseModel<DoctorSchedule>>
  {
    let newPath=this.apiUrl+'/getByDoctorId'
    let params = new HttpParams()
   .set('PageIndex', pageIndex.toString())
   .set('PageSize', pageSize.toString())
   .set('doctorId',doctorId);

   return this.httpClient.get<ResponseModel<DoctorSchedule>>(newPath,{params});
  }



  //scheduleid ye göre sil
  deleteDoctorSchedule(id:number)
  {
    let newPath=this.apiUrl+'/'+id;
    return this.httpClient.delete(newPath);
  }

  updateDoctorSchedule(schedule:UpdateDoctorSchedule):Observable<UpdateDoctorSchedule>
  {
    return this.httpClient.put<UpdateDoctorSchedule>(this.apiUrl,schedule);
  }

    //bu takvimin kendi id sine göre getir
  getById(id:number):Observable<UpdateDoctorSchedule>
  {
      let newPath=this.apiUrl+'/'+id;
      return this.httpClient.get<UpdateDoctorSchedule>(newPath);
  }







  }

