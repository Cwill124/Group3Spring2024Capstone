import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
@Component({
  selector: 'app-project-viewer',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './project-viewer.component.html',
  styleUrl: './project-viewer.component.css'
})
export class ProjectViewerComponent {
  sources = [
  {name: 'Project 1', description: 'This is project 1', status: 'Active'},
  {name: 'Project 2', description: 'This is project 2', status: 'Active'},
]
constructor() { 

}
onInit() {
  console.log('ProjectViewerComponent initialized');
}
}