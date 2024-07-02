import { Component, inject, input, output } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AccountService } from '../_services/account.service';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  private accountService = inject(AccountService);
  cancelRegister = output<boolean>();
  startLogin = output<boolean>();
  model: any = {}

  register(){
    this.accountService.register(this.model).subscribe({
      next: _ =>{
        this.cancel();
      },
      error: error => {
        console.log(error);
      }
    })
  }

  toggleToLogin(){
    this.startLogin.emit(true);
  }

  cancel(){
    this.cancelRegister.emit(false);
  }
}
