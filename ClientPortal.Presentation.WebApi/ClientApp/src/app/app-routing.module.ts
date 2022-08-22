import { DashboardComponent } from './dashboard/dashboard.component';
import { HomeComponent } from './home/home.component';
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ClientportalComponent } from './clientportal/clientportal.component';
import { ProjectsComponent } from './projects/projects.component';

const routes: Routes = [
  {path: '', redirectTo: 'home', pathMatch: 'full'},
  {path:'home', component:HomeComponent},
  {path:'dashboard', component:DashboardComponent},
  {path:'clientportal', component:ClientportalComponent},
  {path:'projects', component:ProjectsComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }

