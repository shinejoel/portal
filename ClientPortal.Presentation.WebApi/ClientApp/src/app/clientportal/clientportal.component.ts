import { Component, OnInit } from '@angular/core';
import { FormControl } from '@angular/forms';
import {TooltipPosition} from '@angular/material/tooltip';

@Component({
  selector: 'app-clientportal',
  templateUrl: './clientportal.component.html',
  styleUrls: ['./clientportal.component.css']
})
export class ClientportalComponent implements OnInit {

  constructor() { }

  ngOnInit(): void {
  }

  positionOptions: TooltipPosition[] = ['after', 'before', 'above', 'below', 'left', 'right'];
  position = new FormControl(this.positionOptions[0]);


  tasks =[
    {value: 'Project one', completed: false},
    {value: 'Project two', completed: false},
    {value: 'Project three', completed: false},
    {value: 'Project 4', completed: false},
    {value: 'Project 5', completed: false},
    {value: 'Project 6', completed: false},
    {value: 'Project 7', completed: false},
    {value: 'Project 8', completed: false},
    {value: 'Project 9', completed: false},
    {value: 'Project 0', completed: false},
    {value: 'Project 11', completed: false},
    {value: 'Project 12', completed: false},
    {value: 'Project 13', completed: false},
    {value: 'Project 14', completed: false},
    {value: 'Project 15', completed: false},
    {value: 'Project 16', completed: false},
  ];

  selectAllChanged(event: any){
    if(event.checked){
      //select all checkbox
      this.tasks = this.tasks.map((task)=>{
        task.completed = true;
        return task;
      });
    }
    else{
      //Unselect all checkbox
      this.tasks = this.tasks.map((task)=>{
        task.completed = false;
        return task;
      });
    }
  }
}
