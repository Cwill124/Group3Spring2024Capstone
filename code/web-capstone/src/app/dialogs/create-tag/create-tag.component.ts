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
  
 
  constructor() { 
    console.log(this.dialogId)
  }

  onSubmit(data: any) {
    if(data.tag == ""){
      alert("Please enter a tag name.");
      return;
    }
    this.tagCreated.emit(data.tag);
  }
  onClose() {
    const dialog = document.getElementById(this.dialogId + '-tag-creation') as HTMLDialogElement;
    dialog?.close();
  }
}
