import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ResponseModel } from '../../models/responseModel';
import { Feedback } from '../models/feedback';

@Injectable({
  providedIn: 'root',
})
export class FeedbackService {
  apiUrl = 'http://localhost:60805/api/Feedbacks';
  constructor(private httpClient: HttpClient) {}

  getFeedbacks(
    pageIndex: number,
    pageSize: number
  ): Observable<ResponseModel<Feedback>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString());

    return this.httpClient.get<ResponseModel<Feedback>>(this.apiUrl, {
      params,
    });
  }

  getFeedbackById(id: number): Observable<Feedback> {
    return this.httpClient.get<Feedback>(`${this.apiUrl}/${id}`);
  }
  getFeedbackByUserId(
    pageIndex: number,
    pageSize: number,
    userID: string
  ): Observable<ResponseModel<Feedback>> {
    let params = new HttpParams()
      .set('PageIndex', pageIndex.toString())
      .set('PageSize', pageSize.toString())
      .set('userId', userID); // Parametre olarak userID ekleniyor

    return this.httpClient.get<ResponseModel<Feedback>>(`${this.apiUrl}/getByUserId`, {
      params,
    });
  }
  addFeedback(feedback: any): Observable<Feedback> {
    return this.httpClient.post<any>(this.apiUrl, feedback);
  }

  updateFeedback(feedback: Feedback): Observable<ResponseModel<Feedback>> {
    return this.httpClient.put<ResponseModel<Feedback>>(this.apiUrl, feedback);
  }

  deleteFeedback(id: number): Observable<ResponseModel<any>> {
    return this.httpClient.delete<ResponseModel<any>>(`${this.apiUrl}/${id}`);
  }
}
