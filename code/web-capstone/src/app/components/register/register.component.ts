import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {

 
  constructor(private router: Router) { }

  onRegister(data: any): void {
    console.log(data);

    if (this.checkInputtedInfo(data)) {
      fetch('https://localhost:7062/Register', {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json'
        },
        body: JSON.stringify(data)
      })
        .then(response => {
          if (response.ok) {
            // Use 'navigate' instead of 'redirectTo'
            this.router.navigate(['/login']);
          } else {
            throw new Error('Failed to register');
          }
        })
        .catch(error => {
          console.error(error);
        });
    }
  }

  private checkInputtedInfo(data : any): boolean {
    const usernameRegex = /^[a-zA-Z0-9_]{5,}$/;
    const emailRegex = /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$/;
    const phoneRegex: RegExp = /^\d{10}$/;
    const passwordRegex: RegExp = /^(?=.*[A-Z])(?=.*\d).{8,}$/;
    let errorMessage = '';
    if(!usernameRegex.test(data.username)) {
      errorMessage += 'Username must be at least 5 characters long and contain only letters, numbers, and underscores.\n';
    }
    if(data.firstName.length < 1) {
      errorMessage += 'First name cannot be empty.\n';
    }
    if(data.lastName.length < 1) {
      errorMessage += 'Last name cannot be empty.\n';
    }
    if(!phoneRegex.test(data.phone)) {
      errorMessage += 'Phone number must be 10 digits long. No [ - ] inbetween\n';
    }
    if(!emailRegex.test(data.email)) {
      errorMessage += 'Email must be valid.\n';
    }
    if(!passwordRegex.test(data.password)) {
      errorMessage += 'Password must be at least 8 characters long and contain at least one uppercase letter and one number.\n';
    }
    if(data.password !== data.confirmPassword) {
      errorMessage += 'Passwords do not match.\n';
    }
    if(errorMessage.length > 0) {
      alert(errorMessage);
      return false;
    } else {
      return true;
    }
  }
}
