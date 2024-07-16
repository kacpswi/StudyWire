import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';
import { authGuard } from './_guards/auth.guard';
import { teacherGuard } from './_guards/teacher.guard';
import { NewsNewComponent } from './news/news-new/news-new.component';
import { UserNewsComponent } from './news/user-news/user-news.component';
import { NewsEditComponent } from './news/news-edit/news-edit.component';
import { preventUnsavedChangesGuard } from './_guards/prevent-unsaved-changes.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';

export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate:[authGuard],
        children:[
            {path: 'news', component:NewsListComponent},
            {path: 'myNews', component:UserNewsComponent, canActivate:[teacherGuard]},
            {path: 'myNews/:newsId/edit', component:NewsEditComponent, canDeactivate:[preventUnsavedChangesGuard]},
            {path: 'schools/:schoolId/news/new', component:NewsNewComponent, canActivate:[teacherGuard], canDeactivate:[preventUnsavedChangesGuard]},
            {path: 'schools/:schoolId/news/:newsId', component: NewsDetailComponent},
        ]
    },
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-error', component: ServerErrorComponent},
    {path: '**', component: HomeComponent, pathMatch: 'full'},
];
