import { Component, inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment } from '../../../environments/environment';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { SchoolService } from '../../_services/school.service';
import { AccountService } from '../../_services/account.service';

@Component({
  selector: 'app-school-new',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, TextInputComponent],
  templateUrl: './school-new.component.html',
  styleUrl: './school-new.component.css'
})
export class SchoolNewComponent implements OnInit{
  private fb = inject(FormBuilder);
  private toastr = inject(ToastrService);
  private schoolService = inject(SchoolService);
  private accountService = inject(AccountService);
  model: any = {}
  newSchoolId: number | null = null;
  router = inject(Router)
  baseUrl = environment.apiUrl;
  newSchoolForm: FormGroup = new FormGroup({});
  validationErrors: string[] = [];

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm(){
    this.newSchoolForm = this.fb.group({
      name: ['', Validators.required],
      phoneNumber: ['', Validators.required],
      street: ['', Validators.required],
      city: ['', Validators.required],
      postalCode: ['', Validators.required],
    });
  }

  create(){
    this.schoolService.upload(this.newSchoolForm.value).subscribe({
      next: response => {
        this.toastr.success("School Created");
        
        // api/schools/id
        const location = response.headers.get('Location');
        const idMatch = location!.match(/api\/schools\/(\d+)/);
        const schoolId = idMatch ? parseInt(idMatch[1], 10) : null;
        let user = this.accountService.currentUser();
        if (user) user.schoolId = schoolId;
        this.accountService.currentUser.set(user);
        this.schoolService.userSchool.set(this.newSchoolForm.value);
        localStorage.removeItem("user");
        localStorage.setItem("user", JSON.stringify(user));
        this.router.navigateByUrl("/mySchool");
      },
      error: error => {
        this.toastr.error("School could not be created");
        this.validationErrors = error;
      }
    })
  }
}
