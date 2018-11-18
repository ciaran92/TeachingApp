import { Component, OnInit, Input } from '@angular/core';
import { CreateCourseService } from '../services/create-course.service';

@Component({
  selector: 'app-create-course',
  templateUrl: './create-course.component.html',
  styleUrls: ['./create-course.component.css']
})
export class CreateCourseComponent implements OnInit{
  
  currentStep: number;

  constructor(private courseService: CreateCourseService) {}

  ngOnInit() {
    this.courseService.currentStep.subscribe(step => this.currentStep = step);
  } 

  GetCurrentStep(): number{
    return this.currentStep;
  }

  ChangeCurrentStep(newStepNumber: number){
    this.currentStep = newStepNumber;
    console.log("step: " + this.currentStep);
  }

  isClicked(step: number):boolean{
    if(this.GetCurrentStep() == step){
      return true;
    }else{
      return false;
    }
    
  }
}
