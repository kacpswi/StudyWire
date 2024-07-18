import { Component, inject, OnChanges, OnInit, output, SimpleChanges } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { NewsCardComponent } from '../news-card/news-card.component';
import { RouterLink } from '@angular/router';
import { HasRoleDirective } from '../../_directives/has-role.directive';
import { UserNewsCardComponent } from '../user-news-card/user-news-card.component';
import { NewsService } from '../../_services/news.service';
import { News } from '../../_models/news';

@Component({
  selector: 'app-user-news',
  standalone: true,
  imports: [NewsCardComponent, RouterLink, HasRoleDirective, UserNewsCardComponent],
  templateUrl: './user-news.component.html',
  styleUrl: './user-news.component.css'
})
export class UserNewsComponent implements OnInit {
  newsService = inject(NewsService);
  accountService = inject(AccountService);

  ngOnInit(): void {
    if (!this.newsService.userNews()){
      this.newsService.getUserNews()
      return;
    }
    if (this.newsService.userNewsChanged()){
      this.newsService.getUserNews();
      return;
    }
  }

  loadNews(){
    this.newsService.getUserNews();
  }

  reload(event: boolean){
    if (event){
      this.loadNews();
    }
  }
}
