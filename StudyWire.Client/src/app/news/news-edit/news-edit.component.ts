import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { News } from '../../_models/news';
import { AccountService } from '../../_services/account.service';
import { NewsService } from '../../_services/news.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { FormsModule, NgForm } from '@angular/forms';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-news-edit',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './news-edit.component.html',
  styleUrl: './news-edit.component.css'
})
export class NewsEditComponent implements OnInit{
  @ViewChild('form') form?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    if(this.form?.dirty){
      $event.returnValue = true;
    }
  }
  private accountService = inject(AccountService);
  private newsService = inject(NewsService);
  private route = inject(ActivatedRoute);
  private toastr = inject(ToastrService);
  private router = inject(Router);
  validationErrors: string[] = [];
  newsId: string | null = null;
  news: News | null = null;


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
      this.newsService.getNews(user!.schoolId!, this.newsId!).subscribe({
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
      this.newsService.updateNews(this.form?.value, this.accountService.currentUser()!.schoolId!, this.newsId!).subscribe({
        next: _ =>{
          this.toastr.success("News updated.")
          this.form?.reset(this.news);
        },
        error: error =>{
          this.validationErrors = error;
        }
      })
      
    }
  }
}
