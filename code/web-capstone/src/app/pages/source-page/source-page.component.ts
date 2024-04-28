import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { SourceCreationComponent } from '../../dialogs/source-creation/source-creation.component';

@Component({
  selector: 'app-source-page',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule,CommonModule,SourceCreationComponent],
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
    const dialog = document.getElementById("create-source") as HTMLDialogElement;
    dialog.showModal();    
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
    }
  }
  goToSource(source: any) {
    this.router.navigate(['/sourceViewer', source.source_Id, source.source_Type_Id]);
  }
}
