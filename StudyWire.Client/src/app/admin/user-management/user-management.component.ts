import { Component, inject, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { User } from '../../_models/user';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../modals/roles-modal/roles-modal.component';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [FormsModule, PaginationModule, ButtonsModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent implements OnInit{
  private modalService = inject(BsModalService);
  adminService = inject(AdminService);
  sortByList = [{value:'Name', display: 'Name'},{value:'Surename', display:'Surename'},{value:'SchoolId',display:'School'}]
  itemsPerPageList = ['12','16','20']
  bsModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();

  ngOnInit(): void {
    if (!this.adminService.paginatedResults()){
      this.getUsersWithRoles();
    }
  }

  openRolesModal(user: User){
    const initialState: ModalOptions ={
      class: 'modal-lg',
      initialState:{
        title: 'User roles',
        userId: user.id,
        selectedRoles:[...user.userRoles],
        availableRoles: ['Admin', 'School-Admin', 'Teacher', 'Student', 'Guest'],
        users: this.adminService.paginatedResults()?.items,
        rolesUpdated: false
      }
    }
    this.bsModalRef = this.modalService.show(RolesModalComponent, initialState);
    this.bsModalRef.onHide?.subscribe({
      next: () => {
        if (this.bsModalRef.content && this.bsModalRef.content.rolesUpdated){
          const selectedRoles = this.bsModalRef.content.selectedRoles;
          this.adminService.updateUserRoles(user.id.toString(), selectedRoles).subscribe({
            next: response => {
              user.userRoles = response.userRoles;
              user.schoolName = response.schoolName;
            }
          })
        }
      }
    })
  }

  getUsersWithRoles(){
    this.adminService.getUserWithRoles()
  }

  resetFilters(){
    this.adminService.resetUserParams();
    this.getUsersWithRoles();
  }

  pageChange(event:any){
    if(this.adminService.userParams().pageNumber !== event.page){
      this.adminService.userParams().pageNumber = event.page;
      this.getUsersWithRoles();
    }
  }
}
