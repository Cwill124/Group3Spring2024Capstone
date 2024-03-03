import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { NgIf } from '@angular/common';
import { TagComponent } from '../tag/tag.component';

@Component({
  selector: 'app-note',
  standalone: true,
  imports: [NgIf, TagComponent],
  templateUrl: './note.component.html',
  styleUrl: './note.component.css'
})
export class NoteComponent {

@Input('currentNote') currentNote: any;
@Input('tags') tags: any[] = [];

@Output() deleteNote: EventEmitter<any> = new EventEmitter<any>();
@Output() deleteTag: EventEmitter<any> = new EventEmitter<any>();
constructor() { 
  console.log(this.currentNote);
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

onDeleteTag(data: any) {
  this.deleteTag.emit(data);
}
}