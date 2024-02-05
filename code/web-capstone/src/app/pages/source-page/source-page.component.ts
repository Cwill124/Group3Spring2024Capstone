import { Component } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SourceDialogComponent } from '../../dialogs/source-dialog/source-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';

@Component({
  selector: 'app-source-page',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule],
  templateUrl: './source-page.component.html',
  styleUrl: './source-page.component.css'
})
export class SourcePageComponent {
  private isDialogOpen = false;

  constructor(private dialog: MatDialog) {
    
  }

  openDialog() {
    // Check if the dialog is already open
    if (!this.isDialogOpen) {
      this.isDialogOpen = true;

      // Get the button element
      const button = document.getElementById('add-source');

      // Calculate the position of the button
      const buttonRect = button?.getBoundingClientRect();
      let left = (buttonRect?.left ?? 0) - 230;
      // Set the dialog position below the button
      const dialogRef = this.dialog.open(SourceDialogComponent, {
        width: '300px',
        height: 'auto',
        id: 'add-source-dialog',
        position: {
          left: `${left}px`
        },
        // Add any other dialog configuration options as needed
      });

      // Subscribe to the afterClosed event to reset the flag when the dialog is closed
      dialogRef.afterClosed().subscribe(() => {
        this.isDialogOpen = false;
      });
    }
  }
}
