import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Question } from '../shared/models/question.model';
import { iQuestion } from '../shared/models/iquestion.model';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class QuestionService {

  private quizquestionsUrl = "http://localhost:52459/api/questions";
  
  constructor(private http: HttpClient) { }


  getQuizQuestions(quizId: number): Observable<iQuestion[]>{
    var result = this.http.get<iQuestion[]>(this.quizquestionsUrl + "/" + quizId);
    return result;
  }

}
