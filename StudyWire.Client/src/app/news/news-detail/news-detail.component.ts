import { Component, OnInit, inject } from '@angular/core';
import { NewsService } from '../../_services/news.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { News } from '../../_models/news';
import { CommonModule } from '@angular/common';
import { AccountService } from '../../_services/account.service';
import { NewsCardComponent } from '../news-card/news-card.component';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-news-detail',
  standalone: true,
  imports: [CommonModule, RouterLink, NewsCardComponent],
  templateUrl: './news-detail.component.html',
  styleUrl: './news-detail.component.css'
})
export class NewsDetailComponent implements OnInit{
  private newsService = inject(NewsService);
  private route = inject(ActivatedRoute);
  private router = inject(Router);
  accountService = inject(AccountService);
  newsDetail?: News;
  newsCards?: News[];
  schoolId: string | null = null;
  newsId: string | null = null;


  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(){
    this.route.paramMap.subscribe({
      next: param => {
        this.schoolId = param.get('schoolId');
        this.newsId = param.get('newsId');

        this.newsService.getNews(Number(this.schoolId), this.newsId!).subscribe({
          next: news =>{
             this.newsDetail = news
             this.newsCards = this.newsService.takeSomeNews(this.accountService.currentUser()!.id.toString(), this.newsDetail!.id.toString())
          }
      });
      }
    })
  }

  reload(event: boolean){
    if(event)
    {
      this.loadNews();
    } 
  }

  goToEdit(){
    this.router.navigateByUrl('myNews/' + this.newsDetail!.id + '/edit')
  }
}
