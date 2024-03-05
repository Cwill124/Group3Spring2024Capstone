import { Component, Input, Output, input, EventEmitter } from '@angular/core';

@Component({
  selector: 'app-tag',
  standalone: true,
  imports: [],
  templateUrl: './tag.component.html',
  styleUrl: './tag.component.css'
})
export class TagComponent {
@Input('tag') tag: any;
@Output() deleteTag: EventEmitter<any> = new EventEmitter<any>();

removeTag() {
  this.deleteTag.emit(this.tag.TagId);
}
}
