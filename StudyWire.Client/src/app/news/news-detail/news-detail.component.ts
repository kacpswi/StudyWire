import { Component, OnInit, inject } from '@angular/core';
import { NewsesService } from '../../_services/newses.service';
import { ActivatedRoute } from '@angular/router';
import { News } from '../../_models/news';

@Component({
  selector: 'app-news-detail',
  standalone: true,
  imports: [],
  templateUrl: './news-detail.component.html',
  styleUrl: './news-detail.component.css'
})
export class NewsDetailComponent implements OnInit{
  private newsesService = inject(NewsesService);
  private route = inject(ActivatedRoute);
  news?: News;

  ngOnInit(): void {
    this.loadNews();
  }

  loadNews(){
    const schoolId = this.route.snapshot.paramMap.get('schoolId');
    const newsId = this.route.snapshot.paramMap.get('newsId');
    if (!newsId || !schoolId) 
      {
        console.log("hi");
        return;
      }
    this.newsesService.getNews(schoolId, newsId).subscribe({
      next: news => this.news = news
    });
  }
}
