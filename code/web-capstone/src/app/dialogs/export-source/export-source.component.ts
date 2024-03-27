import { Component } from '@angular/core';
import { Input } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-export-source',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './export-source.component.html',
  styleUrl: './export-source.component.css'
})
export class ExportSourceComponent {

  @Input('sources') sources: any[] = [];
  constructor() { 

  }
  closeExportSource() {
    const dialog = document.getElementById('export-source') as HTMLDialogElement;
    dialog.close();
  }
  parseMetadata(metaDataString: string) {
    return JSON.parse(metaDataString);
  }
}
