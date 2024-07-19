import { Component, inject } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { SchoolService } from '../../_services/school.service';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-school-details',
  standalone: true,
  imports: [RouterLink],
  templateUrl: './school-details.component.html',
  styleUrl: './school-details.component.css'
})
export class SchoolDetailsComponent {
  private router = inject(Router);
  accountService = inject(AccountService);
  schoolService = inject(SchoolService);
  
}
