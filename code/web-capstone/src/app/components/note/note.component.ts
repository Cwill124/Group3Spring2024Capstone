import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { NgIf } from '@angular/common';
import { TagComponent } from '../tag/tag.component';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { NgForOf } from '@angular/common';
@Component({
  selector: 'app-note',
  standalone: true,
  imports: [NgIf, TagComponent, FormsModule,ReactiveFormsModule, NgForOf],
  templateUrl: './note.component.html',
  styleUrl: './note.component.css'
})
export class NoteComponent {

@Input('currentNote') currentNote: any;
@Output() deleteNote: EventEmitter<any> = new EventEmitter<any>();
tags: any;
constructor() { 
 
}
ngOnInit() {
  this.tags = JSON.parse(this.currentNote.tags);
  console.log('Tags:', this.tags);
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
}