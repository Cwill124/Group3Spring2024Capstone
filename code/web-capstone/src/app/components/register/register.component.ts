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

    if (this.checkPassword(data.password, data.confirmPassword)) {
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

  private checkPassword(password: string, confirmPassword: string): boolean {
    return password === confirmPassword;
  }
}
