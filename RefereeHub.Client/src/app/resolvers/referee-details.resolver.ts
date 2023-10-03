import { Injectable } from '@angular/core';
import {
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable } from 'rxjs';
import { RefereeService } from '../services/referee.service';
import { Referee } from '../models/referee/referee';

@Injectable({
  providedIn: 'root'
})
export class RefereeDetailsResolver {
  constructor(private service: RefereeService) {}

  resolve(route: ActivatedRouteSnapshot): Observable<Referee> {
    return this.service.getRefereeByName(route.paramMap.get('username'));
  }
}
