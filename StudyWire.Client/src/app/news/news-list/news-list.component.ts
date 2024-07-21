import { Component, OnInit, inject } from '@angular/core';
import { NewsService } from '../../_services/news.service';
import { NewsCardComponent } from "../news-card/news-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
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
  sortByList = [{value:'CreatedAt', display: 'Date'},{value:'SchoolId', display:'School'},{value:'Author',display:'Author'}]
  itemsPerPageList = ['12','16','20']
  newsFrom: string = "userSchool";

  ngOnInit(): void {
    if (!this.newsService.paginatedResults() || this.newsService.newsCacheChanged()){
      this.loadNews();
      this.newsService.newsCacheChanged.set(false);
    }
  }

  loadNews(){
    if (this.accountService.currentUser()?.schoolId == null)
    {
      this.newsService.getAllNews();
    }
    else if(this.newsFrom == "userSchool"){
      this.newsService.getNewsForSchool(this.accountService.currentUser()!.schoolId);
    }
    else if(this.newsFrom == "allSchools"){
      this.newsService.getAllNews();
    }
  }

  resetFilters(){
    this.newsService.resetUserParams();
    this.newsFrom = "userSchool"
    this.loadNews;
  }

  pageChange(event:any){
    if(this.newsService.userParams().pageNumber !== event.page){
      this.newsService.userParams().pageNumber = event.page;
      this.loadNews()
    }
  }
}
