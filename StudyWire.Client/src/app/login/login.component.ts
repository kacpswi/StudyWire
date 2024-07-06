import { Component, inject, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  private router = inject(Router);
  accountService = inject(AccountService)
  cancelLogin = output<boolean>();
  startRegister = output<boolean>();
  model: any = {};

  login()
  {
    this.accountService.login(this.model).subscribe({
      next: _ => {
        this.cancel()
        this.router.navigateByUrl("/newses")
      },
      error: error => console.log(error)
    })
  }

  toggleToRegister(){
    this.startRegister.emit(true);
  }

  cancel(){
    this.cancelLogin.emit(false);
  }
}
