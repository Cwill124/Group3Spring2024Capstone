import { Component,Inject } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OnInit } from '@angular/core';
import {DomSanitizer} from "@angular/platform-browser";
import { Router } from '@angular/router';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NoteComponent } from '../../components/note/note.component';
import { CreateNoteComponent } from '../../dialogs/create-note/create-note.component';
import { ExpandNoteComponent } from '../../dialogs/expand-note/expand-note.component';
@Component({
  selector: 'app-source-viewer',
  standalone: true,
  imports: [CreateNoteComponent,NoteComponent,CommonModule,FormsModule,ReactiveFormsModule , ExpandNoteComponent],
  templateUrl: './source-viewer.component.html',
  styleUrl: './source-viewer.component.css'
})
export class SourceViewerComponent implements OnInit {
  name: string = '';
  id: string = '';
  sourceType: string = '';
  author: string = '';
  description: string = '';
  publisher: string = '';
  year: string = '';
  url: any = '';
  createdBy: string = '';
  isLoading = false;
  notesisLoading = false;
  noteTitle: string = '';
  noteContent: string = '';

  notes : any[] = [];

  constructor(private route: ActivatedRoute,private dataSanitizer: DomSanitizer,private router: Router) {  }
  async ngOnInit(): Promise<void> {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.sourceType = this.route.snapshot.paramMap.get('sourceType') ?? '';
    this.fetchSource();
    await this.fetchNotes();
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
      let contentJson = JSON.parse(data.content);
      let metaDataJson = JSON.parse(data.meta_Data);
      this.name = data.name;
      this.description = data.description;
      this.publisher = metaDataJson.publisher;
      this.year = metaDataJson.year;
      let tempurl = contentJson.url;
      if (this.sourceType === '2') {
        tempurl = this.formatLink(tempurl);
      }
      this.url = this.dataSanitizer.bypassSecurityTrustResourceUrl(tempurl);
      this.author = metaDataJson.author;
      this.createdBy = data.createdBy;
      
    }).finally(() => {
      this.isLoading = false;
    });
  }
  async fetchNotes() {
    this.notesisLoading = true;
   await fetch('https://localhost:7062/Notes/GetBySourceId', {
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
      this.notes = data;
    }).finally(() => {
      this.notesisLoading = false;
      console.log(this.notes);
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
      }).finally(() => {
        this.goToSource();
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
    note_Title: data.title,
    note_Content: data.content
  }
  if(!this.checkForNoteErrors(content)) {
    let note = {
      source_Id: this.id,
      content : JSON.stringify(content),
      username: JSON.parse(localStorage["user"])?.username,
      tags : JSON.stringify(data.tags)
    }
    this.postNote(note);
  }
}
private checkForNoteErrors(note: any) : boolean {
  let message = '';
  if(note.note_Title === '') {
    message += 'Title is required. \n';
  }
  if(note.note_Content === '') {
    message += 'Content is required. \n';
  }
  if(message !== '') {
    alert(message);
    return true;
  }
  return false;

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
  }).finally(() => {
    this.reloadCurrentRoute();
  });
}
deleteNote(noteId : number) {
  console.log(noteId);
  fetch('https://localhost:7062/Notes/Delete', {
    method: 'POST',
    headers: {
      'Content-Type': 'application/json',
    },
    body: noteId.toString(),
  }).then(response => {
    if (!response.ok) {
      throw new Error('Network response was not ok');
    }
    return response.json();
  }).finally(() => {
    this.reloadCurrentRoute();
  });
}
parseNoteContent(note: any): any {
  if (note.content) {
    try {
      return JSON.parse(note.content);
    } catch (error) {
      console.error('Error parsing JSON:', error);
    }
  }
  return null;
}
refreshNotes(data: any) {
  this.fetchNotes();

}
formatLink(link: string) {

  let formattedLink = link;
  if (formattedLink.includes('youtube')) {
      let videoId = formattedLink.substring(formattedLink.indexOf("=") + 1);
      formattedLink = "https://www.youtube.com/embed/" + videoId;
  }
  return formattedLink;
}

}
