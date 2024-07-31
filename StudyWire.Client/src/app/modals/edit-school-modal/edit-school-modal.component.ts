import { Component, inject, OnInit } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';
import { School } from '../../_models/school';

@Component({
  selector: 'app-edit-school-modal',
  standalone: true,
  imports: [],
  templateUrl: './edit-school-modal.component.html',
  styleUrl: './edit-school-modal.component.css'
})
export class EditSchoolModalComponent implements OnInit{
  ngOnInit(): void {
    console.log
  }
  schoolName : string = '';
  bsModalRef = inject(BsModalRef);
  title = '';
  availableSchools: School[] = [];
  userSchool: School|null = null;
  schoolUpdated = false;
  valueSelected : number = -1;

  updateChecked(checkedValue: number){
    if (!this.userSchool)
    {
      this.valueSelected = checkedValue;
    }
    else if (checkedValue !== this.userSchool.id){
      this.valueSelected = checkedValue;
    }    
  }


  onSelectSchool(){
    if (this.userSchool && this.userSchool.id === this.valueSelected){
      this.schoolUpdated = false;
      console.log(this.userSchool.id)
      console.log(this.valueSelected)
      console.log(this.schoolUpdated)
      this.bsModalRef.hide();
    }
    else if (!this.userSchool && this.valueSelected !== -1){
      this.schoolUpdated = true;
      console.log(this.schoolUpdated)
      this.bsModalRef.hide();
    }
    else if (this.userSchool && this.valueSelected === -1){
      this.schoolUpdated = false;
      console.log(this.schoolUpdated)
      this.bsModalRef.hide();
    }
    else if (this.userSchool && this.valueSelected !== -1){
      this.schoolUpdated = true;
      console.log(this.schoolUpdated)
      this.bsModalRef.hide();
    }
  }
}
