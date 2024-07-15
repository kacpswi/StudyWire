import { Component, HostListener, inject, ViewChild } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { newNews } from '../../_models/newNews';
import { AccountService } from '../../_services/account.service';
import { NewsService } from '../../_services/news.service';
import { Router, RouterLink } from '@angular/router';
import { environment } from '../../../environments/environment';

@Component({
  selector: 'app-news-new',
  standalone: true,
  imports: [FormsModule, RouterLink],
  templateUrl: './news-new.component.html',
  styleUrl: './news-new.component.css'
})
export class NewsNewComponent {
  @ViewChild('newNews') editNews?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    if(this.editNews?.dirty){
      $event.returnValue = true;
    }
  }
  accountService = inject(AccountService);
  newsService = inject(NewsService)
  model: any = {}
  newNewsId: number | null = null;
  router = inject(Router)
  baseUrl = environment.apiUrl;

  create(){
    if (this.accountService.currentUser()?.schoolId != null)
    {
      this.newsService.upload(this.model, this.accountService.currentUser()!.schoolId)
    }
  }
}
