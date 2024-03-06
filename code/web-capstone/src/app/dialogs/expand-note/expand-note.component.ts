import { Component, Input} from '@angular/core';
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
@Input() dialogId: any;

tags: any;
title: string = '';
content: string = '';

constructor(private cdr: ChangeDetectorRef , private zone: NgZone){
  this.tags = [];
}
ngOnInit() {
  this.tags = JSON.parse(this.note.tags);
  this.title = JSON.parse(this.note.content).note_Title;
  this.content = JSON.parse(this.note.content).note_Content;
  console.log(this.tags);
}
deleteTag(tag: any) {

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
  this.getTags();
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
    } else {
      console.error("Error creating tag");
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
    this.tags = data;
    console.log(this.tags + ' tags');
  });

}
openDialog() {
  let dialog = document.getElementById(this.dialogId + '-tag-creation') as HTMLDialogElement;
  dialog.showModal();
}
}
