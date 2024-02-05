import { Component } from '@angular/core';
import { AuthServiceService } from '../../auth/auth-service.service'
@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css'
})
export class NavbarComponent {

  constructor(private authService : AuthServiceService) { }

  onLogout() : void {
    this.authService.logout();
  }
}
