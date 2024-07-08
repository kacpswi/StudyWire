import { Component, OnInit, inject } from '@angular/core';
import { NewsService } from '../../_services/news.service';
import { NewsCardComponent } from "../news-card/news-card.component";
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { UserParams } from '../../_models/userParams';
import { FormsModule } from '@angular/forms';
import { ButtonsModule } from 'ngx-bootstrap/buttons';

@Component({
    selector: 'app-news-list',
    standalone: true,
    templateUrl: './news-list.component.html',
    styleUrl: './news-list.component.css',
    imports: [NewsCardComponent, PaginationModule, FormsModule, ButtonsModule]
})
export class NewsListComponent implements OnInit {
  newsService = inject(NewsService);
  userParams = new UserParams(null,null,null);
  sortByList = [{value:'CreatedAt', display: 'Date'},{value:'SchoolId', display:'School'},{value:'Author',display:'Author'}]

  ngOnInit(): void {
    if (!this.newsService.paginatedResults()) this.loadNews();
  }

  loadNews(){
    this.newsService.getAllNews(this.userParams);
  }

  resetFilters(){
    this.userParams = new UserParams(null, null, null);
    this.loadNews;
  }

  pageChange(event:any){
    if(this.userParams.pageNumber !== event.page){
      this.userParams.pageNumber = event.page;
      this.loadNews()
    }
  }
}
