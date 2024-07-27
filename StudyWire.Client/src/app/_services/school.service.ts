import { HttpClient } from '@angular/common/http';
import { inject, Injectable, signal } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';
import { School } from '../_models/school';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {
  private router = inject(Router);
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  userSchool = signal<School | null>(null);


  upload(model: any){
    return this.http.post(this.baseUrl + 'schools/', model, {observe: 'response'})
  }

  getSchoolById(schoolId: string){
    return this.http.get<School>(this.baseUrl + 'schools/' + schoolId).subscribe({
      next: response => {
        this.userSchool.set(response);
      }
    })
  }
}
