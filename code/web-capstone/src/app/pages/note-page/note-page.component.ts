import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NoteComponent } from '../../components/note/note.component';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';
import { FilterTagDialogComponent } from '../../dialogs/filter-tag-dialog/filter-tag-dialog.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TagComponent } from '../../components/tag/tag.component';
import { ExpandNoteComponent } from '../../dialogs/expand-note/expand-note.component';
@Component({
  selector: 'app-note-page',
  standalone: true,
  imports: [ExpandNoteComponent,CommonModule, NoteComponent, FormsModule, FilterTagDialogComponent,FormsModule, ReactiveFormsModule, TagComponent],
  templateUrl: './note-page.component.html',
  styleUrls: ['./note-page.component.css']
})

export class NotePageComponent implements OnInit {
  private isDialogOpen = false;
  isLoading = false;
  error: string | null = null;
  notes: any[] = [];
  filterTags: any[] = [];
  searchInput: string = '';


  constructor(private router: Router) {

  }

  ngOnInit() {
    this.fetchNotes();
  }

  async fetchNotes() {
    this.isLoading = true;
    let username = JSON.parse(localStorage["user"])?.username;
    const response = await fetch('https://localhost:7062/Notes/GetByUsername', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(username),
      }).then(response => {
        if (!response.ok) {
          throw new Error('Network response was not ok');
        }
        return response.json();
      }).then(data => {
        this.notes = data;
      }).finally(() => {
        this.isLoading = false;
        console.log(this.notes);
      });
}

  searchNotes() {
    let exactMatchNotes: any[] = [];
    let partialMatchNotes: any[] = [];
    const searchTerm = this.searchInput.toLowerCase();

    console.log("input: " + this.searchInput);
    this.notes.forEach(note => {
      let parsedNote = this.parseNoteContent(note);
      let title = parsedNote.note_Title.toLowerCase();
      console.log("current note title: " + title);
      if (title === searchTerm) {
        exactMatchNotes.push(note);
      } else if (title.includes(searchTerm)) {
        partialMatchNotes.push(note);
      }
    });
    this.notes = exactMatchNotes.concat(partialMatchNotes);
  }

  filterNotes() {
    
  }

  resetNotes() {
    this.fetchNotes();
    this.filterTags = [] as any;
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

  openDialog() {
    console.log("clicked on filter button");
    const dialogElement = document.getElementById('filter-tag-dialog') as HTMLDialogElement | null;
    if (dialogElement) {
        dialogElement.showModal();
    } else {
        console.error("Dialog element not found");
    }
  }

  closeDialog() {
    const dialogElement = document.getElementById('filter-tag-dialog') as HTMLDialogElement | null;
    dialogElement?.showModal()
  }

  onSubmit(data : any){
    console.log(data);
    this.formatFilterTags(data);
    this.sortNotesByTags();
  }

  private formatFilterTags(data : any){
    const formattedTags: any[] = [];
    for (const tag of data) {
      const formattedTag = tag.split(":")[1].trim().slice(1, -1);
      formattedTags.push(formattedTag);
    }
    this.filterTags = formattedTags;
  }

  private sortNotesByTags() {
    let filteredNotes: any[] = [];
    for (const currentNote of this.notes) {
        const parsedTags: any[] = JSON.parse(currentNote.tags);
        const hasMatchingTag = parsedTags.some(tag => this.filterTags.includes(tag.Tag));
        if (hasMatchingTag) {
            filteredNotes.push(currentNote);
        }
    }
    this.notes = filteredNotes;
}

  private reloadCurrentRoute() {
    const currentUrl = this.router.url;
    this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
      this.router.navigate([currentUrl]);
    });
  }

  removeTag(tag: string): void {
    const index = this.filterTags.indexOf(tag);
    if (index !== -1) {
        this.filterTags.splice(index, 1);
    }
    if (this.filterTags.length === 0) {
      this.fetchNotes();
    } else {
      this.sortNotesByTags();
    }
}
refreshNotes(data: any) {
    this.fetchNotes();
}
}
