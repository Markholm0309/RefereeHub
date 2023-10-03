import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { Referee } from '../models/referee/referee';
import { HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment.development';
import { CreateReferee } from '../models/referee/createReferee';

@Injectable({
  providedIn: 'root'
})
export class RefereeService {
  private baseUrl = `${environment.url}/referee`;

  constructor(private http: HttpClient) { } 

  async getReferees(): Promise<Observable<Referee[]>> {
    return this.http.get<Referee[]>(this.baseUrl).pipe(map(res => {
      return Object.values(res);
    }));
  }

  getRefereeByName(name: string): Observable<Referee> {
    return this.http.get<Referee>(`${this.baseUrl}/${name}`).pipe(map(res => {
      return res;
    }));
  }

  async getRefereeId(name: string): Promise<Observable<number>> {
    return this.http.get<number>(`${this.baseUrl}/GetIdByName/${name}`).pipe(map(res => {
      return res;
    }));
  }

  async createReferee(referee: CreateReferee): Promise<Observable<CreateReferee>> {
    return this.http.post<CreateReferee>(this.baseUrl, referee);
  }

  async updateReferee(referee: CreateReferee): Promise<Observable<CreateReferee>> {
    return this.http.patch<CreateReferee>(this.baseUrl, referee);
  }

  async deleteReferee(name: string): Promise<Observable<string>> {
    return this.http.delete<string>(`${this.baseUrl}/${name}`);
  }
}
