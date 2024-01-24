import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthServiceService } from '../../auth/auth-service.service';
import { YouTubePlayerModule } from '@angular/youtube-player';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [
    RouterModule,
    YouTubePlayerModule,
  ],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  authService: AuthServiceService;
  title = 'web-capstone';
  videoSource: any = '';
  pdfSource: any = '';

  constructor(authService: AuthServiceService) { 
    this.authService = authService;
    this.loadVideo();
    this.loadPdf();
  }

  onLogout() {
    console.log('logout');
    this.authService.logout();
  }
  loadVideo() {
    fetch('https://localhost:7062/TempContent/GetVideo', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(1),
    })
      .then((response) => response.json())
      .then((data) => {
        // Assuming data.link is the video source, sanitize the URL
        console.log(data.link);
        this.videoSource = data.link;
        console.log(this.videoSource);
      })
      .catch((error) => {
        console.error(error);
      });
  }
  loadPdf() {
    fetch('https://localhost:7062/TempContent/GetPdf', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
      },
      body: JSON.stringify(1),
    })
      .then((response) => response.json())
      .then((data) => {
        // Assuming data.link is the video source, sanitize the URL
        console.log(data.link);
        this.pdfSource = data.link;
        console.log(this.pdfSource);
      })
      .catch((error) => {
        console.error(error);
      });
  }
}
