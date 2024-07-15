import { Component, inject, OnInit, output } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { NewsCardComponent } from '../news-card/news-card.component';
import { RouterLink } from '@angular/router';
import { HasRoleDirective } from '../../_directives/has-role.directive';
import { UserNewsCardComponent } from '../user-news-card/user-news-card.component';

@Component({
  selector: 'app-user-news',
  standalone: true,
  imports: [NewsCardComponent, RouterLink, HasRoleDirective, UserNewsCardComponent],
  templateUrl: './user-news.component.html',
  styleUrl: './user-news.component.css'
})
export class UserNewsComponent implements OnInit {
  accountService = inject(AccountService);
  

  ngOnInit(): void {
    if (!this.accountService.userNews()) this.loadNews();
  }

  loadNews(){
    this.accountService.getNews();
  }

}
