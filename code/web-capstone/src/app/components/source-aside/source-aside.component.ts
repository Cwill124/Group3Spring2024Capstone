import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { SourceDialogComponent } from '../../dialogs/source-dialog/source-dialog.component';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-source-aside',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule,CommonModule],
  templateUrl: './source-aside.component.html',
  styleUrl: './source-aside.component.css'
})
export class SourceAsideComponent implements OnInit{
  sources: any[] = [];
  isLoading = false;
  error: string | null = null;

  constructor(private dialog: MatDialog, private router: Router,) {}

  ngOnInit() {
    this.fetchSources();
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
  
    if (source.sourceTypeId == 1) {
      this.router.navigate(['/pdfsource', source.sourceId]);
    } else {
      this.router.navigate(['/videosource', source.sourceId]);
    }
    //this.router.navigate(['/source', source.sourceId]);
    setTimeout(() => {
      this.reloadCurrentRoute();
    }, 100);
}
private reloadCurrentRoute() {
  const currentUrl = this.router.url;
  this.router.navigateByUrl('/', { skipLocationChange: true }).then(() => {
    this.router.navigate([currentUrl]);
  });
}
}
