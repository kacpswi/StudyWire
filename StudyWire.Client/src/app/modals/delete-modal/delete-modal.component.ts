import { Component, inject } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-delete-modal',
  standalone: true,
  imports: [],
  templateUrl: './delete-modal.component.html',
  styleUrl: './delete-modal.component.css'
})
export class DeleteModalComponent {
  bsModalRef = inject(BsModalRef);
  deleteNews = false;

  onDeleteClick(){
    this.deleteNews = true;
    this.bsModalRef.hide();
  }
}
