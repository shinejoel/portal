import { Component, OnInit } from '@angular/core';
import {FormControl, Validators} from '@angular/forms';

@Component({
  selector: 'app-projects',
  templateUrl: './projects.component.html',
  styleUrls: ['./projects.component.css']
})
export class ProjectsComponent implements OnInit {


  constructor() { }

  ngOnInit(): void {
  }


  userGroupControl = new FormControl('', [Validators.required]);

  usergroup = [
    {name: 'Dog',},
    {name: 'Cat',},
    {name: 'Cow', },
    {name: 'Fox',},
  ];


}
