import { Component, Output , EventEmitter, Input} from '@angular/core';
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
@Input('dialogId') dialogId: any;
  tags: any[];
  
  constructor(private cdr: ChangeDetectorRef , private zone: NgZone) {
    this.tags = [];
  }

  closeDialog() {
    const dialog = document.getElementById('dialog-note-creation') as HTMLDialogElement;
    this.tags = [];
    dialog?.close();
  }
  onSubmit(data: any) {
   let note = {
      title: data.title,
      content: data.note,
      tags: this.tags
   }
  
   console.log(note);
    this.noteCreated.emit(note);
  }
  openTagCreationDialog() {
    const dialog = document.getElementById(this.dialogId + '-tag-creation') as HTMLDialogElement;
    dialog?.showModal();
  }
  closeTagCreationDialog() {
    const dialog = document.getElementById(this.dialogId +'-tag-creation') as HTMLDialogElement;
    dialog?.close();
  }
  onTagCreated(data: any) {
    if(this.tags.includes(data)) {
      alert('Tag already exists');
    } else {
    this.tags.push(data);
    this.zone.run(() => {
      this.cdr.detectChanges();
    });
    this.closeTagCreationDialog();
  }
  }
}
