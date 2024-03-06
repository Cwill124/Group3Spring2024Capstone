import { Component, Output , EventEmitter, Inject} from "@angular/core";
import { FormControl } from "@angular/forms";
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { Router } from '@angular/router';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { CommonModule, NgFor } from "@angular/common";
import { ReactiveFormsModule } from "@angular/forms";
import { TagComponent } from "../../components/tag/tag.component";

@Component({
    selector: 'app-tag-filter',
    standalone: true,
    imports: [FormsModule, CommonModule, NgFor, ReactiveFormsModule, TagComponent],
    templateUrl: './filter-tag-dialog.component.html',
    styleUrl: './filter-tag-dialog.component.css'
})
export class FilterTagDialogComponent {

    @Output() close = new EventEmitter<any>();
    @Output() tagsFiltered = new EventEmitter<any>();

    tagsFormControl = new FormControl();
    tags: any[] = [];
    selectedTags: any[] = [];
    tagSelectionChange: EventEmitter<any[]> = new EventEmitter<any[]>();


    ngOnInit() {
        this.getUserTags();
    }

    closeDialog() {
        this.onSubmit();
        const dialog = document.getElementById('filter-tag-dialog') as HTMLDialogElement;
        dialog?.close();
    }

    onSubmit(){
        this.tagsFiltered.emit(this.selectedTags);
    }

    async getUserTags(){
        let username = JSON.parse(localStorage["user"])?.username;
        console.log("current user: " + username);
        await fetch('https://localhost:7062/Tags/GetTagsBelongingToUsername', {
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
        this.tags = data;
      }).finally(() => {
        console.log(this.tags);
      });
    }

      onTagSelectionChange(event: any) {
        const selectedOptions = event.target.selectedOptions;
        this.selectedTags = [];
        for (let i = 0; i < selectedOptions.length; i++) {
            this.selectedTags.push(selectedOptions[i].value);
        }
    }
}