import { Component, OnInit, inject } from '@angular/core';
import { NewsService } from '../../_services/news.service';
import { NewsCardComponent } from "../news-card/news-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserParams } from '../../_models/userParams';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { AccountService } from '../../_services/account.service';
import { RouterLink } from '@angular/router';
import { HasRoleDirective } from '../../_directives/has-role.directive';

@Component({
    selector: 'app-news-list',
    standalone: true,
    templateUrl: './news-list.component.html',
    styleUrl: './news-list.component.css',
    imports: [NewsCardComponent, PaginationModule, FormsModule, ButtonsModule, RouterLink, HasRoleDirective]
})
export class NewsListComponent implements OnInit {
  newsService = inject(NewsService);
  accountService = inject(AccountService);
  userParams = new UserParams(null,null,null);
  sortByList = [{value:'CreatedAt', display: 'Date'},{value:'SchoolId', display:'School'},{value:'Author',display:'Author'}]
  itemsPerPageList = ['12','16','20']
  newsFromUserSchool: string = "true";

  ngOnInit(): void {
    if (!this.newsService.paginatedResults()) this.loadNews();
  }

  loadNews(){
    if (this.accountService.currentUser()?.schoolId == null)
    {
      this.newsService.getAllNews(this.userParams);
    }
    else if(this.newsFromUserSchool == "true"){
      this.newsService.getNewsForSchool(this.userParams, this.accountService.currentUser()!.schoolId);
    }
    else if(this.newsFromUserSchool == "false"){
      this.newsService.getAllNews(this.userParams);
    }
  }

  resetFilters(){
    this.userParams = new UserParams(null, null, null);
    this.newsFromUserSchool = "true"
    this.loadNews;
  }

  pageChange(event:any){
    if(this.userParams.pageNumber !== event.page){
      this.userParams.pageNumber = event.page;
      this.loadNews()
    }
  }
}
