import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { HttpClient, HttpParams, HttpResponse } from '@angular/common/http';
import { User } from '../_models/user';
import { UserParams } from '../_models/userParams';
import { PaginatedResult } from '../_models/pagination';
import { School } from '../_models/school';

@Injectable({
  providedIn: 'root'
})
export class AdminService {
  baseUrl = environment.apiUrl;
  private http = inject(HttpClient);
  userParams = signal<UserParams>(new UserParams("","Name","ASC")); 
  paginatedResults = signal<PaginatedResult<User[]> | null>(null);
  userCache = new Map();

  getUserWithRoles(){
    const response = this.userCache.get(Object.values(this.userParams()).join('-'));
    if (response) return this.setPaginatedResponse(response);

    let params = this.setPaginationHeaders();

    return this.http.get<User[]>(this.baseUrl + 'admin/users-with-roles', {observe: 'response', params}).subscribe({
      next: response => {
        this.setPaginatedResponse(response);
        this.userCache.set(Object.values(this.userParams()).join('-'), response);
      }
    });
  }

  updateUserRoles(userId: string, roles: string[]){
    return this.http.post<User>(this.baseUrl + 'admin/edit-roles/' + userId + '?roles=' + roles, {})
  }

  updateUserSchool(userId: string, schoolName: string){
    return this.http.post<School>(this.baseUrl + 'admin/edit-school/' + userId + '?schoolId=' + schoolName, {})
  }

  resetUserParams(){
    this.userParams.set(new UserParams("","Name","ASC"))
  }

  deleteUser(userId: string){
    return this.http.delete(this.baseUrl + 'admin/delete-user/' + userId)
  }

  private setPaginationHeaders(){
    let params = new HttpParams();

      params = params.append('searchPhrase', this.userParams().searchPhrase);
      params = params.append('sortBy', this.userParams().sortBy);
      params = params.append('sortDirection', this.userParams().sortDirection);
      params = params.append('pageNumber',this.userParams().pageNumber);
      params = params.append('pageSize', this.userParams().pageSize);
  

    return params;
  }

  private setPaginatedResponse(response: HttpResponse<User[]>){
    this.paginatedResults.set({
      items:response.body as User[],
      pagination: JSON.parse(response.headers.get('Pagination')!)
    })
  }
}
