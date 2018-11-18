import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Question } from '../shared/models/question.model';
import { iQuizInstanceAnswerOption } from '../shared/models/iQuizInstanceAnswerOption.model';
import { iQuizInstanceAnswer } from '../shared/models/iquiz-instance-answer.model';
import { Observable } from 'rxjs';





@Injectable({
  providedIn: 'root'
})
export class QuizInstanceService {
    
  private quizInstanceUrl = "http://localhost:52459/api/quizinstance";
  private quizInstanceAnswerURL = "http://localhost:52459/api/quizinstanceanswer";
  
  constructor(private http: HttpClient) { }
  

  getQuizInstanceQuestionAnswerOptions(quizId: number): Observable<iQuizInstanceAnswerOption[]>{
    var result = this.http.get<iQuizInstanceAnswerOption[]>(this.quizInstanceUrl + "/" + quizId);
   
    return result;
  }

  createQuizInstanceAnswer(quizInstanceAnswer: iQuizInstanceAnswer){
    //console.log("QuizInstanceId: " + quizInstanceAnswer.QuizInstanceId);
    //console.log("QuestionId: " + quizInstanceAnswer.QuestionId);
    //console.log("AnswerId: " + quizInstanceAnswer.AnswerId);
    //console.log("AppUserAnswer: " + quizInstanceAnswer.AppUserAnswer);
    //console.log("AppUserAnswerDateTime: " + quizInstanceAnswer.AppUserAnswerDateTime);
    
    var body = {
      QuizInstanceId: quizInstanceAnswer.QuizInstanceId,
      QuestionId: quizInstanceAnswer.QuestionId,
      AnswerId: quizInstanceAnswer.AnswerId,
      AppUserAnswer: quizInstanceAnswer.AppUserAnswer,
      AppUserAnswerDateTime: quizInstanceAnswer.AppUserAnswerDateTime
    };

    return this.http.post(this.quizInstanceAnswerURL, body);

  }

}
