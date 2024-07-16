import { Component, inject, OnInit, output } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../_services/account.service';
import { Router } from '@angular/router';
import { TextInputComponent } from '../_forms/text-input/text-input.component';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [ReactiveFormsModule, TextInputComponent],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  private router = inject(Router);
  private fb = inject(FormBuilder);
  accountService = inject(AccountService);
  cancelLogin = output<boolean>();
  startRegister = output<boolean>();
  validationErrors: string[] = [];
  model: any = {};
  loginForm: FormGroup = new FormGroup({});

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
      this.loginForm = this.fb.group({
        email: ['', Validators.required],
        password: ['',[Validators.required]]
      });
    }
  
  login()
  {
    this.accountService.login(this.loginForm.value).subscribe({
      next: _ => {
        this.cancel()
        this.router.navigateByUrl("/news")
      },
      error: error => {
        this.validationErrors = error;
      }
    })
  }

  toggleToRegister(){
    this.startRegister.emit(true);
  }

  cancel(){
    this.cancelLogin.emit(false);
  }
}
