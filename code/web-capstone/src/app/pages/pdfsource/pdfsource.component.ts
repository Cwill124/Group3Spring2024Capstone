import { Component } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { OnInit } from '@angular/core';
import { SourceAsideComponent } from '../../components/source-aside/source-aside.component';

@Component({
  selector: 'app-pdfsource',
  standalone: true,
  imports: [SourceAsideComponent],
  templateUrl: './pdfsource.component.html',
  styleUrl: './pdfsource.component.css'
})
export class PDFSourceComponent implements OnInit {
  name: string = '';
  id: string = '';
  author: string = '';
  constructor(private route: ActivatedRoute) {
    console.log(this.route.snapshot.params);
    this.id = this.route.snapshot.paramMap.get('id') ?? '';
  }
  ngOnInit(): void {
    
  }  
}
