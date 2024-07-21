import { AfterViewInit, Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { AccountService } from '../../_services/account.service';
import { NewsService } from '../../_services/news.service';
import { Router, RouterLink } from '@angular/router';
import { environment } from '../../../environments/environment';
import { NewsFormComponent } from '../../_forms/news-form/news-form.component';
import { ToastrService } from 'ngx-toastr';
import { News } from '../../_models/news';

@Component({
  selector: 'app-news-new',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, NewsFormComponent],
  templateUrl: './news-new.component.html',
  styleUrl: './news-new.component.css'
})
export class NewsNewComponent implements OnInit, AfterViewInit {
  @ViewChild('newNewsForm') newNews?: FormGroup;
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    if(this.newNews?.dirty){
      $event.returnValue = true;
    }
  }
  private fb = inject(FormBuilder);
  private toastr = inject(ToastrService);
  accountService = inject(AccountService);
  newsService = inject(NewsService)
  model: any = {}
  newNewsId: number | null = null;
  router = inject(Router)
  baseUrl = environment.apiUrl;
  newNewsForm: FormGroup = new FormGroup({});
  validationErrors: string[] = [];

  ngOnInit(): void {
    this.initializeForm();
  }

  ngAfterViewInit(): void {
    this.newNews = this.newNewsForm
  }

  initializeForm(){
    this.newNewsForm = this.fb.group({
      title: ['', [Validators.required, Validators.maxLength(70)]],
      description: ['',[Validators.required, Validators.maxLength(100)]],
      content: ['', [Validators.required, Validators.minLength(200)]],
    });
  }

  create(){
    if (this.accountService.currentUser()?.schoolId != null)
    {
      this.newsService.upload(this.newNewsForm.value, this.accountService.currentUser()!.schoolId).subscribe({
        next: (response) =>{
          this.toastr.success('Created news');
          this.newsService.userNews()?.push(response.body as News)
          this.newNewsForm.reset();
          const location = response.headers.get('Location');
          this.router.navigateByUrl('/' + location);
        },
        error: error =>{
          this.validationErrors = error;
        }
      })
    }
  }
}
