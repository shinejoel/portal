import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'clientportal';
  sideBarOpen =true;

  ngOnInit() {}

  sideBarToggler(){
    this.sideBarOpen = !this.sideBarOpen;
  }
}
