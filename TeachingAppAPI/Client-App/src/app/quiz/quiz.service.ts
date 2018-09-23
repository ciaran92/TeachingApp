import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs';
import { map } from 'rxjs/operators';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable()
export class QuizService {
    private questionsUrl = "http://localhost:52459/api/questions";
    private rootURL = "http://localhost:52459/api/users";

    constructor(private http: HttpClient){}

    /**
     * returns http get request for questions.
     * headers require token that is generated on login
     * for authorization.
     */
    getQuestions(): any {
        let token = localStorage.getItem("jwt");
        console.log("i got this far");
        var result = this.http.get(this.questionsUrl, {
            headers: new HttpHeaders({
                "Authorization": "Bearer " + token,
                "Content-Type": "application/json"
            })
        });
        return result;
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

    /**
     * called from login.component.ts login()
     * @param credentials JSON stringified creds
     */
    login(credentials){
        var requiredHeader = new HttpHeaders({'Content-Type':'application/json'});
        //console.log("reqHeader: " + reqHeader);
        return (this.http.post("http://localhost:52459/api/users/token", credentials, { headers: requiredHeader }));
    }

    loggedIn(){
        return !!localStorage.getItem('jwt');
    }
}