import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-project-viewer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './project-viewer.component.html',
  styleUrl: './project-viewer.component.css'
})
export class ProjectViewerComponent implements OnInit {
  sources = [
  {name: 'Project 1', description: 'This is project 1', status: 'Active'},
  {name: 'Project 2', description: 'This is project 2', status: 'Active'},
]
projectTitle : string = "";
projectDescription : string = "";
id : any;
constructor(private route: ActivatedRoute, private router: Router) { 

this.id = this.route.snapshot.paramMap.get('id') ?? '';
}
ngOnInit() {
  console.log('ProjectViewerComponent initialized');
  this.getProject();
}
getProject() {
  fetch('https://localhost:7062/Project/GetById',{
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(parseInt(this.id)) 
  }).then(response => {
    if (response.ok) {
      return response.json();
    } else {
      throw new Error('Failed to get projects');
    }
  }).then(data => {
    console.log(data);
    this.projectTitle = data.title;
    this.projectDescription = data.description;
  }).catch(error => {
    console.log(error);
  });
}
deleteProject() {
  const userConfirmed = window.confirm('Are you sure you want to delete this source?');
  if (userConfirmed) {
    fetch('https://localhost:7062/Project/Delete',{
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(parseInt(this.id)) 
    }).then(response => {
      if (response.ok) {
        this.router.navigate(['/projects']);
      } else {
        throw new Error('Failed to delete project');
      }
    }).catch(error => {
      console.log(error);
    });
  }
}
}