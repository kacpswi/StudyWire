import { Component, OnInit, inject } from '@angular/core';
import { RegisterComponent } from "../register/register.component";
import { LoginComponent } from "../login/login.component";
import { AccountService } from '../_services/account.service';

@Component({
    selector: 'app-home',
    standalone: true,
    templateUrl: './home.component.html',
    styleUrl: './home.component.css',
    imports: [RegisterComponent, LoginComponent]
})
export class HomeComponent{
  accountService = inject(AccountService);
  registerMode = false;
  loginMode = false;

  registerToggle(){
    this.registerMode = !this.registerMode;
  }

  loginToRegister(event: boolean)
  {
    this.registerMode = event;
    this.loginMode = !event;
  }

  cancelRegisterMode(event: boolean)
  {
    this.registerMode = event;
  }

  loginToggle(){
    this.loginMode = !this.loginMode
  }

  registerToLogin(event: boolean)
  {
    this.registerMode = !event;
    this.loginMode = event;
  }

  cancelLoginMode(event: boolean){
    this.loginMode = event;
  }

}
