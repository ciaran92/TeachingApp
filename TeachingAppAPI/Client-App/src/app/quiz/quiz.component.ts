import { Component, OnInit } from '@angular/core';
import { Question } from './question';
import { Options } from './options';
import { QuizService } from './quiz.service';
import { Observable, timer } from 'rxjs';


@Component({
  selector: 'quiz',
  templateUrl: './quiz.component.html',
  styleUrls: ['./quiz.component.css']
})

export class QuizComponent implements OnInit {

  questions: Question[];
  questionNumber: number = 0;
  quizOver: boolean = false;
  answers: any[];

  constructor(private quizService: QuizService) { 
  }

  ngOnInit() {
    this.getQuestions();
    this.answers = [];
    for(var i: number = 0; i < 10; i++){
        this.answers[i] = null;
    }
    
  }

  getQuestions(){
    this.quizService.getQuestions().subscribe(questions => this.questions = questions);
  }

  nextQuestion(){
    if(this.questionNumber + 1 <= (this.questions.length - 1)){
      this.questionNumber++;
      console.log("question No. = " + (this.questionNumber + 1)); // Have to add +1 as questionNumber starts @ 0;
    }
  }

  previousQuestion(){
    
    if(this.questionNumber + 1 >= 2){
      this.questionNumber--;
      console.log("question No. = " + (this.questionNumber + 1));
    }
  }

  checkAnswer(logh: any){
    var tempAnswer = this.questions[this.questionNumber].answer;
    this.answers[this.questionNumber] = logh;
    console.log("answers array length = " + this.answers.length);
    console.log("answers array: " + this.answers);
    if(logh == tempAnswer){
      console.log("correct");
    }else{
      console.log("wrong");
    }
  }

  public submitQuiz(){
    console.log("checking answers");
    console.log(this.checkQuizFinished());
    if(this.checkQuizFinished()){
      this.quizOver = true;
      console.log("you got " + this.calculateResults() + " Questions correct");
    }
    else{
      console.log("Please answere all questions before submitting");
    }
  }

  public checkQuizFinished(): boolean{
    for(var i: number = 0; i < this.answers.length; i++){
      if(this.answers[i] == null){
        this.quizOver = false;
        return false;
      }
    }
    this.quizOver = true;
    return true;
  }

  calculateResults():number{
    var score: number = 0;
    for(var i: number = 0; i < this.answers.length; i++){
      if(this.answers[i] == this.questions[i].answer){
        score++;
      }
    }
    return score;
  }

}
