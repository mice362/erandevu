import { Injectable } from '@angular/core';
import { Appointment } from '../models/appointmentModel';
import { HttpClient, HttpParams } from '@angular/common/http';
import { ResponseModel } from '../../models/responseModel';
import { Observable } from 'rxjs';
import { Doctor } from '../../panels/doctor/models/doctor';
import { AppointmentForPatientPanel } from '../models/appointmentforpatientpanel';
import { CreateAppointment } from '../models/createAppointment';
import { CreateAppointmentResponse } from '../models/createAppointmentResponse';

@Injectable({
  providedIn: 'root',
})
export class AppointmentService {

  apiUrl = 'http://localhost:60805/api/Appointments';

  constructor(private httpClient: HttpClient) { }


  getAllAppointments(
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Appointment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());

    return this.httpClient.get<ResponseModel<Appointment>>(
      this.apiUrl,
      { params }
    );
  }

  getDoctorAppointments(
    doctorId: string,
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Appointment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString())
      .set('doctorId', doctorId); // doctorId parametresini doğrudan params'a ekliyoruz

    return this.httpClient.get<ResponseModel<Appointment>>(
      `${this.apiUrl}/getByDoctorId`,
      { params }
    );
  }

  getPatientAppointments(
    patientId: string,
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Appointment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString())
      .set('patientId', patientId);

    return this.httpClient.get<ResponseModel<Appointment>>(
      `${this.apiUrl}/getByPatientId`,
      { params }
    );
  }

  getAppointmentById(appointmentId: number): Observable<Appointment> {
    return this.httpClient.get<Appointment>(`${this.apiUrl}/${appointmentId}`);
  }


  getAppointmentId(
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Appointment>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());

    return this.httpClient.get<ResponseModel<Appointment>>(this.apiUrl, { params });
  }

  deleteAppointment(appointmentId: number): Observable<void> {
    return this.httpClient.delete<void>(`${this.apiUrl}/${appointmentId}`);
  }





     //Hasta penli için doktorun appointment tablosunda o tarihteki randevularını bul
     getByDoctorDate(pageIndex:number,pageSize:number,doctorId:string,date:string):Observable<ResponseModel<AppointmentForPatientPanel>>{
      let newPath=this.apiUrl+'/getByDoctorDate';
    let params = new HttpParams()
    .set('PageIndex', pageIndex.toString())
    .set('PageSize', pageSize.toString())
    .set('doctorId',doctorId)
    .set('date',date);



    return this.httpClient.get<ResponseModel<AppointmentForPatientPanel>>(newPath,{params});
    }


    createAppointment(appointment:CreateAppointment):Observable<CreateAppointmentResponse>
    {
       return this.httpClient.post<CreateAppointmentResponse>(this.apiUrl,appointment);
    }



  /* addAppointment(appointment: any): Observable<Appointment> {
    return this.httpClient.post<any>(this.apiUrl, appointment);
  }

  updateAppointment(appointment: Appointment): Observable<ResponseModel<Appointment>> {
    return this.httpClient.put<ResponseModel<Appointment>>(this.apiUrl, appointment);
  } */


  addAppointment(appointment: Appointment): Observable<Appointment> {
    return this.httpClient.post<Appointment>(this.apiUrl, appointment);
  }

/*   updateAppointment(appointmentId: number, appointment: Appointment): Observable<Appointment> {
    return this.httpClient.put<Appointment>(`${this.apiUrl}/${appointmentId}`, appointment);
  } */

  updateAppointment(appointmentId: number, appointment: Appointment): Observable<ResponseModel<Appointment>> {
    return this.httpClient.put<ResponseModel<Appointment>>(`${this.apiUrl}/${appointmentId}`, appointment);
  }


}
