import { Component, Output , EventEmitter} from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { CreateTagComponent } from '../create-tag/create-tag.component';
import { ChangeDetectorRef, NgZone} from '@angular/core';
import { NgFor } from '@angular/common';
@Component({
  selector: 'app-create-note',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,CreateTagComponent, NgFor],
  templateUrl: './create-note.component.html',
  styleUrl: './create-note.component.css'
})
export class CreateNoteComponent {

  @Output() close = new EventEmitter<any>();
  @Output() noteCreated = new EventEmitter<any>();

  tags: any[];
  
  constructor(private cdr: ChangeDetectorRef , private zone: NgZone) {
    this.tags = [];
  }

  closeDialog() {
    const dialog = document.getElementById('dialog-note-creation') as HTMLDialogElement;
    dialog?.close();
  }
  onSubmit(data: any) {
   let note = {
      title: data.title,
      content: data.note,
      tags: this.tags
   }
  
    this.noteCreated.emit(note);
  }
  openTagCreationDialog() {
    const dialog = document.getElementById('dialog-tag-creation') as HTMLDialogElement;
    dialog?.showModal();
  }
  closeTagCreationDialog() {
    const dialog = document.getElementById('dialog-tag-creation') as HTMLDialogElement;
    dialog?.close();
  }
  onTagCreated(data: any) {
    this.tags.push(data);
    this.zone.run(() => {
      this.cdr.detectChanges();
    });
    this.closeTagCreationDialog();
  }
}
