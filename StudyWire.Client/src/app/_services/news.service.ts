import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { News } from '../_models/news';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResults = signal<PaginatedResult<News[]> | null>(null)

  getAllNews(userParams: UserParams){

    let params = this.setPaginationHeaders(userParams.pageNumber, userParams.pageSize)

    params = params.append('searchPhrase', userParams.searchPhrase);
    params = params.append('sortBy', userParams.sortBy);
    params = params.append('sortDirection', userParams.sortDirection);

    return this.http.get<News[]>(this.baseUrl + 'news', {observe: 'response', params}).subscribe({
      next: response => {
        this.paginatedResults.set({
          items:response.body as News[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    });
  }

  private setPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();

    if(pageNumber && pageSize){
      params = params.append('pageNumber',pageNumber);
      params = params.append('pageSize', pageSize);
    }

    return params;
  }

  getNews(schoolId: string, newsId: string){
    return this.http.get<News>(this.baseUrl + 'schools/' + schoolId + '/news/' + newsId);
  }

  getNewsForSchool(userParams: UserParams, schoolId: string){
    let params = this.setPaginationHeaders(userParams.pageNumber, userParams.pageSize)

    params = params.append('searchPhrase', userParams.searchPhrase);
    params = params.append('sortBy', userParams.sortBy);
    params = params.append('sortDirection', userParams.sortDirection);

    return this.http.get<News[]>(this.baseUrl + 'schools/' + schoolId + '/news', {observe: 'response', params}).subscribe({
      next: response => {
        this.paginatedResults.set({
          items:response.body as News[],
          pagination: JSON.parse(response.headers.get('Pagination')!)
        })
      }
    });
  }
}
