import { Component, inject, input, output } from '@angular/core';
import { News } from '../../_models/news';
import { Router, RouterLink } from '@angular/router';
import { NewsService } from '../../_services/news.service';
import { AccountService } from '../../_services/account.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-user-news-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-news-card.component.html',
  styleUrl: './user-news-card.component.css'
})
export class UserNewsCardComponent {
  private newsService = inject(NewsService);
  private accountService = inject(AccountService)
  private toastr = inject(ToastrService);
  news = input.required<News>();
  router = inject(Router);
  delete = output<boolean>();
  

  goToEdit(){
    this.router.navigateByUrl('myNews/' + this.news().id + '/edit');
  }

  deleteNews(){
    this.newsService.deleteNews(this.accountService.currentUser()?.schoolId!, this.news().id.toString()).subscribe({
      next: _ => {
        this.toastr.success('News deleted');
        this.delete.emit(true);
        const index = this.newsService.userNews()?.findIndex(n => n.id == this.news().id);
        if (index){
          this.newsService.userNews()?.splice(index,1);
        }
      }
    })
  }
}
