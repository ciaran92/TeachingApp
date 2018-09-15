import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { Question } from './question';

@Injectable()
export class QuizService {
    private questionsUrl = "http://localhost:52459/api/questions";
    private rootURL = "http://localhost:52459/api/users";
    constructor(private http: Http){
        
    }

    getQuestions(): Observable<Question[]>{
        return this.http.get(this.questionsUrl).pipe(map((response: Response) => <Question[]>response.json()));
    }

    createUser(firstName: string, lastName: string, username: string, userPassword: string){
        var body = {
            FirstName: firstName,
            LastName: lastName,
            Username: username,
            UserPassword: userPassword
        }
        return this.http.post(this.rootURL, body);
    }
}