import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { CreateReportEvent, UpdateEvent } from '../models/report/report';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private baseUrl = `${environment.url}/event`;

  constructor(private http: HttpClient) { }

  async getReportId(id: number): Promise<Observable<number>> {
    return this.http.get<number>(this.baseUrl + `/GetReportId/${id}`).pipe(map(res => {
      return res;
    }));
  }

  async updateEvent(event: UpdateEvent): Promise<Observable<UpdateEvent>> {
    return this.http.patch<UpdateEvent>(this.baseUrl, event);
  }

  async createEvent(event: CreateReportEvent): Promise<Observable<CreateReportEvent>> {
    return this.http.post<CreateReportEvent>(this.baseUrl, event);
  }


  async deleteEvent(id: number): Promise<Observable<number>> {
    return this.http.delete<number>(`${this.baseUrl}/${id}`);
  }}
