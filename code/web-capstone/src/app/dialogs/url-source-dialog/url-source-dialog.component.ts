import { Component, Inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import {MAT_DIALOG_DATA} from '@angular/material/dialog'

@Component({
  selector: 'app-url-source-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule,FormsModule],
  templateUrl: './url-source-dialog.component.html',
  styleUrl: './url-source-dialog.component.css'
})
export class UrlSourceDialogComponent {
  type: string = '';
  url: string = '';
  name : string = '';
  author : string = '';
  publisher : string = '';
  publishYear : string = ''; 
  constructor(private dialog: MatDialog,@Inject(MAT_DIALOG_DATA) public data: any) {
    this.type = data.sourceType;
  }
}
