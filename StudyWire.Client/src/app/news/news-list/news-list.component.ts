import { Component, OnInit, inject } from '@angular/core';
import { NewsesService } from '../../_services/newses.service';
import { News } from '../../_models/news';
import { NewsCardComponent } from "../news-card/news-card.component";

@Component({
    selector: 'app-news-list',
    standalone: true,
    templateUrl: './news-list.component.html',
    styleUrl: './news-list.component.css',
    imports: [NewsCardComponent]
})
export class NewsListComponent implements OnInit {
  private newsesService = inject(NewsesService);
  newses: News[] = [];

  ngOnInit(): void {
    this.loadNewses();
  }

  loadNewses(){
    this.newsesService.getNewses().subscribe({
      next: newses => this.newses = newses
    })
  }
}
