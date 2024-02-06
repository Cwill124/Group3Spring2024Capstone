import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { OnInit } from '@angular/core';
import { SourceAsideComponent } from '../../components/source-aside/source-aside.component';
import {DomSanitizer} from "@angular/platform-browser";

@Component({
  selector: 'app-pdfsource',
  standalone: true,
  imports: [SourceAsideComponent,CommonModule],
  templateUrl: './pdfsource.component.html',
  styleUrl: './pdfsource.component.css'
})
export class PDFSourceComponent implements OnInit {
  name: string = '';
  id: string = '';
  author: string = '';
  description: string = '';
  publisher: string = '';
  year: string = '';
  url: any = '';
  createdBy: string = '';
  isLoading = false;

  constructor(private route: ActivatedRoute,private dataSanitizer: DomSanitizer) {
    console.log(this.route.snapshot.params);
    
  }
  ngOnInit(): void {
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
    this.fetchSource();
  }  
  async fetchSource() {
    this.isLoading = true;
   await fetch('https://localhost:7062/Sources/GetById', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(this.id),
    }).then(response => {
      if (!response.ok) {
        throw new Error('Network response was not ok');
      }
      return response.json();
    }).then(data => {
      // Set the source properties
      console.log(data);
      let contentJson = JSON.parse(data.content);
      let metaDataJson = JSON.parse(data.metaData);
      this.name = data.name;
      this.description = data.description;
      this.publisher = metaDataJson.publisher;
      this.year = metaDataJson.year;
      this.url = this.dataSanitizer.bypassSecurityTrustResourceUrl(contentJson.url);
      this.author = metaDataJson.author;
      this.createdBy = data.createdBy;
      

    }).finally(() => {
      this.isLoading = false;
    });
  }
}
