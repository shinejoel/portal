import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { SidenavComponent } from './sidenav/sidenav.component';
import { HomeComponent } from './home/home.component';
import { HeaderComponent } from './header/header.component';
import { DashboardComponent } from './dashboard/dashboard.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatSidenavModule } from '@angular/material/sidenav'
import {MatToolbarModule } from '@angular/material/toolbar'
import {MatMenuModule } from '@angular/material/menu'
import {MatIconModule } from '@angular/material/icon'
import {MatDividerModule } from '@angular/material/divider'
import {MatListModule } from '@angular/material/list'
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { ClientportalComponent } from './clientportal/clientportal.component';
import {MatCheckboxModule} from '@angular/material/checkbox'
import { FormsModule } from '@angular/forms';
import {TooltipPosition} from '@angular/material/tooltip';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatFormFieldModule } from '@angular/material/form-field';
import { ProjectsComponent } from './projects/projects.component';


@NgModule({
  declarations: [ AppComponent,HeaderComponent,SidenavComponent,HomeComponent,DashboardComponent,
    AppComponent,
    SidenavComponent,
    HomeComponent,
    HeaderComponent,
    DashboardComponent,
    ClientportalComponent,
    ProjectsComponent,

  ],
  imports: [NgbModule,
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    BrowserAnimationsModule,

    //*Material import
    MatSidenavModule,
    MatToolbarModule,
    MatMenuModule,
    MatIconModule,
    MatDividerModule,
    MatListModule,
    MatCheckboxModule,
    MatSelectModule,
    MatInputModule,
    MatFormFieldModule


  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
