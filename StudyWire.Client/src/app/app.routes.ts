import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';
import { authGuard } from './_guards/auth.guard';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate:[authGuard],
        children:[
            {path: 'news', component:NewsListComponent},
            {path: 'news/:id', component: NewsDetailComponent},
        ]
    },
    {path: '**', component: HomeComponent, pathMatch: 'full'},
];
