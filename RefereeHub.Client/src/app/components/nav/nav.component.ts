import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent {
  display: boolean;
  model: any = {}
  
  constructor(public accountService: AccountService, private router: Router) { }

  login() {
    this.accountService.login(this.model).subscribe({
      complete: () => this.router.navigateByUrl('/landingpage'),
      error: (err) => console.log(err)
    })
  }

  logout() {
    this.accountService.logout();
    this.router.navigateByUrl('/');
  }
}
