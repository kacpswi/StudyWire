import { Component, input, output } from '@angular/core';
import { News } from '../../_models/news';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-news-card',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './news-card.component.html',
  styleUrl: './news-card.component.css'
})
export class NewsCardComponent {
  news = input.required<News>();
  goToDetail = output<boolean>();

  reload(){
    this.goToDetail.emit(true);
  }
}
