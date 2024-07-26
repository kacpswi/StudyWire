import { HttpClient } from '@angular/common/http';
import { Injectable, computed, inject, signal } from '@angular/core';
import { User } from '../_models/user';
import { map } from 'rxjs';
import { environment } from '../../environments/environment';
import { News } from '../_models/news';
import { NewsService } from './news.service';
import { UserParams } from '../_models/userParams';

@Injectable({
  providedIn: 'root'
})
export class AccountService {
  private http = inject(HttpClient);
  private newsService = inject(NewsService);
  baseUrl = environment.apiUrl;
  currentUser = signal<User | null>(null);
  
  
  roles = computed(() => {
    const user = this.currentUser();
    if (user && user.token) {
      try {
        const parts = user.token.split('.');
        if (parts.length !== 3) {
          throw new Error('Invalid token structure');
        }
        const base64 = parts[1].replace(/-/g, '+').replace(/_/g, '/');
        const jsonPayload = atob(base64.padEnd(base64.length + (4 - base64.length % 4) % 4, '='));
        const payload = JSON.parse(jsonPayload);
        const role = payload.role;
        return Array.isArray(role) ? role : [role];
      } catch (e) {
        console.error('Failed to decode token or extract roles', e);
        return [];
      }
    }
    return [];
  })

  login(model: any){
    return this.http.post<User>(this.baseUrl + "account/login", model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }

  register(model: any){
    return this.http.post<User>(this.baseUrl + "account/register", model).pipe(
      map(user => {
        if (user) {
          localStorage.setItem("user", JSON.stringify(user));
          this.currentUser.set(user);
        }
        return user;
      })
    )
  }
  
  logout(){
    localStorage.removeItem("user");
    this.newsService.newsCache.clear();
    this.newsService.userNews.set(null);
    this.newsService.paginatedResults.set(null);
    this.newsService.userParams.set(new UserParams("","CreatedAt","DESC"));
    this.newsService.userNewsChanged.set(false);
    this.newsService.newsCacheChanged.set(false);
    this.currentUser.set(null);
  }


}

