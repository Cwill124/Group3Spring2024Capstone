import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-source-creation',
  standalone: true,
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './source-creation.component.html',
  styleUrl: './source-creation.component.css'
})
export class SourceCreationComponent {

  selectedType: number = 1; 

  constructor( private router: Router) {

  }

  closeDialog() {
    const dialog = document.getElementById("create-source") as HTMLDialogElement;
    dialog.close()
  
  }
  onSubmit(data: any) {
    let metaData = {
      author: data.author,
      publisher: data.publisher,
      publishYear: data.publishYear
  }
  let content = {
    url: data.url,
    file: ''
  }
  let tags = {
    tags: []
  };
  let source = {
    source_Type_Id: data.type,
    name: data.name,
    description : '',
    content : JSON.stringify(content),
    meta_Data: JSON.stringify(metaData),
    tags : JSON.stringify(tags),
    created_By: JSON.parse(localStorage["user"])?.username
  }
  if(!this.checkForContentErrors(source,content)) {
    this.submitRequest(source);
    this.closeDialog();
  }
  console.log(source);
  }
  submitRequest(source: any) {
    fetch('https://localhost:7062/Sources/Create', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(source), // Convert the source object to a JSON string
    })
      .then(response => {
        console.log(response);
  
        if (!response.ok) {
          throw new Error(`Failed to create source. Status: ${response.status}`);
        }
  
        // Ensure that the response body is read as JSON
        return response.json();
      })
      .finally(() => {
        this.reloadCurrentRoute();
      });
  }
  private checkForContentErrors(source: any,content : any) : boolean {
    let message = '';
    if(source.name === '') {
      message += 'Name is required. \n';
    }
   if(content.url === '') {
      message += 'URL is required. \n';
    }
    if(message !== '') {
      alert(message);
      return true;
    }
    return false;
  }
  private reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }
}
