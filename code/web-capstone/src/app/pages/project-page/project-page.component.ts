import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ProjectCreationComponent } from '../../dialogs/project-creation/project-creation.component';
import { Router } from '@angular/router';
@Component({
  selector: 'app-project-page',
  standalone: true,
  imports: [ProjectCreationComponent, CommonModule],
  templateUrl: './project-page.component.html',
  styleUrl: './project-page.component.css'
})
export class ProjectPageComponent {
  projects: any[] = [];
  isLoading = false;
  error: string | null = null;
constructor(private router: Router) {

}
 
ngOnInit() {
  this.getAllProjects();
}

openDialog() {
  const dialog = document.getElementById("create-project-dialog") as HTMLDialogElement;
  
  dialog.showModal();

  dialog.addEventListener('close', async () => {
    await this.getAllProjects();
  });
}
getAllProjects() {
  this.isLoading = true;
  fetch('https://localhost:7062/Project/GetAllByUser',{
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(JSON.parse(localStorage["user"])?.username)
  }).then(response => {
    if (response.ok) {
      return response.json();
    } else {
      throw new Error('Failed to get projects');
    }
  }).then(data => {
    this.projects = data;
    
  }).catch(error => {
    console.error(error);
    this.error = 'Failed to fetch projects. Please try again later.';
  }).finally(() => {
    this.isLoading = false;
  });
}
goToProject(project: any) {
  this.router.navigate(['/projectViewer', project.projectId]);
}
}
