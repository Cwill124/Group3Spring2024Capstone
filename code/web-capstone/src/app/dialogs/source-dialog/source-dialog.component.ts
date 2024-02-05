import { Component, inject } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-source-dialog',
  standalone: true,
  imports: [MatDialogModule, MatButtonModule],
  templateUrl: './source-dialog.component.html',
  styleUrl: './source-dialog.component.css'
})
export class SourceDialogComponent {

 
}
