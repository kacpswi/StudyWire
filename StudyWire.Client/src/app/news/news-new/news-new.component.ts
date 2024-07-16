import { Component, HostListener, inject, OnInit, ViewChild } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, NgForm, ReactiveFormsModule, Validators } from '@angular/forms';
import { newNews } from '../../_models/newNews';
import { AccountService } from '../../_services/account.service';
import { NewsService } from '../../_services/news.service';
import { Router, RouterLink } from '@angular/router';
import { environment } from '../../../environments/environment';
import { TextInputComponent } from '../../_forms/text-input/text-input.component';
import { NewsFormComponent } from '../../_forms/news-form/news-form.component';

@Component({
  selector: 'app-news-new',
  standalone: true,
  imports: [ReactiveFormsModule, RouterLink, NewsFormComponent],
  templateUrl: './news-new.component.html',
  styleUrl: './news-new.component.css'
})
export class NewsNewComponent implements OnInit {
  @ViewChild('newNews') editNews?: NgForm;
  @HostListener('window:beforeunload', ['$event']) notify($event:any){
    if(this.editNews?.dirty){
      $event.returnValue = true;
    }
  }
  private fb = inject(FormBuilder);
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

  initializeForm(){
    this.newNewsForm = this.fb.group({
      title: ['', Validators.required],
      description: ['',[Validators.required, Validators.minLength(6), Validators.maxLength(50)]],
      content: ['', [Validators.required]],
    });
  }

  create(){
    if (this.accountService.currentUser()?.schoolId != null)
    {
      this.newsService.upload(this.newNewsForm.value, this.accountService.currentUser()!.schoolId).subscribe({
        next: (response) =>{
          const location = response.headers.get('Location')
          this.router.navigateByUrl('/' + location)
        },
        error: error =>{
          this.validationErrors = error;
        }
      })
    }
  }
}
