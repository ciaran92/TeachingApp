import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-create-quiz',
  templateUrl: './create-quiz.component.html',
  styleUrls: ['./create-quiz.component.css']
})
export class CreateQuizComponent implements OnInit {

  currentStep: number;

  constructor() { }

  ngOnInit() {
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
