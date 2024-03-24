import { Component } from '@angular/core';
import { ProjectCreationComponent } from '../../dialogs/project-creation/project-creation.component';

@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [ProjectCreationComponent],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.css'
})
export class ProjectPageComponent {
constructor() { 

}

openDialog() {
  console.log("Opening create project dialog");
  const dialog = document.getElementById("create-project-dialog") as HTMLDialogElement;
  dialog.showModal()
}
}
