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

  ngOnInit(): void {
    localStorage.clear();
  }

 async onLogin(data: any) {
    let token: any = null;
    
   await fetch('https://localhost:7062/Login', {
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
            throw new Error('Failed to login. Please check your credentials and try again.');
        }
    })
    .then(async parsedData => {
        console.log(parsedData);
        // Do something with parsedData, e.g., set token
        token = parsedData.token;
        this.authService.setToken(token);
        this.authService.setRedirectUrl('/home');

        await this.getUserData(data.username);
        // Handle successful login
        this.authService.loginSuccess();
        
    })
    .catch(error => {
        alert(error);
    });
}
private async getUserData(username: String)  {
    const token = this.authService.getToken();
    if (token) {
       await fetch('https://localhost:7062/GetUserByUsername', {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json',
            },
            body: JSON.stringify({ username })
        }).then(response => {
            if (response.ok) {
                return response.json();
            } else {
                throw new Error('Failed to get user data');
            }
        }).then(parsedData => {
            localStorage.setItem('user', JSON.stringify(parsedData));
        }).catch(error => {
        });
    }
}

}
