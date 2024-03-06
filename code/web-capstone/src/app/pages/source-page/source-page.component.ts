import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SourceDialogComponent } from '../../dialogs/source-dialog/source-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-source-page',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule,CommonModule],
  templateUrl: './source-page.component.html',
  styleUrls: ['./source-page.component.css']
})
export class SourcePageComponent implements OnInit {
  private isDialogOpen = false;
  sources: any[] = [];
  isLoading = false;
  error: string | null = null;

  constructor(private dialog: MatDialog, private router: Router) {}

  ngOnInit() {
    this.fetchSources();
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
  async fetchSources() {
    this.isLoading = true;
    let username = JSON.parse(localStorage["user"])?.username;

    try {
      const response = await fetch('https://localhost:7062/Sources/GetByUsername', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(username),
      });

      if (!response.ok) {
        throw new Error('Network response was not ok');
      }

      const data = await response.json();
      this.sources = data;
    } catch (error) {
      console.error('There has been a problem with your fetch operation:', error);
      this.error = 'Failed to fetch sources. Please try again later.';
    } finally {
      this.isLoading = false;
      console.log(this.sources);
    }
  }
  goToSource(source: any) {
	  console.log(source);
    // Navigate to the source page
    if (source.source_Type_Id === 1) {
      this.router.navigate(['/pdfsource', source.source_Id]);
    } else {
      this.router.navigate(['/videosource', source.source_Id]);
    }
    //this.router.navigate(['/pdfsource', source.sourceId]);
  
  }
}
