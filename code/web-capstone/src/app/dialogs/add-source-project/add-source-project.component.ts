import { CommonModule } from '@angular/common';
import { Input } from '@angular/core';
import { Component } from '@angular/core';
import { OnInit } from '@angular/core';

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
  isLoading = false;
  error: string | null = null;
  constructor() { 

  }

  ngOnInit() {
    this.getSources();
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

}
