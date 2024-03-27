import { Component } from '@angular/core';
import { AuthServiceService } from '../../auth/auth-service.service'
import { OnInit } from '@angular/core';
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent implements OnInit {
user: string = "";
  userDataLoaded: boolean = false;
  constructor(private authService : AuthServiceService)  { 
  
  }
  ngOnInit() : void {
    setTimeout(() => {
      const user = JSON.parse(localStorage.getItem('user') || '{}');
      this.user = user.username;
    }, 50);
  
  }
  onLogout() : void {
    this.authService.logout();
  }
}
