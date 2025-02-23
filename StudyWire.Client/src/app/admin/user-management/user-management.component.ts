import { Component, inject, OnInit } from '@angular/core';
import { AdminService } from '../../_services/admin.service';
import { User } from '../../_models/user';
import { FormsModule } from '@angular/forms';
import { PaginationModule } from 'ngx-bootstrap/pagination';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsModalRef, BsModalService, ModalOptions } from 'ngx-bootstrap/modal';
import { RolesModalComponent } from '../../modals/roles-modal/roles-modal.component';
import { DeleteModalComponent } from '../../modals/delete-modal/delete-modal.component';
import { ToastrService } from 'ngx-toastr';
import { systemRoles } from '../../_models/systemRoles';
import { EditSchoolModalComponent } from '../../modals/edit-school-modal/edit-school-modal.component';
import { SchoolService } from '../../_services/school.service';
import { lastValueFrom } from 'rxjs';

@Component({
  selector: 'app-user-management',
  standalone: true,
  imports: [FormsModule, PaginationModule, ButtonsModule],
  templateUrl: './user-management.component.html',
  styleUrl: './user-management.component.css'
})
export class UserManagementComponent implements OnInit{
  private toastr = inject(ToastrService);
  private modalService = inject(BsModalService);
  private schoolService = inject(SchoolService);
  adminService = inject(AdminService);
  sortByList = [{value:'Name', display: 'Name'},{value:'Surename', display:'Surename'},{value:'SchoolId',display:'School'}]
  itemsPerPageList = ['12','16','20']
  bsRolesModalRef: BsModalRef<RolesModalComponent> = new BsModalRef<RolesModalComponent>();
  bsDeleteModalRef: BsModalRef<DeleteModalComponent> = new BsModalRef<DeleteModalComponent>();
  bsEditSchoolModalRef: BsModalRef<EditSchoolModalComponent> = new BsModalRef<EditSchoolModalComponent>();
  roles = new systemRoles;

  ngOnInit(): void {
    if (!this.adminService.paginatedResults()){
      this.getUsersWithRoles();
    }
  }

  openRolesModal(user: User){
    const initialState: ModalOptions ={
      class: 'modal-mg',
      initialState:{
        title: 'User roles',
        name: user.name,
        userId: user.id,
        selectedRoles:[...user.userRoles],
        availableRoles: this.roles.avaliableRoles,
        users: this.adminService.paginatedResults()?.items,
        rolesUpdated: false
      }
    }
    this.bsRolesModalRef = this.modalService.show(RolesModalComponent, initialState);
    this.bsRolesModalRef.onHide?.subscribe({
      next: () => {
        if (this.bsRolesModalRef.content && this.bsRolesModalRef.content.rolesUpdated){
          const selectedRoles = this.bsRolesModalRef.content.selectedRoles;
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

  openDeleteModal(user: User){
    const inisialState: ModalOptions =
    {
      class: 'modal-mg',
      initialState: {
        deleteItem: false,
        itemNameToDelete: "User"
      }
    }
    this.bsDeleteModalRef = this.modalService.show(DeleteModalComponent, inisialState);
    this.bsDeleteModalRef.onHide?.subscribe({
      next: () => {
        if(this.bsDeleteModalRef.content && this.bsDeleteModalRef.content.deleteItem)
        {
          this.deleteUser(user)
        }
      }
    })
  }

  async openEditSchoolModal(user: User){
    if (!this.schoolService.schools()) {
      await lastValueFrom(this.schoolService.getAllSchools());
    }
    const initialState: ModalOptions ={
      class: 'modal-mg',
      initialState:{
        title: 'Schools',
        schoolName: user.schoolName,
        userId: user.id,
        userSchool:user.schoolId,
        availableSchools: this.schoolService.schools(),
        users: this.adminService.paginatedResults()?.items,
        schoolUpdated: false
      }
    }
    this.bsEditSchoolModalRef = this.modalService.show(EditSchoolModalComponent, initialState);
    this.bsEditSchoolModalRef.onHide?.subscribe({
      next: () => {
        if (this.bsEditSchoolModalRef.content && this.bsEditSchoolModalRef.content.schoolUpdated){
          const selectedSchool = this.bsEditSchoolModalRef.content.valueSelected;
          this.adminService.updateUserSchool(user.id.toString(), selectedSchool.toString()).subscribe({
            next: response => {
              user.schoolName = response.name;
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

  deleteUser(user: User){
    this.adminService.deleteUser(user.id.toString()).subscribe({
      next: _ =>{
        this.toastr.success('User Deleted')
        const index = this.adminService.paginatedResults()?.items!.findIndex(n => n.id == user.id);
        if (index){
          this.adminService.paginatedResults()?.items!.splice(index,1);
        }
      },
      error: _ => this.toastr.error('User could not be deleted')
    })
  }
}
