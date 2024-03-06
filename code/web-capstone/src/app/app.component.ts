import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AuthServiceService } from './auth/auth-service.service'
import {NavbarComponent} from './components/navbar/navbar.component'
@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,CommonModule,NavbarComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'web-capstone';

  validUser: boolean = false;
  init() {
  }
  constructor(private authService: AuthServiceService) { } 


  isAuthenticated() {
    return this.authService.isLoggedIn();
  }



}
