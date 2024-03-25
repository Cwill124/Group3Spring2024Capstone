import { Component, Output , EventEmitter, Input} from '@angular/core';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-create-tag',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule],
  templateUrl: './create-tag.component.html',
  styleUrl: './create-tag.component.css'
})
export class CreateTagComponent {

  @Input('dialogId') dialogId: any;
  @Output() tagCreated = new EventEmitter<any>();
  
 
  constructor() { }

  onSubmit(data: any) {
    this.tagCreated.emit(data.tag);
    this.onClose();
  }
  onClose() {
    const dialog = document.getElementById(this.dialogId + '-tag-creation') as HTMLDialogElement;
    dialog?.close();
  }
}
