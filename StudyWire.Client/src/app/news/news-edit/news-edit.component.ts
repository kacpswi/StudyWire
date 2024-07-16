import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { News } from '../../_models/news';
import { AccountService } from '../../_services/account.service';
import { NewsService } from '../../_services/news.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';

@Component({
  selector: 'app-news-edit',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './news-edit.component.html',
  styleUrl: './news-edit.component.css'
})
export class NewsEditComponent implements OnInit{
  @ViewChild('editNews') editNews?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    if(this.editNews?.dirty){
      $event.returnValue = true;
    }
  }
  news: News | null = null;
  private accountService = inject(AccountService);
  private newsService = inject(NewsService);
  private route = inject(ActivatedRoute);
  validationErrors: string[] = [];
  newsId: string | null = null;

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(){
    const user = this.accountService.currentUser();
    this.newsId = this.route.snapshot.paramMap.get('newsId');
    if (!user && !this.newsId){
      return;
    }
    else{
      this.newsService.getNews(user!.schoolId, this.newsId!).subscribe({
        next: news => {
          this.news = news
        },
        error: error =>{
          this.validationErrors = error;
        }
    });
    }
  }

  edit(){
    if (this.newsId)
    {
      this.newsService.updateNews(this.editNews?.value, this.accountService.currentUser()!.schoolId, this.newsId!).subscribe({
        next: _ =>{
          this.editNews?.reset(this.news);
        }
      })
      
    }
  }
}
