import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { HomeComponent } from './components/home/home.component';
import { AuthGuard } from './auth/auth.guard'
import { RegisterComponent } from './components/register/register.component';
import { SourcePageComponent } from './pages/source-page/source-page.component';
import {PDFSourceComponent} from './pages/pdfsource/pdfsource.component';
export const routes: Routes = [
	{path: '', redirectTo: '/home', pathMatch: 'full'},
	{path: 'home', component: HomeComponent,
	canActivate: [AuthGuard] // visit this route only if logged in
	},
	{path: 'sources', component: SourcePageComponent, canActivate: [AuthGuard]},
	{path: 'login', component: LoginComponent},
	{path: 'register', component: RegisterComponent},
	{path: 'source/:id', component: PDFSourceComponent, canActivate: [AuthGuard]}
	

];

