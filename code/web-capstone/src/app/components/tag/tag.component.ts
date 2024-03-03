import { Component, Input, Output, EventEmitter, OnChanges, SimpleChanges } from '@angular/core';
import { NgIf } from '@angular/common';
@Component({
  selector: 'app-tag',
  standalone: true,
  imports: [NgIf],
  templateUrl: './tag.component.html',
  styleUrl: './tag.component.css'
})
export class TagComponent {

@Input('currentTag') currentTag: any;
@Output() deleteTag: EventEmitter<any> = new EventEmitter<any>();
constructor() { 
  console.log(this.currentTag);
}

ngOnChanges(changes: SimpleChanges): void {
  if ('note' in changes) {
    console.log('Note changed:', this.currentTag);
  }
}
onDeleteTag(data: any) {
  this.deleteTag.emit(data);
}
}