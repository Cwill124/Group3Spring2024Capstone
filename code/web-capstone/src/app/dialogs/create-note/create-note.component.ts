import { Component, Output , EventEmitter} from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-create-note',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule,],
  templateUrl: './create-note.component.html',
  styleUrl: './create-note.component.css'
})
export class CreateNoteComponent {

  @Output() close = new EventEmitter<any>();
  @Output() noteCreated = new EventEmitter<any>();
  closeDialog() {
    const dialog = document.getElementById('dialog-note-creation') as HTMLDialogElement;
    dialog?.close();
  }
  onSubmit(data: any) {
   console.log(data); 
   this.noteCreated.emit(data);
  }
}
