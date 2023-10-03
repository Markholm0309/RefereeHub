import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { environment } from 'src/environments/environment.development';
import { CreateReport, CreateReportRequest, Report, UpdateReport } from '../models/report/report';

@Injectable({
  providedIn: 'root'
})
export class ReportService {
  baseUrl = `${environment.url}/report`;

  constructor(private http: HttpClient) { }

  async getReports(): Promise<Observable<Report[]>> {
    return this.http.get<Report[]>(this.baseUrl).pipe(map(res => {
      return Object.values(res);
    }));
  }

  async getReportsForRefereeById(id: number): Promise<Observable<Report[]>> {
    return this.http.get<Report[]>(this.baseUrl + `/${id}`).pipe(map(res => {
      return Object.values(res);
    }));
  }

  async getReportsForRefereeByName(name: string): Promise<Observable<Report[]>> {
    return this.http.get<Report[]>(this.baseUrl + `/${name}`).pipe(map(res => {
      return Object.values(res);
    }));
  }

  async getReportId(name: string): Promise<Observable<number>> {
    return this.http.get<number>(this.baseUrl + `/GetIdByName/${name}`).pipe(map(res => {
      return res;
    }));
  }

  async updateReport(report: UpdateReport): Promise<Observable<UpdateReport>> {
    return this.http.patch<UpdateReport>(this.baseUrl, report);
  }

  async createReport(report: CreateReportRequest): Promise<Observable<CreateReportRequest>> {
    return this.http.post<CreateReportRequest>(this.baseUrl, report);
  }

  async deleteReport(id: number): Promise<Observable<number>> {
    return this.http.delete<number>(`${this.baseUrl}/${id}`);
  }
}
