import { Routes } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { NewsListComponent } from './news/news-list/news-list.component';
import { NewsDetailComponent } from './news/news-detail/news-detail.component';
import { authGuard } from './_guards/auth.guard';
import { teacherGuard } from './_guards/teacher.guard';
import { NewsNewComponent } from './news/news-new/news-new.component';
import { UserNewsComponent } from './news/user-news/user-news.component';
import { NewsEditComponent } from './news/news-edit/news-edit.component';
import { preventEditUnsavedChangesGuard} from './_guards/prevent-edit-unsaved-changes.guard';
import { NotFoundComponent } from './errors/not-found/not-found.component';
import { ServerErrorComponent } from './errors/server-error/server-error.component';
import { schoolAdminGuard } from './_guards/school-admin.guard';
import { SchoolDetailsComponent } from './schools/school-details/school-details.component';
import { SchoolNewComponent } from './schools/school-new/school-new.component';
import { preventNewUnsavedChanges } from './_guards/prevent-new-unsaved-changes.guard';
import { AdminPanelComponent } from './admin/admin-panel/admin-panel.component';
import { adminGuard } from './_guards/admin.guard';


export const routes: Routes = [
    {path: '', component: HomeComponent},
    {
        path: '',
        runGuardsAndResolvers: 'always',
        canActivate:[authGuard],
        children:[
            {path: 'news', component:NewsListComponent},
            {path: 'myNews', component:UserNewsComponent, canActivate:[teacherGuard]},
            {path: 'myNews/:newsId/edit', component:NewsEditComponent, canDeactivate:[preventEditUnsavedChangesGuard]},
            {path: 'mySchool', component:SchoolDetailsComponent, canActivate:[schoolAdminGuard]},
            {path: 'mySchool/new', component:SchoolNewComponent, canActivate:[schoolAdminGuard]},
            {path: 'schools/:schoolId/news/new', component:NewsNewComponent, canActivate:[teacherGuard], canDeactivate:[preventNewUnsavedChanges]},
            {path: 'schools/:schoolId/news/:newsId', component: NewsDetailComponent},
            {path: 'admin', component: AdminPanelComponent, canActivate:[adminGuard]},
        ]
    },
    {path: 'not-found', component: NotFoundComponent},
    {path: 'server-error', component: ServerErrorComponent},
    {path: '**', component: HomeComponent, pathMatch: 'full'},
];
