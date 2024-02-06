import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import {MAT_DIALOG_DATA} from '@angular/material/dialog'

@Component({
  selector: 'app-url-source-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule,FormsModule],
  templateUrl: './url-source-dialog.component.html',
  styleUrl: './url-source-dialog.component.css'
})
export class UrlSourceDialogComponent {
  type: string = '';
  url: string = '';
  name : string = '';
  author : string = '';
  publisher : string = '';
  publishYear : string = ''; 
  constructor(private dialog: MatDialog,@Inject(MAT_DIALOG_DATA) public data: any) {
    this.type = data.sourceType;
  }
  handleSubmit() {
    let metaData = {
        author: this.author,
        publisher: this.publisher,
        publishYear: this.publishYear
    }
    let content = {
      url: this.url
    }

    let source = {
      sourceTypeId: this.type === 'PDF' ? 1 : 2,
      name: this.name,
      description : '',
      content : JSON.stringify(content),
      metaData: JSON.stringify(metaData),
      tags : null,
      createdBy: JSON.parse(localStorage["user"])?.username
    }
    console.log(source);
    this.submitRequest(source);
    this.dialog.closeAll();
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
      .then(data => {
        // Handle the successful response data here
        console.log(data);
      })
      .catch(error => {
        // Handle errors here
        console.error(error.message);
      });
  }
  
}

