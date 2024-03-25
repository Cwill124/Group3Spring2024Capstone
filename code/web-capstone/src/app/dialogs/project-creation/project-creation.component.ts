import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-creation',
  standalone: true,
  imports: [FormsModule,ReactiveFormsModule, CommonModule,],
  templateUrl: './project-creation.component.html',
  styleUrl: './project-creation.component.css'
})
export class ProjectCreationComponent {


project : any = {};

  constructor( private router: Router) {}

  
  closeDialog() {
    const dialog = document.getElementById("create-project-dialog") as HTMLDialogElement;
    dialog.close()
  }
  onSubmit(data: any) {
    console.log(data);
    if(data.title === "") {
      this.showInvaildAlert();
      return;
    }
    let newProject = {
      title: data.title,
      description: data.description,
      owner: JSON.parse(localStorage["user"])?.username,
    }
    console.log(newProject);
    this.createRequest(newProject);
    this.closeDialog();
    this.reloadCurrentRoute();
  }
  showInvaildAlert() {
    window.alert("Please provide a title.");
  }
  async createRequest(data: any) {
    await fetch('https://localhost:7062/Project/Create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(data)
    })
      .then(response => {
        if (response.ok) {

        } else {
          throw new Error('Failed to create project');
        }
      })
      .catch(error => {
        console.error(error);
      });
  }
  private reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
