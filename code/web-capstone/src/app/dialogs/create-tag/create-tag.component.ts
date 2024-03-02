import { Component, Output , EventEmitter} from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-create-tag',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './create-tag.component.html',
  styleUrl: './create-tag.component.css'
})
export class CreateTagComponent {

  @Output() tagCreated = new EventEmitter<any>();
  
  constructor() { }

  onSubmit(data: any) {
    this.tagCreated.emit(data);
  }
  onClose() {
    const dialog = document.getElementById('dialog-tag-creation') as HTMLDialogElement;
    dialog?.close();
  }
}
