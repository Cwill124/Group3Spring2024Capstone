import { Component  } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthServiceService } from '../../auth/auth-service.service';
import { YouTubePlayerModule } from '@angular/youtube-player';
import {DomSanitizer} from "@angular/platform-browser";
import { NgxExtendedPdfViewerModule  } from 'ngx-extended-pdf-viewer';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterModule,
    YouTubePlayerModule,
    NgxExtendedPdfViewerModule
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {
  authService: AuthServiceService;
  videoSource: any = '';
  pdfSource: any = '';
  currentUser: any = '';
  constructor(authService: AuthServiceService, private dataSanitizer: DomSanitizer) { 
    this.authService = authService;
    this.dataSanitizer = dataSanitizer;
    
  
  }
  ngOnInit(): void {
    const user = JSON.parse(localStorage.getItem('user') || '{}');
    this.currentUser = user.username;
  }
  // loadVideo() {
  //   fetch('https://localhost:7062/TempContent/GetVideo', {
  //     method: 'POST',
  //     headers: {
  //       'Content-Type': 'application/json',
  //     },
  //     body: JSON.stringify(1),
  //   })
  //     .then((response) => response.json())
  //     .then((data) => {
  //       // Assuming data.link is the video source, sanitize the URL
  //       this.videoSource = this.dataSanitizer.bypassSecurityTrustResourceUrl(data.link);
  //     })
  //     .catch((error) => {
  //       console.error(error);
  //     });
  // }
  // loadPdf() {
  //   fetch('https://localhost:7062/TempContent/GetPdf', {
  //     method: 'POST',
  //     headers: {
  //       'Content-Type': 'application/json',
  //     },
  //     body: JSON.stringify(1),
  //   })
  //     .then((response) => response.json())
  //     .then((data) => {
  //       // Assuming data.link is the video source, sanitize the URL
  //       console.log(data.link);
  //       this.pdfSource = this.dataSanitizer.bypassSecurityTrustResourceUrl(data.link);
  //       console.log(this.pdfSource);
  //     })
  //     .catch((error) => {
  //       console.error(error);
  //     });
  // }
}
