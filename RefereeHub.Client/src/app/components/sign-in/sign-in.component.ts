import { Component, Input } from '@angular/core';
import { Router } from '@angular/router';
import { AuthenticateModel } from 'src/app/models/authenticate';
import { AccountService } from 'src/app/services/account.service';

@Component({
  selector: 'app-sign-in',
  templateUrl: './sign-in.component.html',
  styleUrls: ['./sign-in.component.scss']
})
export class SignInComponent {
  @Input() model: AuthenticateModel;
}
