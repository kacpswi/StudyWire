import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { NewsCardComponent } from '../news-card/news-card.component';
import { RouterLink } from '@angular/router';
import { HasRoleDirective } from '../../_directives/has-role.directive';

@Component({
  selector: 'app-user-news',
  standalone: true,
  imports: [NewsCardComponent, RouterLink, HasRoleDirective],
  templateUrl: './user-news.component.html',
  styleUrl: './user-news.component.css'
})
export class UserNewsComponent implements OnInit {
  accountService = inject(AccountService)

  ngOnInit(): void {
    this.accountService.getNews();
  }
}
