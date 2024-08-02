import { Component, inject, OnInit } from '@angular/core';
import { AccountService } from '../../_services/account.service';
import { SchoolService } from '../../_services/school.service';
import { Router, RouterLink } from '@angular/router';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { MemberManagementComponent } from "../member-management/member-management.component";
import { HasRoleDirective } from '../../_directives/has-role.directive';

@Component({
  selector: 'app-school-details',
  standalone: true,
  imports: [RouterLink, TabsModule, MemberManagementComponent, HasRoleDirective],
  templateUrl: './school-details.component.html',
  styleUrl: './school-details.component.css'
})
export class SchoolDetailsComponent implements OnInit{
  private router = inject(Router);
  accountService = inject(AccountService);
  schoolService = inject(SchoolService);
  
  ngOnInit(): void {
    if (this.accountService.currentUser()?.schoolId==null || this.schoolService.userSchool())
      {
        return;
      } 
    this.getSchool(this.accountService.currentUser()?.schoolId!);
  }

  getSchool(schoolId: number)
  {
    this.schoolService.getSchoolById(schoolId.toString())
  }
}
