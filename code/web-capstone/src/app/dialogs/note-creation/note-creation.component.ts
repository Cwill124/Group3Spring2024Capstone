import { Component } from '@angular/core';
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule } from '@angular/material/dialog';
import { MatDialog } from '@angular/material/dialog';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
@Component({
  selector: 'app-note-creation',
  standalone: true,
  imports: [MatButtonModule, MatDialogModule,CommonModule,FormsModule],
  templateUrl: './note-creation.component.html',
  styleUrl: './note-creation.component.css'
})
export class NoteCreationComponent {

}
