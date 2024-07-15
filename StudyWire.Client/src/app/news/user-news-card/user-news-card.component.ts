import { Component, inject, input, output } from '@angular/core';
import { News } from '../../_models/news';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-user-news-card',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './user-news-card.component.html',
  styleUrl: './user-news-card.component.css'
})
export class UserNewsCardComponent {
  news = input.required<News>();
  router = inject(Router)

  goToEdit(){
    this.router.navigateByUrl('myNews/' + this.news().id + '/edit');
  }
}
