import { Component, Input, OnChanges, SimpleChange} from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgForOf } from '@angular/common';
import { TagComponent } from '../../components/tag/tag.component';
import { NgIf } from '@angular/common';
import { FormsModule,ReactiveFormsModule } from '@angular/forms';
import { CreateTagComponent } from '../create-tag/create-tag.component';
import { ChangeDetectorRef, NgZone} from '@angular/core';
@Component({
  selector: 'app-expand-note',
  standalone: true,
  imports: [NgForOf, TagComponent, CreateTagComponent , CommonModule, NgIf, FormsModule,ReactiveFormsModule],
  templateUrl: './expand-note.component.html',
  styleUrl: './expand-note.component.css'
})
export class ExpandNoteComponent {
@Input() note: any;
@Input() outerTags: any;
@Input() dialogId: any;

tags: any;
title: string = '';
content: string = '';

constructor(private cdr: ChangeDetectorRef , private zone: NgZone){
  this.tags = [];
}
ngOnChanges(changes: any): void {
  if (changes.outerTags && changes.outerTags.currentValue) {
    this.tags = changes.outerTags.currentValue;
  }

  if (this.note && this.note.content) {
    const parsedContent = JSON.parse(this.note.content);
    this.title = parsedContent.note_Title || '';
    this.content = parsedContent.note_Content || '';
  }
}
deleteTag(tag: any) {
  
  this.tags = this.tags.filter((t: any) => t.TagId !== tag);
  this.tags = this.tags.filter((t: any) => t.tagId !== tag);
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
closeDialog() {
  let dialog = document.getElementById(this.dialogId) as HTMLDialogElement;
  dialog.close();
}
addTag(tag: any) {
  let newTag = {
    tag: tag,
    note: this.note.note_Id
  }
  this.createTag(newTag);
  this.zone.run(() => {
    this.cdr.detectChanges();
  });

}
createTag(tag: any) {
  fetch("https://localhost:7062/Tags/CreateTag", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(tag),
  }).then(response => {
    if (response.ok) {
      console.log("Tag created");
      console.log(response);
      this.getTags();
    } else {
     if(response.status == 406) {
        alert("Tag already exists");
     }
    }
  });
}
getTags() {
  fetch("https://localhost:7062/Tags/GetByNoteId", {
    method: "POST",
    headers: {
      "Content-Type": "application/json"
    },
    body: JSON.stringify(this.note.note_Id),
  }).then(response => {
    if (response.ok) {
      return response.json();
    } else {
      console.error("Error getting tags");
      return [];
    }
  }).then(data => {
    console.log(data);
    this.tags = data;
    console.log(this.tags + ' tags');
  });

}
openDialog() {
  let dialog = document.getElementById(this.dialogId + '-tag-creation') as HTMLDialogElement;
  dialog.showModal();
}
}
