import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { NgIf } from '@angular/common';
import { TagComponent } from '../tag/tag.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgForOf } from '@angular/common';
import { ExpandNoteComponent } from '../../dialogs/expand-note/expand-note.component';
@Component({
  selector: 'app-note',
  standalone: true,
  imports: [NgIf, TagComponent, FormsModule,ReactiveFormsModule, NgForOf, ExpandNoteComponent],
  templateUrl: './note.component.html',
  styleUrl: './note.component.css'
})
export class NoteComponent {

@Input('currentNote') currentNote: any;
@Output() deleteNote: EventEmitter<any> = new EventEmitter<any>();
@Output() refreshNotes: EventEmitter<any> = new EventEmitter<any>();
tags: any;
constructor() { 
 
}
ngOnInit() {
  this.tags = JSON.parse(this.currentNote.tags);
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
ngOnChanges(changes: SimpleChanges): void {
  if ('note' in changes) {
    console.log('Note changed:', this.currentNote);
  }
}
onDeleteNote(data: any) {
  this.deleteNote.emit(data);
}
deleteTag(tag: any) {
  this.tags = this.tags.filter((t: any) => t.TagId !== tag);
  fetch("https://localhost:7062/Tags/DeleteById", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(tag),
  }).then(response => {
    if (response.ok) {
      console.log("Tag deleted");
    } else {
      console.error("Error deleting tag");
    }
  });
}
refreshNotesDisplayTags() {
  this.refreshNotes.emit();
}
getTags() {
  fetch("https://localhost:7062/Tags/GetByNoteId", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(this.currentNote.note_Id),
  }).then(response => {
    if (response.ok) {
      return response.json();
    } else {
      console.error("Error getting tags");
      return [];
    }
  }).then(data => {
    console.log(data);
    this.currentNote.tags = data;
    console.log(this.tags + ' tags');
  });

}
openExpandDialog() {
  this.getTags();

  let dialog = document.getElementById(this.currentNote.note_Id) as HTMLDialogElement;
  dialog.addEventListener('close', () => {
    this.refreshNotesDisplayTags();
  });

  dialog.showModal();
}

}