import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AddSourceProjectComponent } from '../../dialogs/add-source-project/add-source-project.component';
import { ExportSourceComponent } from '../../dialogs/export-source/export-source.component';

@Component({
  selector: 'app-project-viewer',
  standalone: true,
  imports: [CommonModule,AddSourceProjectComponent,ExportSourceComponent],
  templateUrl: './project-viewer.component.html',
  styleUrl: './project-viewer.component.css'
})
export class ProjectViewerComponent implements OnInit {

projectTitle : string = "";
projectDescription : string = "";
id : any;
sources: any[] = [];
constructor(private route: ActivatedRoute, private router: Router) { 

this.id = this.route.snapshot.paramMap.get('id') ?? '';
}
ngOnInit() {
  console.log('ProjectViewerComponent initialized');
  this.getProject();
  this.getSources();
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
getSources() {
  fetch('https://localhost:7062/Sources/GetAllInProject',{
    method: 'POST',
    headers: {
      'Content-Type': 'application/json'
    },
    body: JSON.stringify(parseInt(this.id)) 
  }).then(response => {
    if (response.ok) {
      return response.json();
    } else {
      throw new Error('Failed to get sources');
    }
  }).then(data => {
    console.log(data);
    this.sources = data;
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
async deleteSource(sourceId: number) {
  let sourceIds = [sourceId];
  const userConfirmed = window.confirm('Are you sure you want to delete this source?');
  if(userConfirmed) {
    await fetch('https://localhost:7062/Sources/DeleteSourceFromProject',{
      method: 'DELETE',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        projectId: parseInt(this.id),
        sources: sourceIds
      })
    }).then(response => {
      if (response.ok) {
        return response.json();
      } else {
        throw new Error('Failed to add sources to project');
      }
    }).catch(error => {
      console.log(error);
    });
    this.reloadCurrentRoute();
  }
}
parseMetadata(metaDataString: string) {
  return JSON.parse(metaDataString);
}
openAddSource() {
  const dialog = document.getElementById('project-add-source') as HTMLDialogElement;
  dialog.showModal();
}
private reloadCurrentRoute() {
  const currentUrl = this.router.url;
  this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    this.router.navigate([currentUrl]);
  });
}
goToSource(source: any) {
  this.router.navigate(['/sourceViewer', source.source_Id, source.source_Type_Id]);
}
openExportSource() {
  const dialog = document.getElementById('export-source') as HTMLDialogElement;
  dialog.showModal();
}
}