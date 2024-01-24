import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthServiceService } from '../../auth/auth-service.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  constructor(private authService: AuthServiceService) { }

  onLogin(data: any) {
    console.log(data);
    let token: any = null;
    
    fetch('https://localhost:7062/Login', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
    })
    .then(response => {
        if (response.ok) {
            return response.json(); // Parse JSON here and return the promise
        } else {
            throw new Error('Failed to login');
        }
    })
    .then(parsedData => {
        console.log(parsedData);
        // Do something with parsedData, e.g., set token
        token = parsedData.token;
        this.authService.setToken(token);
        this.authService.setRedirectUrl('/home');

        // Handle successful login
        this.authService.loginSuccess();
        
    })
    .catch(error => {
        console.error(error);
    });
}

}
