import { Component } from '@angular/core';

@Component({
  selector: 'app-project-creation',
  standalone: true,
  imports: [],
  templateUrl: './project-creation.component.html',
  styleUrl: './project-creation.component.css'
})
export class ProjectCreationComponent {

  constructor() {}


  closeDialog() {
    const dialog = document.getElementById("create-project-dialog") as HTMLDialogElement;
    dialog.close()
  }
}
