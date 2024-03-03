import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';

@Component({
  selector: 'app-note-page',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule,CommonModule],
  templateUrl: './note-page.component.html',
  styleUrls: ['./note-page.component.css']
})
export class NotePageComponent implements OnInit {
  private isDialogOpen = false;
  isLoading = false;
  error: string | null = null;
  notes: any[] = [];

  constructor(private dialog: MatDialog, private router: Router) {}

  ngOnInit() {
    this.fetchNotes();
  }

  async fetchNotes() {
    this.isLoading = true;
    let username = JSON.parse(localStorage["user"])?.username;

    try {
      const response = await fetch('https://localhost:7062/Notes/GetByUsername', {
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
      this.notes = data;
    } catch (error) {
      console.error('There has been a problem with your fetch operation:', error);
      this.error = 'Failed to fetch notes. Please try again later.';
    } finally {
      this.isLoading = false;
      console.log(this.notes);
    }
  }

  searchNotes() {

  }

  filterNotes() {
    
  }

}
