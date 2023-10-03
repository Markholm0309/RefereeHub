import { Injectable } from '@angular/core';
import { CanActivate } from '@angular/router';
import { Observable, map } from 'rxjs';
import { AccountService } from '../services/account.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {
  
  constructor(private service: AccountService) {}

  canActivate(): Observable<boolean> {
    return this.service.currentUser$.pipe(
      map(user => {
          if (user)
            console.log("true")
            return true;
        })
    )
  }
}
