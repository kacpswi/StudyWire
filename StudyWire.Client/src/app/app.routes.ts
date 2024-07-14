import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';
import { authGuard } from './_guards/auth.guard';
import { teacherGuard } from './_guards/teacher.guard';
import { NewsNewComponent } from './news/news-new/news-new.component';
import { UserNewsComponent } from './news/user-news/user-news.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate:[authGuard],
        children:[
            {path: 'news', component:NewsListComponent},
            {path: 'myNews', component:UserNewsComponent},
            {path: 'schools/:schoolId/news/new', component:NewsNewComponent, canActivate:[teacherGuard]},
            {path: 'schools/:schoolId/news/:newsId', component: NewsDetailComponent},
        ]
    },
    {path: '**', component: HomeComponent, pathMatch: 'full'},
];
