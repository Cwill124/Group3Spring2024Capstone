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

ngOnInit() {
    if (this.tag.tag !== undefined) {
      let reFormatTag = {
        TagId: this.tag.tagId,
        Tag: this.tag.tag
      }
      console.log(reFormatTag);
      this.tag = reFormatTag;
    }
  }
removeTag() {
  this.deleteTag.emit(this.tag.TagId);
}
}
