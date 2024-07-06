import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { environment } from '../../environments/environment';
import { News } from '../_models/news';

@Injectable({
  providedIn: 'root'
})
export class NewsesService {
  private http = inject(HttpClient);
  baseUrl = environment.apiUrl;

  getNewses(){
    return this.http.get<News[]>(this.baseUrl + 'newses');
  }

  getNews(schoolId: string, newsId: string){
    return this.http.get<News>(this.baseUrl + 'schools/' + schoolId + '/newses/' + newsId);
  }

  getNewsesForSchool(schoolId: string){
    return this.http.get<News[]>(this.baseUrl + 'schools/' + schoolId);
  }
}
