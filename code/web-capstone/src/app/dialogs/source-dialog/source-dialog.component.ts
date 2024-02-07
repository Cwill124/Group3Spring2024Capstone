import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { UrlSourceDialogComponent } from '../url-source-dialog/url-source-dialog.component';
@Component({
  selector: 'app-source-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule,FormsModule],
  templateUrl: './source-dialog.component.html',
  styleUrl: './source-dialog.component.css'
})
export class SourceDialogComponent {

  sourceType: string = 'PDF';
  sourceFormat: string = 'Link';

  constructor(private dialog: MatDialog) {
    
  }
  handleContinue() {
    this.dialog.getDialogById('add-source-dialog')?.close();

    this.dialog.open(UrlSourceDialogComponent, {
      width: '500px',
      height: 'auto',
      id: 'url-source-dialog',
      data:{
        sourceType: this.sourceType,
      },
      position: {
        left: '300px',
        bottom: '200px'
      },
    });

  }
}
