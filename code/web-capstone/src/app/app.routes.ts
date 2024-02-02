import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './auth/auth.guard'
import { RegisterComponent } from './components/register/register.component';

export const routes: Routes = [
	{path: '', redirectTo: '/home', pathMatch: 'full'},
	{path: 'home', component: HomeComponent,
	canActivate: [AuthGuard] // visit this route only if logged in
	},
	{path: 'login', component: LoginComponent},
	{path: 'register', component: RegisterComponent}

];

