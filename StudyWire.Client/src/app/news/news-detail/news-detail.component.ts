import { Component, OnInit, inject } from '@angular/core';
import { NewsService } from '../../_services/news.service';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { News } from '../../_models/news';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-news-detail',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './news-detail.component.html',
  styleUrl: './news-detail.component.css'
})
export class NewsDetailComponent implements OnInit{
  private newsesService = inject(NewsService);
  private route = inject(ActivatedRoute);
  news?: News;

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(){
    const schoolId = this.route.snapshot.paramMap.get('schoolId');
    const newsId = this.route.snapshot.paramMap.get('newsId');
    if (!newsId || !schoolId) return;
    this.newsesService.getNews(schoolId, newsId).subscribe({
        next: news => this.news = news
    });
  }
}
