import { CommonModule } from '@angular/common';
import { Input } from '@angular/core';
import { Component } from '@angular/core';
import { OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-source-project',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './add-source-project.component.html',
  styleUrl: './add-source-project.component.css'
})

export class AddSourceProjectComponent implements OnInit {

  @Input('id') id: any;
  
  sources: any[] = [];
  selectedSources: number[] = [];
  isLoading = false;
  error: string | null = null;
  constructor(private router: Router) { 

  }

  ngOnInit() {
    this.getSources();
  }
  getSources() {
    fetch('https://localhost:7062/Sources/GetNotInProject',{
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
      this.sources = data;
    }).catch(error => {
      console.log(error);
    });
  }
  closeDialog() {
    const dialog = document.getElementById('project-add-source') as HTMLDialogElement;
    dialog.close();
  }
  async onSubmit() {
    await fetch('https://localhost:7062/Sources/AddSourceToProject',{
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify({
        projectId: parseInt(this.id),
        sources: this.selectedSources
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
    this.closeDialog();
    this.reloadCurrentRoute();
  }
  toggleSelection(event: any, sourceId: number) {
    const isChecked = event.target.checked;

    if (isChecked && !this.selectedSources.includes(sourceId)) {
      this.selectedSources.push(sourceId);
    } else if (!isChecked && this.selectedSources.includes(sourceId)) {
      this.selectedSources.splice(this.selectedSources.indexOf(sourceId), 1);
    }
  }

  isSelected(sourceId: number): boolean {
    return this.selectedSources.includes(sourceId);
  }
  private reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
