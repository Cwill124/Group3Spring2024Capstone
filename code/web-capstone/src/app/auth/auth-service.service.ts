// auth.service.ts
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
@Injectable({
  providedIn: 'root'
})
export class AuthServiceService {
  private tokenKey: any | null = null;
  private redirectUrl: any | null = null;

  constructor(private router: Router) {}

  setToken(token: string): void {
    localStorage.setItem(this.tokenKey, token);
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenKey);
  }

  setRedirectUrl(url: string): void {
    this.redirectUrl = url;
  }

  getRedirectUrl(): string {
    return this.redirectUrl;
  }

  isLoggedIn(): boolean {
    return !!this.getToken() && !this.isTokenExpired();
  }

  // Check if the token is expired
  isTokenExpired(): boolean {
    const token = this.getToken();
    if (token) {
      const expirationDate = this.getExpirationDate(token);
      return expirationDate ? expirationDate <= new Date() : false;
    }
    return true;
  }

  // Get the expiration date from the token
  private getExpirationDate(token: string): Date | null {
    const decoded = JSON.parse(atob(token.split('.')[1]));
    return decoded.exp ? new Date(decoded.exp * 1000) : null;
  }

  // Other authentication-related methods

  // Example of a method to handle successful login
  loginSuccess(): void {
    // Redirect to the stored or default URL after successful login
    this.router.navigateByUrl(this.redirectUrl);
  }

  // Example of a method to handle logout
  logout(): void {
    console.log(this.tokenKey);
    localStorage.removeItem(this.tokenKey);
    this.router.navigate(['/login']);
    // Redirect or perform other actions after logout
  }
  // Other authentication-related methods
}
