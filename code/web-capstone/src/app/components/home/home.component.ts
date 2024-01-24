import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { AuthServiceService } from '../../auth/auth-service.service';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [RouterModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent {

  authService: AuthServiceService;
  constructor(authService: AuthServiceService) { 
    this.authService = authService;
  }

  onLogout() {
    console.log('logout');
    this.authService.logout();
  }
}
