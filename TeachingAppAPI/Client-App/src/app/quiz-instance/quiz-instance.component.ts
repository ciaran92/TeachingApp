import { Component, OnInit } from '@angular/core';
import { QuizInstanceService } from '../services/quiz-instance.service';
import { QuestionService } from '../services/question.service';
import { Question } from '../shared/models/question.model';
import { iQuizInstanceAnswerOption } from '../shared/models/iQuizInstanceAnswerOption.model';
import { iQuizInstanceAnswer } from '../shared/models/iquiz-instance-answer.model';
import { QuizInstanceAnswer } from '../shared/models/quiz-instance-answer.model';
import { getLocaleTimeFormat } from '@angular/common';
import { error } from '@angular/compiler/src/util';

@Component({
  selector: 'app-quiz-instance',
  templateUrl: './quiz-instance.component.html',
  styleUrls: ['./quiz-instance.component.css']
})
export class QuizInstanceComponent implements OnInit {

  allQuestionsAnswerOptions: iQuizInstanceAnswerOption[];
  //singleQuestionAnswerOptions: any[];
  quizOver: boolean = false;
  questions: Question[];
  userAnswers: QuizInstanceAnswer[];
  quizInstance: number;

  constructor(private quizInstanceService: QuizInstanceService, private questionService: QuestionService) { }

  ngOnInit() {
    this.allQuestionsAnswerOptions = [];
    this.userAnswers = [];
    this.quizInstance = 1;

    this.questionService.getQuizQuestions(this.quizInstance)
      .subscribe((response) => {
        this.questions = response;
      });
    this.quizInstanceService.getQuizInstanceQuestionAnswerOptions(this.quizInstance)
      .subscribe((response) => {
        this.allQuestionsAnswerOptions = response;
        }); 
    
  }

  storeUserAnswer(userAnswer: iQuizInstanceAnswerOption){
    this.userAnswers.push(this.createQuizInstanceAnswer(userAnswer));
    /*
    for (let element of this.allQuestionsAnswerOptions){
      if (userAnswer.answerID == element.answerID){
        this.userAnswers.push( this.createQuizInstanceAnswer(element));
      }
    }
    */
  }

  createQuizInstanceAnswer(userAnswer: iQuizInstanceAnswerOption){
    
    let now = new Date();
    now.getDate();
    
    
    var quizInstanceAnswer = {} as QuizInstanceAnswer;
    quizInstanceAnswer.QuizInstanceId = userAnswer.quizInstanceId;
    quizInstanceAnswer.QuestionId = userAnswer.questionId;
    quizInstanceAnswer.AnswerId = userAnswer.answerID;
    quizInstanceAnswer.AppUserAnswer = "Not empty";
    quizInstanceAnswer.AppUserAnswerDateTime = now;
    console.log("quizInstanceAnswer: " + quizInstanceAnswer);
    return quizInstanceAnswer;
  }

  checkAnswer(userAnswer: iQuizInstanceAnswerOption, divElement: any) {
    if (this.checkIfAlreadyAnswered(userAnswer)){
      // Check if user answer is correct or incorrect:
      if(userAnswer.answerTypeDesc == 1){
        (divElement).style.background = 'green';
      }else{
        (divElement).style.background = 'red';
      }
      this.storeUserAnswer(userAnswer);
    }else{
      console.log("already answered");
    }
  }

  checkIfAlreadyAnswered(userAnswer: iQuizInstanceAnswerOption): boolean { 
    if (this.userAnswers.some(x =>x.QuestionId === userAnswer.questionId)){
      return false;
    }else{
      return true;
    }
  }

  submitQuizResults(){
    if (this.checkQuizFinished()){
      console.log("checkQuizFinished = true");
      //console.log(JSON.stringify(this.userAnswers));


      for (let element of this.userAnswers){
        this.quizInstanceService.createQuizInstanceAnswer(element).subscribe((result)=>
          {
            console.log("Resly from webAPI: " + result);
            
          }, error => 
          {
            console.log(error);
            
          });
      }
    }else{
      // need to return a message to user?
      console.log("checkQuizFinished = false");
    }
    
  }

  checkQuizFinished(): boolean {
    if (this.userAnswers.length < 10){
      return false;
    }else{
      return true;
    }
  }



  /*
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
*/

}
