import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SchoolService {
  private router = inject(Router);
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;


  upload(model: any){
    return this.http.post(this.baseUrl + 'schools/', model, {observe: 'response'})
  }

}
