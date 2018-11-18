import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class Quiz2Service {

  private rootURL = "http://localhost:52459/api/quiz";
    

  constructor(private http: HttpClient) { }
  
  getQuizzes(): any {
    var result = this.http.get(this.rootURL);
    
    return result;
  }

}
