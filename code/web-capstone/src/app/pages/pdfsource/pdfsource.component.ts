import { Component,Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OnInit } from '@angular/core';
import { SourceAsideComponent } from '../../components/source-aside/source-aside.component';
import {DomSanitizer} from "@angular/platform-browser";
import { Router } from '@angular/router';
import { NoteCreationComponent } from '../../dialogs/note-creation/note-creation.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
@Component({
  selector: 'app-pdfsource',
  standalone: true,
  imports: [SourceAsideComponent,CommonModule,NoteCreationComponent,FormsModule,ReactiveFormsModule],
  templateUrl: './pdfsource.component.html',
  styleUrl: './pdfsource.component.css'
})
export class PDFSourceComponent implements OnInit {
  name: string = '';
  id: string = '';
  author: string = '';
  description: string = '';
  publisher: string = '';
  year: string = '';
  url: any = '';
  createdBy: string = '';
  isLoading = false;
  noteTitle: string = '';
  noteContent: string = '';

  constructor(private route: ActivatedRoute,private dataSanitizer: DomSanitizer,private router: Router) {
    console.log(this.route.snapshot.params);
    
  }
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.fetchSource();
  }  
  async fetchSource() {
    this.isLoading = true;
   await fetch('https://localhost:7062/Sources/GetById', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.id),
    }).then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json();
    }).then(data => {
      // Set the source properties
      console.log(data);
      let contentJson = JSON.parse(data.content);
      let metaDataJson = JSON.parse(data.metaData);
      this.name = data.name;
      this.description = data.description;
      this.publisher = metaDataJson.publisher;
      this.year = metaDataJson.year;
      this.url = this.dataSanitizer.bypassSecurityTrustResourceUrl(contentJson.url);
      this.author = metaDataJson.author;
      this.createdBy = data.createdBy;
      

    }).finally(() => {
      this.isLoading = false;
    });
  }
  openDialog() {
 
    const dialogElement = document.getElementById('dialog-note-creation')  as HTMLDialogElement | null;
    dialogElement?.showModal()
  }
  deleteSource() {
    const userConfirmed = window.confirm('Are you sure you want to delete this source?');
    if (userConfirmed) {
      fetch('https://localhost:7062/Sources/DeleteById', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(this.id),
      }).then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      }).then(data => {
        // Redirect to the sources page
        this.goToSource();
      }).catch(error => {
        console.error('There has been a problem with your fetch operation:', error);
      });
    }
  }
  goToSource() {
  
    this.router.navigate(['/sources']);
    setTimeout(() => {
      this.reloadCurrentRoute();
    }, 100);
}
private reloadCurrentRoute() {
  const currentUrl = this.router.url;
  this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    this.router.navigate([currentUrl]);
  });
}
closeDialog() {
  const dialogElement = document.getElementById('dialog-note-creation')  as HTMLDialogElement | null;
  dialogElement?.close();
}
onSubmit(data : any) {
  console.log(data);
  let content = {
    noteTitle: data.title,
    noteContent: data.note
  }
  let note = {
    sourceId: this.id,
    content : JSON.stringify(content),
    username: JSON.parse(localStorage["user"])?.username
  }
  this.postNote(note);
}
postNote(newNote : any) {
  fetch('https://localhost:7062/Notes/Create', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: JSON.stringify(newNote),
  }).then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  }).then(data => {
    // Redirect to the sources page
    this.closeDialog();
  }).catch(error => {
    console.error('There has been a problem with your fetch operation:', error);
  });
}
}
