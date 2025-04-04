import { Routes } from '@angular/router';
import { LoginComponent } from './components/login/login.component';
import { AuthGuard } from './auth/auth.guard'
import { RegisterComponent } from './components/register/register.component';
import { SourcePageComponent } from './pages/source-page/source-page.component';
import {SourceViewerComponent} from './pages/source-viewer/source-viewer.component';
import {NotePageComponent} from './pages/note-page/note-page.component';
import { ProjectPageComponent } from './pages/project-page/project-page.component';
import { ProjectViewerComponent } from './pages/project-viewer/project-viewer.component';
export const routes: Routes = [
	{path: '', redirectTo: '/projects', pathMatch: 'full'},
	{path: 'sources', component: SourcePageComponent, canActivate: [AuthGuard]},
	{path: 'notes', component: NotePageComponent , canActivate: [AuthGuard]},
	{path: 'projects', component: ProjectPageComponent, canActivate: [AuthGuard]},
	{path: 'login', component: LoginComponent},
	{path: 'register', component: RegisterComponent},
	{path: 'sourceViewer/:id/:sourceType', component: SourceViewerComponent, canActivate: [AuthGuard]},
	{path: 'projectViewer/:id', component: ProjectViewerComponent, canActivate: [AuthGuard]},
];

