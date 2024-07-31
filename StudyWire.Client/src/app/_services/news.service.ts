import { HttpClient, HttpHeaders, HttpParams, HttpResponse } from '@angular/common/http';
import { Injectable, inject, signal } from '@angular/core';
import { environment } from '../../environments/environment';
import { News } from '../_models/news';
import { PaginatedResult } from '../_models/pagination';
import { UserParams } from '../_models/userParams';
import { map, of, tap } from 'rxjs';
import { Router } from '@angular/router';
import { AccountService } from './account.service';

@Injectable({
  providedIn: 'root'
})
export class NewsService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;
  paginatedResults = signal<PaginatedResult<News[]> | null>(null);
  newsCache = new Map();
  userParams = signal<UserParams>(new UserParams("","CreatedAt","DESC")); 
  router = inject(Router);
  userNewsChanged = signal<boolean>(false);
  userNews = signal<News[] | null>(null);
  newsCacheChanged = signal<boolean>(false);

  resetUserParams(){
    this.userParams.set(new UserParams("","CreatedAt","DESC"))
  }

  getAllNews(){
    const response = this.newsCache.get(Object.values(this.userParams()).join('-')+'-allSchools');

    if (response) return this.setPaginatedResponse(response);

    let params = this.setPaginationHeaders(this.userParams().pageNumber, this.userParams().pageSize)

    params = params.append('searchPhrase', this.userParams().searchPhrase);
    params = params.append('sortBy', this.userParams().sortBy);
    params = params.append('sortDirection', this.userParams().sortDirection);

    return this.http.get<News[]>(this.baseUrl + 'news', {observe: 'response', params}).subscribe({
      next: response => {
        this.setPaginatedResponse(response);
        this.newsCache.set(Object.values(this.userParams()).join('-')+'-allSchools',response);
      }
    });
  }

  private setPaginatedResponse(response: HttpResponse<News[]>){
    this.paginatedResults.set({
      items:response.body as News[],
      pagination: JSON.parse(response.headers.get('Pagination')!)
    })
  }

  private setPaginationHeaders(pageNumber: number, pageSize: number){
    let params = new HttpParams();

    if(pageNumber && pageSize){
      params = params.append('pageNumber',pageNumber);
      params = params.append('pageSize', pageSize);
    }

    return params;
  }

  getNews(schoolId: number, newsId: string){
    const news: News = [...this.newsCache.values()]
      .reduce((arr, elem) => arr.concat(elem.body), [])
      .find((n: News) => n.schoolId === schoolId && n.id.toString() === newsId)

    if (news) return of(news);

    return this.http.get<News>(this.baseUrl + 'schools/' + schoolId + '/news/' + newsId)
  }
  

  getNewsForSchool(schoolId: number){
    const response = this.newsCache.get(Object.values(this.userParams()).join('-')+'-mySchool');
    if (response) return this.setPaginatedResponse(response);

    let params = this.setPaginationHeaders(this.userParams().pageNumber, this.userParams().pageSize)

    params = params.append('searchPhrase', this.userParams().searchPhrase);
    params = params.append('sortBy', this.userParams().sortBy);
    params = params.append('sortDirection', this.userParams().sortDirection);

    return this.http.get<News[]>(this.baseUrl + 'schools/' + schoolId + '/news', {observe: 'response', params}).subscribe({
      next: response => {
        this.setPaginatedResponse(response);
        this.newsCache.set(Object.values(this.userParams()).join('-')+'-mySchool',response);
      }
    });
  }

  upload(model: any, schoolId: number){
    this.userNewsChanged.set(true);
    this.newsCacheChanged.set(true);
    this.newsCache.clear();
    return this.http.post(this.baseUrl + 'schools/' + schoolId + '/news', model, {observe: 'response'})
  }

  updateNews<News>(news:News, schoolId: number, newsId: string){
    this.userNewsChanged.set(true);
    this.newsCacheChanged.set(true);
    this.newsCache.clear();
    return this.http.put(this.baseUrl + 'schools/' + schoolId + '/news/' + newsId, news)
    
  }

  deleteNews(schoolId: number, newsId: string){
    this.userNewsChanged.set(true);
    this.newsCacheChanged.set(true);
    this.newsCache.clear();
    return this.http.delete(this.baseUrl + 'schools/' + schoolId + '/news/' + newsId)
  }

  getUserNews(){
    this.userNewsChanged.set(false);
    return this.http.get<News[]>(this.baseUrl + "account/news").subscribe({ 
        next: response => {
          this.userNews.set(response)
        }})  
  }

  takeSomeNews(userId: string, newsId: string){
    const news: News[] = [...this.newsCache.values()]
      .reduce((arr, elem) => arr.concat(elem.body), [])
      .filter((n: News) => n.createdById.toString() !== userId && n.id.toString() !== newsId)

    const shuffledNewsList = this.shuffleArray(news)
    return shuffledNewsList.slice(0,3);
  }

  private shuffleArray(array: any[]): any[] {
    for (let i = array.length - 1; i > 0; i--) {
      const j = Math.floor(Math.random() * (i + 1));
      [array[i], array[j]] = [array[j], array[i]];
    }
    return array;
  }
}
